using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodSubsoundsException
        : FmodException
    {
        public FmodSubsoundsException()
            : base(RESULT.ERR_SUBSOUNDS)
        {
            
        }
    }

    public class FmodSubsoundAllocatedException
        : FmodException
    {
        public FmodSubsoundAllocatedException()
            : base(RESULT.ERR_SUBSOUND_ALLOCATED)
        {

        }
    }

    public class FmodSubsoundCannotMoveException
        : FmodException
    {
        public FmodSubsoundCannotMoveException()
            : base(RESULT.ERR_SUBSOUND_CANTMOVE)
        {

        }
    }
}
