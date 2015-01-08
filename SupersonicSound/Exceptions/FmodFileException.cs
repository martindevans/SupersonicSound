using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodFileException
        : FmodException
    {
        public FmodFileException(RESULT fmodError)
            : base(fmodError)
        {
        }
    }
}
