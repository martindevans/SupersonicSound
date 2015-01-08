using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodTooManyChannelsException
        : FmodException
    {
        public FmodTooManyChannelsException()
            : base(RESULT.ERR_TOOMANYCHANNELS)
        {
            
        }
    }
}
