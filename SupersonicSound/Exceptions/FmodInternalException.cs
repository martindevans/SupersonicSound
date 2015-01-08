using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodInternalException
        : FmodException
    {
        public FmodInternalException()
            : base(RESULT.ERR_INTERNAL)
        {
            
        }
    }
}
