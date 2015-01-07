using FMOD;

namespace SupersonicSound.LowLevel
{
    [EquivalentEnum(typeof(OPENSTATE), "MAX")]
    public enum OpenState
    {
        /// <summary>
        /// Opened and ready to play
        /// </summary>
        Ready = OPENSTATE.READY,

        /// <summary>
        /// Initial load in progress
        /// </summary>
        Loading = OPENSTATE.LOADING,

        /// <summary>
        /// Failed to open - file not found, out of memory etc.  See return value of Sound::getOpenState for what happened.
        /// </summary>
        Error = OPENSTATE.ERROR,

        /// <summary>
        /// Connecting to remote host (internet sounds only)
        /// </summary>
        Connecting = OPENSTATE.CONNECTING,

        /// <summary>
        /// Buffering data
        /// </summary>
        Buffering = OPENSTATE.BUFFERING,

        /// <summary>
        /// Seeking to subsound and re-flushing stream buffer.
        /// </summary>
        Seeking = OPENSTATE.SEEKING,

        /// <summary>
        /// Ready and playing, but not possible to release at this time without stalling the main thread.
        /// </summary>
        Playing = OPENSTATE.PLAYING,

        /// <summary>
        /// Seeking within a stream to a different position.
        /// </summary>
        SetPosition = OPENSTATE.SETPOSITION,
    }
}
