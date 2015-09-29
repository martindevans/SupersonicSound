using FMOD.Studio;

namespace SupersonicSound.Studio
{
    [EquivalentEnum(typeof(COMMANDREPLAY_FLAGS))]
    public enum CommandReplayFlags
        : uint
    {
        /// <summary>
        /// Standard behaviour.
        /// </summary>
        Normal = COMMANDREPLAY_FLAGS.NORMAL,

        /// <summary>
        /// Normally the playback will release any created resources when it stops, unless this flag is set.
        /// </summary>
        SkipCleanup = COMMANDREPLAY_FLAGS.SKIP_CLEANUP
    }
}
