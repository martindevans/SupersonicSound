using System;
using FMOD;

namespace SupersonicSound
{
    public class FmodException
        : Exception
    {
        public RESULT FMODError { get; private set; }

        public FmodException(RESULT fmodError)
            :base(Error.String(fmodError))
        {
            FMODError = fmodError;
        }
    }
}
