using FMOD;

namespace SupersonicSound.Exceptions
{
    /// <summary>
    /// The requested event, bus or vca could not be found.
    /// </summary>
    public class FmodEventNotFoundException
        : FmodException
    {
        public FmodEventNotFoundException()
            : base(Error.String(RESULT.ERR_EVENT_NOTFOUND))
        {
        }
    }
}
