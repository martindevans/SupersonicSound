using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodNotLockedException
        : FmodException
    {
        public FmodNotLockedException()
            : base(RESULT.ERR_NOT_LOCKED)
        {
            
        }
    }
}
