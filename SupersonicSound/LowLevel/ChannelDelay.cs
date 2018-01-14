namespace SupersonicSound.LowLevel
{
    public struct ChannelDelay
    {
        public readonly ulong DspClockStart;
        public readonly ulong DspClockEnd;
        public readonly bool StopChannels;

        public ChannelDelay(ulong dspClockStart, ulong dspClockEnd, bool stopChannels)
        {
            DspClockStart = dspClockStart;
            DspClockEnd = dspClockEnd;
            StopChannels = stopChannels;
        }
    }
}
