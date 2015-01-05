
using FMOD;

namespace SupersonicSound.LowLevel
{
    public struct DspMeteringInfo
    {
        public int SampleCount { get; private set; }

        public float[] PeakLevel { get; private set; }

        public float[] RmsLevel { get; private set; }

        public short ChannelCount { get; private set; }

        public DspMeteringInfo(DSP_METERING_INFO info)
            : this()
        {
            SampleCount = info.numsamples;
            PeakLevel = info.peaklevel;
            RmsLevel = info.rmslevel;
            ChannelCount = info.numchannels;
        }
    }
}
