using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodInitializedException
        : FmodException
    {
        public FmodInitializedException()
            : base(RESULT.ERR_INITIALIZED)
        {
            
        }
    }
}
