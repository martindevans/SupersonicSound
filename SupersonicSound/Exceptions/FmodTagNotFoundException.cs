using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodTagNotFoundException
        : FmodException
    {
        public FmodTagNotFoundException()
            : base(RESULT.ERR_TAGNOTFOUND)
        {
            
        }
    }
}
