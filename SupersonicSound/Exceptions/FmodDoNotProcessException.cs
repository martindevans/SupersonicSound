using FMOD;

namespace SupersonicSound.Exceptions
{
    /// <summary>
    /// DSP return code from a DSP process query callback.  Tells mixer not to call the process callback and therefore not consume CPU.  Use this to optimize the DSP graph.
    /// </summary>
    public class FmodDoNotProcessException
        : FmodException
    {
        public FmodDoNotProcessException()
            : base(RESULT.ERR_DSP_DONTPROCESS)
        {
        }
    }
}
