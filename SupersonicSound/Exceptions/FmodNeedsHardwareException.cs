using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodNeedsHardwareException
        : FmodException
    {
        public FmodNeedsHardwareException()
            : base(RESULT.ERR_NEEDSHARDWARE)
        {
            
        }
    }
}
