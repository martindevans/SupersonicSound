using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SupersonicSound.LowLevel
{
    /// <summary>
    /// Implementation of IFileSystem which simply reads from the real file system. Mostly exists as a handy reference for how to implement this interface.
    /// </summary>
    public class RealFileSystem
        : IFileSystem<FSHandle>
    {
        public int BlockAlign
        {
            get
            {
                return -1;
            }
        }

        public FSHandle Open(string name, out uint fileSize)
        {
            FileInfo f = new FileInfo(name);
            fileSize = (uint)f.Length;

            return new FSHandle(File.Open(name, FileMode.Open, FileAccess.Read, FileShare.Read));
        }

        public void Close(FSHandle handle)
        {
            handle.Stream.Close();
        }

        private static uint CancellableRead(FSHandle handle, Stream buffer, uint? offset, uint bytesToRead, CancellationToken ct)
        {
            lock (handle.Stream)
            {
                if (offset.HasValue)
                    handle.Stream.Seek(offset.Value, SeekOrigin.Begin);

                uint bytesRead = 0;
                for (; bytesToRead > 0; bytesToRead -= 1, bytesRead += 1)
                {
                    //Cancelled?
                    if (ct.IsCancellationRequested)
                        break;

                    //End Of File?
                    if (handle.Stream.Position == handle.Stream.Length)
                        break;

                    var readByte = handle.Stream.ReadByte();
                    if (readByte < 0)
                        break;

                    //Copy a byte
                    buffer.WriteByte((byte)readByte);
                }

                buffer.Flush();
                return bytesRead;
            }
        }

        public Task<uint> AsyncRead(FSHandle handle, Stream buffer, uint offset, uint readBytes, CancellationToken cancellation)
        {
            return Task<uint>.Factory.StartNew(() => CancellableRead(handle, buffer, offset, readBytes, cancellation), cancellation);
        }

        public int HandleID(FSHandle handle)
        {
            return handle.ID;
        }


        public uint Read(FSHandle handle, Stream stream, uint bytesToRead)
        {
            return CancellableRead(handle, stream, null, bytesToRead, new CancellationToken());
        }

        public void Seek(FSHandle handle, uint pos)
        {
            handle.Stream.Seek(pos, SeekOrigin.Begin);
        }
    }

    public struct FSHandle
    {
        private static int _nextId = 0;

        internal readonly FileStream Stream;
        internal readonly int ID;

        public FSHandle(FileStream stream)
        {
            Stream = stream;

            ID = Interlocked.Increment(ref _nextId);
        }
    }
}
