using FMOD;

namespace SupersonicSound.Exceptions
{
    /// <summary>
    /// The specified channel has been reused to play another sound.
    /// </summary>
    public class FmodChannelStolenException
        : FmodException
    {
        public FmodChannelStolenException()
            : base(RESULT.ERR_CHANNEL_STOLEN)
        {
            
        }
    }
}
