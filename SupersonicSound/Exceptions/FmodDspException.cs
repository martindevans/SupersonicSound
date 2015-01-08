using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodDspException
        : FmodException
    {
        public FmodDspException(RESULT fmodError)
            : base(fmodError)
        {
        }
    }

    /// <summary>
    /// DSP connection error.  Connection possibly caused a cyclic dependency or connected dsps with incompatible buffer counts.
    /// </summary>
    public class FmodDspConnectionException
        : FmodDspException
    {
        public FmodDspConnectionException()
            : base(RESULT.ERR_DSP_CONNECTION)
        {

        }
    }

    /// <summary>
    /// DSP Format error.  A DSP unit may have attempted to connect to this network with the wrong format, or a matrix may have been set with the wrong size if the target unit has a specified channel map.
    /// </summary>
    public class FmodDspInUseException
        : FmodDspException
    {
        public FmodDspInUseException()
            : base(RESULT.ERR_DSP_FORMAT)
        {

        }
    }

    public class FmodDspFormatException
        : FmodDspException
    {
        public FmodDspFormatException()
            : base(RESULT.ERR_DSP_INUSE)
        {

        }
    }

    public class FmodDspNotFoundException
        : FmodDspException
    {
        public FmodDspNotFoundException()
            : base(RESULT.ERR_DSP_NOTFOUND)
        {

        }
    }

    public class FmodDspReservedException
        : FmodDspException
    {
        public FmodDspReservedException()
            : base(RESULT.ERR_DSP_RESERVED)
        {
            
        }
    }

    public class FmodDspSilenceException
        : FmodDspException
    {
        public FmodDspSilenceException()
            : base(RESULT.ERR_DSP_SILENCE)
        {

        }
    }

    public class FmodDspTypeException
        : FmodDspException
    {
        public FmodDspTypeException()
            : base(RESULT.ERR_DSP_TYPE)
        {

        }
    }
}
