using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodStudioUninitializedException
        : FmodException
    {
        public FmodStudioUninitializedException()
            : base(RESULT.ERR_STUDIO_UNINITIALIZED)
        {
            
        }
    }
}
