using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodOutputAllocatedException
        : FmodException
    {
        public FmodOutputAllocatedException()
            : base(RESULT.ERR_OUTPUT_ALLOCATED)
        {
        }
    }

    public class FmodOutputCreateBufferException
        : FmodException
    {
        public FmodOutputCreateBufferException()
            : base(RESULT.ERR_OUTPUT_CREATEBUFFER)
        {
        }
    }

    public class FmodOutputDriverCallException
        : FmodException
    {
        public FmodOutputDriverCallException()
            : base(RESULT.ERR_OUTPUT_DRIVERCALL)
        {
        }
    }

    public class FmodOutputFormatException
        : FmodException
    {
        public FmodOutputFormatException()
            : base(RESULT.ERR_OUTPUT_FORMAT)
        {
        }
    }

    public class FmodOutputInitException
        : FmodException
    {
        public FmodOutputInitException()
            : base(RESULT.ERR_OUTPUT_INIT)
        {
        }
    }

    public class FmodOutputNoDriversException
        : FmodException
    {
        public FmodOutputNoDriversException()
            : base(RESULT.ERR_OUTPUT_NODRIVERS)
        {
        }
    }
}
