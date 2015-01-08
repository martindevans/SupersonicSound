using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodNotReadyException
        : FmodException
    {
        public FmodNotReadyException()
            : base(RESULT.ERR_NOTREADY)
        {
            
        }
    }
}
