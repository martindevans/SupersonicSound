using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SupersonicSound.LowLevel
{
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

            return new FSHandle(File.Open(name, FileMode.Open));
        }

        public void Close(FSHandle handle)
        {
            handle.Stream.Close();
        }

        private static uint CancellableRead(FSHandle handle, Stream buffer, uint offset, uint readBytes, CancellationToken ct)
        {
            buffer.Seek(offset, SeekOrigin.Begin);

            uint i = 0;
            for (; i < readBytes; i++)
            {
                if (ct.IsCancellationRequested)
                    break;

                var b = handle.Stream.ReadByte();
                if (b == -1)
                    break;
                buffer.WriteByte((byte)b);
            }

            return i;
        }

        public Task<uint> AsyncRead(FSHandle handle, Stream buffer, uint offset, uint readBytes, CancellationToken cancellation)
        {
            return Task<uint>.Factory.StartNew(() => CancellableRead(handle, buffer, offset, readBytes, cancellation), cancellation);
        }

        public int HandleID(FSHandle handle)
        {
            return handle.ID;
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
