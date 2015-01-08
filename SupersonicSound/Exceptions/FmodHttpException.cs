using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodHttpException
        : FmodException
    {
        public FmodHttpException()
            : base(RESULT.ERR_HTTP)
        {
            
        }
    }

    public class FmodHttpAccessException
        : FmodException
    {
        public FmodHttpAccessException()
            : base(RESULT.ERR_HTTP_ACCESS)
        {

        }
    }

    public class FmodHttpProxyAuthException
        : FmodException
    {
        public FmodHttpProxyAuthException()
            : base(RESULT.ERR_HTTP_PROXY_AUTH)
        {

        }
    }

    public class FmodHttpServerErrorException
        : FmodException
    {
        public FmodHttpServerErrorException()
            : base(RESULT.ERR_HTTP_SERVER_ERROR)
        {

        }
    }

    public class FmodHttpTimeoutException
        : FmodException
    {
        public FmodHttpTimeoutException()
            : base(RESULT.ERR_HTTP_TIMEOUT)
        {

        }
    }
}
