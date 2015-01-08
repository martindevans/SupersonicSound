using FMOD;

namespace SupersonicSound.Exceptions
{
    public class BaseFmodInvalidException
        : FmodException
    {
        public BaseFmodInvalidException(RESULT fmodError)
            : base(fmodError)
        {
        }
    }

    public class FmodInvalidFloatException
        : BaseFmodInvalidException
    {
        public FmodInvalidFloatException()
            : base(RESULT.ERR_INVALID_FLOAT)
        {
        }
    }

    public class FmodInvalidHandleException
        : BaseFmodInvalidException
    {
        public FmodInvalidHandleException()
            : base(RESULT.ERR_INVALID_HANDLE)
        {
        }
    }

    public class FmodInvalidParamException
        : BaseFmodInvalidException
    {
        public FmodInvalidParamException()
            : base(RESULT.ERR_INVALID_PARAM)
        {
        }
    }

    public class FmodInvalidPositionException
        : BaseFmodInvalidException
    {
        public FmodInvalidPositionException()
            : base(RESULT.ERR_INVALID_POSITION)
        {
        }
    }

    public class FmodInvalidSpeakerException
        : BaseFmodInvalidException
    {
        public FmodInvalidSpeakerException()
            : base(RESULT.ERR_INVALID_SPEAKER)
        {
        }
    }

    public class FmodInvalidSyncPointException
        : BaseFmodInvalidException
    {
        public FmodInvalidSyncPointException()
            : base(RESULT.ERR_INVALID_SYNCPOINT)
        {
        }
    }

    public class FmodInvalidThreadException
        : BaseFmodInvalidException
    {
        public FmodInvalidThreadException()
            : base(RESULT.ERR_INVALID_THREAD)
        {
        }
    }

    public class FmodInvalidVectorException
        : BaseFmodInvalidException
    {
        public FmodInvalidVectorException()
            : base(RESULT.ERR_INVALID_VECTOR)
        {
        }
    }

    public class FmodInvalidStringException
        : BaseFmodInvalidException
    {
        public FmodInvalidStringException()
            : base(RESULT.ERR_INVALID_STRING)
        {
        }
    }
}
