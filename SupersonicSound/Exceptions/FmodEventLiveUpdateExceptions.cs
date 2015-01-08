using FMOD;

namespace SupersonicSound.Exceptions
{
    /// <summary>
    /// The requested event, bus or vca could not be found.
    /// </summary>
    public class FmodEventNotFoundException
        : FmodException
    {
        public FmodEventNotFoundException()
            : base(RESULT.ERR_EVENT_NOTFOUND)
        {
        }
    }

    public class FmodEventLiveUpdateBusyException
        : FmodException
    {
        public FmodEventLiveUpdateBusyException()
            : base(RESULT.ERR_EVENT_LIVEUPDATE_BUSY)
        {
        }
    }

    public class FmodEventLiveUpdateMismatchException
        : FmodException
    {
        public FmodEventLiveUpdateMismatchException()
            : base(RESULT.ERR_EVENT_LIVEUPDATE_MISMATCH)
        {
        }
    }

    public class FmodEventLiveUpdateTimeoutException
        : FmodException
    {
        public FmodEventLiveUpdateTimeoutException()
            : base(RESULT.ERR_EVENT_LIVEUPDATE_TIMEOUT)
        {
        }
    }

    public class FmodEventAlreadyLoadedException
        : FmodException
    {
        public FmodEventAlreadyLoadedException()
            : base(RESULT.ERR_EVENT_ALREADY_LOADED)
        {
        }
    }
}
