using FMOD;

namespace SupersonicSound.Exceptions
{
    /// <summary>
    /// An error occurred trying to initialize the recording device.
    /// </summary>
    public class FmodRecordException
        : FmodException
    {
        public FmodRecordException()
            : base(RESULT.ERR_RECORD)
        {
        }
    }
}
