using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FMOD;
using System;
using System.IO;
using SupersonicSound.Exceptions;

namespace SupersonicSound.LowLevel
{
    public interface IFileSystem<THandle>
    {
        int BlockAlign { get; }

        THandle Open(string name, out uint fileSize);

        void Close(THandle handle);

        Task<uint> AsyncRead(THandle handle, Stream buffer, uint offset, uint readBytes, CancellationToken cancellation);

        int HandleID(THandle handle);
    }

    public enum FileError
    {
        BadFile = RESULT.ERR_FILE_BAD,
        CouldNotSeek = RESULT.ERR_FILE_COULDNOTSEEK,
        DiskEjected = RESULT.ERR_FILE_DISKEJECTED,
        EndOfData = RESULT.ERR_FILE_ENDOFDATA,
        EndOfFile = RESULT.ERR_FILE_EOF,
        FileNotFound = RESULT.ERR_FILE_NOTFOUND,
    }

    internal class FileSystemWrapper<THandle>
        : IFileSystemWrapper
    {
        private readonly IFileSystem<THandle> _fileSystem;

        private readonly ConcurrentDictionary<int, THandle> _handleMap = new ConcurrentDictionary<int, THandle>();
        private readonly ConcurrentDictionary<int, List<Tuple<CancellationTokenSource, Task>>> _asyncReadTasks = new ConcurrentDictionary<int, List<Tuple<CancellationTokenSource, Task>>>();

        public FileSystemWrapper(IFileSystem<THandle> fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public RESULT UserOpen(string name,
// ReSharper disable RedundantAssignment
            ref uint filesize,
            ref IntPtr handle,
// ReSharper restore RedundantAssignment
            IntPtr userdata)
        {
            IntPtr fh = IntPtr.Zero;
            uint fs = 0;

            RESULT r = DoWithExceptionHandling(() => {
                var fsHandle = _fileSystem.Open(name, out fs);
                var id = _fileSystem.HandleID(fsHandle);
                _handleMap.AddOrUpdate(id, fsHandle, (_, __) => { throw new InvalidOperationException("File handle already in use"); });

                fh = new IntPtr(id);
            });

            handle = fh;
            filesize = fs;

            return r;
        }

        public RESULT UserClose(IntPtr handle, IntPtr userdata)
        {
            return DoWithExceptionHandling(() => {
                THandle h;
                if (!_handleMap.TryGetValue(handle.ToInt32(), out h))
                    throw new InvalidOperationException("Attempted to close a file which is not open");

                _fileSystem.Close(h);
            });
        }

        public RESULT UserAsyncRead(IntPtr handle, IntPtr infoPtr, IntPtr userdata)
        {
            unsafe
            {
                ASYNCREADINFO* info = (ASYNCREADINFO*)infoPtr.ToPointer();

                CancellationTokenSource cts = new CancellationTokenSource();
                UnmanagedMemoryStream stream = new UnmanagedMemoryStream((byte*)info->buffer, info->sizebytes);

                Task<uint> task = null;

                var r = DoWithExceptionHandling(() => {
                    THandle h;
                    if (!_handleMap.TryGetValue(handle.ToInt32(), out h))
                        throw new InvalidOperationException("Attempted to async read a file which is not open");

                    task = _fileSystem.AsyncRead(h, stream, info->offset, info->sizebytes, cts.Token);

                    //Store cancellor for later retrieval
                    var ctsList = _asyncReadTasks.GetOrAdd(handle.ToInt32(), _ => new List<Tuple<CancellationTokenSource, Task>>());
                    lock (ctsList)
                    {
                        ctsList.Add(new Tuple<CancellationTokenSource, Task>(cts, task));
                    }
                });

                if (task != null)
                {
                    task.ContinueWith(tsk => {

                        //Task is done, remove cancellor
                        List<Tuple<CancellationTokenSource, Task>> ctsList;
                        if (_asyncReadTasks.TryGetValue(handle.ToInt32(), out ctsList))
                        {
                            lock (ctsList)
                                ctsList.RemoveAll(a => a.Item1 == cts);
                        }

                        if (tsk.IsFaulted)
                        {
                            //Rethrow inner exception and convert it to a error code
                            info->result = DoWithExceptionHandling(() => { throw tsk.Exception.InnerException; });
                        }
                        else if (task.IsCanceled)
                        {
                            info->bytesread = 0;
                            info->result = RESULT.OK;
                        }
                        else
                        {
                            info->bytesread = tsk.Result;
                            info->result = RESULT.OK;
                        }
                    }, cts.Token);
                }

                return r;
            }
        }

        public RESULT UserAsyncCancel(IntPtr handle, IntPtr userdata)
        {
            List<Tuple<CancellationTokenSource, Task>> ctsList;
            if (_asyncReadTasks.TryGetValue(handle.ToInt32(), out ctsList))
            {
                lock (ctsList)
                {
                    //Cancel all tasks
                    foreach (var tuple in ctsList)
                        tuple.Item1.Cancel();

                    //Wait for all tasks
                    foreach (var tuple in ctsList)
                    {
                        while (tuple.Item2.Status == TaskStatus.Running)
                            Thread.Sleep(TimeSpan.FromTicks(1));
                    }
                }
            }

            return RESULT.OK;
        }

        private static RESULT DoWithExceptionHandling(Action a)
        {
            try
            {
                a();
            }
            catch (FmodException ex)
            {
                return ex.FMODError;
            }
            catch (FileNotFoundException ex)
            {
                throw new FmodFileNotFoundException(ex.FileName);
            }
            catch (EndOfStreamException)
            {
                throw new FmodEndOfFileException();
            }

            return RESULT.OK;
        }
    }

    internal interface IFileSystemWrapper
    {
        RESULT UserOpen(string name, ref uint filesize, ref IntPtr handle, IntPtr userdata);

        RESULT UserClose(IntPtr handle, IntPtr userdata);

        RESULT UserAsyncRead(IntPtr handle, IntPtr info, IntPtr userdata);

        RESULT UserAsyncCancel(IntPtr handle, IntPtr userdata);
    }
}
