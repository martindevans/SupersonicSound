using FMOD;

namespace SupersonicSound.Exceptions
{
    /// <summary>
    /// Tried to call a function on a data type that does not allow this type of functionality (ie calling Sound::lock on a streaming sound).
    /// </summary>
    public class FmodBadCommandException
        : FmodException
    {
        public FmodBadCommandException()
            : base(Error.String(RESULT.ERR_BADCOMMAND))
        {
            
        }
    }
}
