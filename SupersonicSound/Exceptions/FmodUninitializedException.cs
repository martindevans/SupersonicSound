using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodUninitializedException
        : FmodException
    {
        public FmodUninitializedException()
            : base(RESULT.ERR_UNINITIALIZED)
        {
        }
    }
}
