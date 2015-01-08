using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodMemoryCannotPointException
        : FmodException
    {
        public FmodMemoryCannotPointException()
            : base(RESULT.ERR_MEMORY_CANTPOINT)
        {
            
        }
    }
}
