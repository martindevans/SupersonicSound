using FMOD;

namespace SupersonicSound.Exceptions
{
    public abstract class FmodNetException
        : FmodException
    {
        protected FmodNetException(RESULT fmodError)
            : base(fmodError)
        {
        }
    }

    public class FmodNetConnectException
        : FmodNetException
    {
        public FmodNetConnectException()
            : base(RESULT.ERR_NET_CONNECT)
        {
        }
    }

    public class FmodNetSocketException
        : FmodNetException
    {
        public FmodNetSocketException()
            : base(RESULT.ERR_NET_SOCKET_ERROR)
        {
        }
    }

    public class FmodNetUrlException
        : FmodNetException
    {
        public FmodNetUrlException()
            : base(RESULT.ERR_NET_URL)
        {
        }
    }

    public class FmodNetWouldBlockException
        : FmodNetException
    {
        public FmodNetWouldBlockException()
            : base(RESULT.ERR_NET_WOULD_BLOCK)
        {
        }
    }
}
