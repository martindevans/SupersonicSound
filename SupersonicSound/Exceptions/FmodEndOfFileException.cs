using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodEndOfFileException
        : FmodException
    {
        public FmodEndOfFileException()
            : base(RESULT.ERR_FILE_EOF)
        {
        }
    }
}
