using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodStudioNotLoadedException
        : FmodException
    {
        public FmodStudioNotLoadedException()
            : base(RESULT.ERR_STUDIO_NOT_LOADED)
        {
            
        }
    }
}
