using System;
using System.Linq;

namespace SupersonicSound.Wrapper
{
    public static class Util
    {
        public static bool IsUnix
        {
            get
            {
                // Based on this SO article/Mono FAQ:
                // http://stackoverflow.com/questions/5116977/how-to-check-the-os-version-at-runtime-e-g-windows-or-linux-without-using-a-con

                int p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }
    }
}
