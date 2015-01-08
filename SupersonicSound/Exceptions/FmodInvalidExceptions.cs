using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodInvalidFloatException
        : FmodException
    {
        public FmodInvalidFloatException()
            : base(RESULT.ERR_INVALID_FLOAT)
        {
        }
    }

    public class FmodInvalidHandleException
        : FmodException
    {
        public FmodInvalidHandleException()
            : base(RESULT.ERR_INVALID_HANDLE)
        {
        }
    }

    public class FmodInvalidParamException
        : FmodException
    {
        public FmodInvalidParamException()
            : base(RESULT.ERR_INVALID_PARAM)
        {
        }
    }

    public class FmodInvalidPositionException
        : FmodException
    {
        public FmodInvalidPositionException()
            : base(RESULT.ERR_INVALID_POSITION)
        {
        }
    }

    public class FmodInvalidSpeakerException
        : FmodException
    {
        public FmodInvalidSpeakerException()
            : base(RESULT.ERR_INVALID_SPEAKER)
        {
        }
    }

    public class FmodInvalidSyncPointException
        : FmodException
    {
        public FmodInvalidSyncPointException()
            : base(RESULT.ERR_INVALID_SYNCPOINT)
        {
        }
    }

    public class FmodInvalidThreadException
        : FmodException
    {
        public FmodInvalidThreadException()
            : base(RESULT.ERR_INVALID_THREAD)
        {
        }
    }

    public class FmodInvalidVectorException
        : FmodException
    {
        public FmodInvalidVectorException()
            : base(RESULT.ERR_INVALID_VECTOR)
        {
        }
    }

    public class FmodInvalidStringException
        : FmodException
    {
        public FmodInvalidStringException()
            : base(RESULT.ERR_INVALID_STRING)
        {
        }
    }
}
