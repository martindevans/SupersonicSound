namespace SupersonicSound.LowLevel
{
    public struct DspClock
    {
        public readonly ulong Clock;
        public readonly ulong Parent;

        public DspClock(ulong clock, ulong parent)
        {
            Clock = clock;
            Parent = parent;
        }
    }
}
