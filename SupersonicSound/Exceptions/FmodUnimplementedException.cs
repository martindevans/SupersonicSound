using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodUnimplementedException
        : FmodException
    {
        public FmodUnimplementedException()
            : base(RESULT.ERR_UNIMPLEMENTED)
        {
        }
    }
}
