using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodHeaderMismatchException
        : FmodException
    {
        public FmodHeaderMismatchException()
            : base(RESULT.ERR_HEADER_MISMATCH)
        {
            
        }
    }
}
