using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodTruncatedException
        : FmodException
    {
        public FmodTruncatedException()
            : base(RESULT.ERR_TRUNCATED)
        {
            
        }
    }
}
