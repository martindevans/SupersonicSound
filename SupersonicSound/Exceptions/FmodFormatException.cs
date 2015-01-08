using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodFormatException
        : FmodException
    {
        public FmodFormatException()
            : base(RESULT.ERR_FORMAT)
        {
            
        }
    }
}
