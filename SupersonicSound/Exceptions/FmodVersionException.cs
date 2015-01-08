using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodVersionException
        : FmodException
    {
        public FmodVersionException()
            : base(RESULT.ERR_VERSION)
        {
        }
    }
}
