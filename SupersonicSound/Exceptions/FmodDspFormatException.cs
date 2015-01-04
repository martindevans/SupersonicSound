using FMOD;

namespace SupersonicSound.Exceptions
{
    /// <summary>
    /// DSP Format error.  A DSP unit may have attempted to connect to this network with the wrong format, or a matrix may have been set with the wrong size if the target unit has a specified channel map.
    /// </summary>
    public class FmodDspFormatException
        : FmodException
    {
        public FmodDspFormatException()
            : base(Error.String(RESULT.ERR_DSP_FORMAT))
        {
            
        }
    }
}
