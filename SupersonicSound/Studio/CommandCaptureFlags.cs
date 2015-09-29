using FMOD.Studio;

namespace SupersonicSound.Studio
{
    [EquivalentEnum(typeof(COMMANDCAPTURE_FLAGS))]
    public enum CommandCaptureFlags
        : uint
    {
        /// <summary>
        /// Standard behaviour.
        /// </summary>
        Normal = COMMANDCAPTURE_FLAGS.NORMAL,
        
        /// <summary>
        /// Call file flush on every command.
        /// </summary>
        FileFlush = COMMANDCAPTURE_FLAGS.FILEFLUSH,

        /// <summary>
        /// Normally the initial state of banks and instances is captured, unless this flag is set.
        /// </summary>
        SkipInitialState = COMMANDCAPTURE_FLAGS.SKIP_INITIAL_STATE
    }
}
