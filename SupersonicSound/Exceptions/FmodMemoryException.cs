using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodMemoryException
        : FmodException
    {
        public FmodMemoryException()
            : base(RESULT.ERR_MEMORY)
        {
        }
    }
}
