using FMOD.Studio;

namespace SupersonicSound.Studio
{
    [EquivalentEnum(typeof(RECORD_COMMANDS_FLAGS))]
    public enum RecordCommandsFlags
        : uint
    {
        /// <summary>
        /// Standard behaviour.
        /// </summary>
        Normal = RECORD_COMMANDS_FLAGS.NORMAL,
        
        /// <summary>
        /// Call file flush on every command.
        /// </summary>
        FileFlush = RECORD_COMMANDS_FLAGS.FILEFLUSH
    }
}
