using FMOD;

namespace SupersonicSound.Exceptions
{
    /// <summary>
    /// DSP connection error.  Connection possibly caused a cyclic dependency or connected dsps with incompatible buffer counts.
    /// </summary>
    public class FmodDspConnectionException
        : FmodException
    {
        public FmodDspConnectionException()
            : base(Error.String(RESULT.ERR_DSP_CONNECTION))
        {
            
        }
    }
}
