using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodNeeds3DException
        : FmodException
    {
        public FmodNeeds3DException()
            : base(RESULT.ERR_NEEDS3D)
        {
            
        }
    }
}
