using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodMaxAudibleException
        : FmodException
    {
        public FmodMaxAudibleException()
            : base(RESULT.ERR_MAXAUDIBLE)
        {
            
        }
    }
}
