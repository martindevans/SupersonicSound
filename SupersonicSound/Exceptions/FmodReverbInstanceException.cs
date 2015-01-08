using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodReverbInstanceException
        : FmodException
    {
        public FmodReverbInstanceException()
            : base(RESULT.ERR_REVERB_INSTANCE)
        {
        }
    }
}
