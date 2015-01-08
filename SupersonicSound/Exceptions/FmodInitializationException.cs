using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodInitializationException
        : FmodException
    {
        public FmodInitializationException()
            : base(RESULT.ERR_INITIALIZATION)
        {
            
        }
    }
}
