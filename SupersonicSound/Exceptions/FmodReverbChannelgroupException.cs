using FMOD;

namespace SupersonicSound.Exceptions
{
    /// <summary>
    /// Reverb properties cannot be set on this channel because a parent channelgroup owns the reverb connection.
    /// </summary>
    public class FmodReverbChannelgroupException
        : FmodException
    {
        public FmodReverbChannelgroupException()
            : base(RESULT.ERR_REVERB_CHANNELGROUP)
        {
        }
    }
}
