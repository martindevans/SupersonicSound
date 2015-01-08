using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodAlreadyLockedException
        : FmodException
    {
        public FmodAlreadyLockedException()
            : base(RESULT.ERR_ALREADY_LOCKED)
        {
            
        }
    }
}
