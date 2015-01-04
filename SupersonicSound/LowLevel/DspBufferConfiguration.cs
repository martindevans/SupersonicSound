
namespace SupersonicSound.LowLevel
{
    public struct DspBufferConfiguration
    {
        public uint BufferLength { get; private set; }

        public int NumBuffers { get; private set; }

        public DspBufferConfiguration(uint length, int count)
            :this()
        {
            BufferLength = length;
            NumBuffers = count;
        }
    }
}
