using System;

namespace SupersonicSound
{
    public class FmodException
        : Exception
    {
        public FmodException(string message)
            :base(message)
        {
        }
    }
}
