using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodUnsupportedException
        : FmodException
    {
        public FmodUnsupportedException()
            : base(RESULT.ERR_UNSUPPORTED)
        {
        }
    }
}
