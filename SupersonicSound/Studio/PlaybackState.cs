using FMOD.Studio;

namespace SupersonicSound.Studio
{
    [EquivalentEnum(typeof(PLAYBACK_STATE))]
    public enum PlaybackState
    {
        Playing = PLAYBACK_STATE.PLAYING,
        Sustaining = PLAYBACK_STATE.SUSTAINING,
        Stopped = PLAYBACK_STATE.STOPPED,
        Starting = PLAYBACK_STATE.STARTING,
        Stopping = PLAYBACK_STATE.STOPPING
    }
}
