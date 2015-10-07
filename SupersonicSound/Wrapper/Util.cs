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

        internal static TIn CheckEquivalentCast<TIn, TOut>(this TIn input)
            where TIn : struct
            where TOut : struct
        {
#if DEBUG
            //Basic sanity check, are we working with enums?
            if (!typeof(TIn).IsEnum)
                throw new Exception("CastEquivalentEnum input type must be an enum (programmer error)");
            if (!typeof(TOut).IsEnum)
                throw new Exception("CastEquivalentEnum output type must be an enum (programmer error)");

            //Is the input an equivalent enum?
            var equiv = typeof(TIn).GetCustomAttributes(typeof(EquivalentEnumAttribute), true).Cast<EquivalentEnumAttribute>().SingleOrDefault();
            if (equiv == null)
                throw new Exception("CastEquivalentEnum input type must have EquivalentEnum Attribute (programmer error)");

            //Is the output the correct type (as defined by equivalence)
            if (!equiv.Equivalent.IsAssignableFrom(typeof(TOut)))
                throw new Exception("CastEquivalentEnum output type must be the equivalent enum of input type (programmer error)");

            //consider checking if all the values in input are valid values of output - this is complex due to flags enums mixing stuff up
#endif

            return input;
        }
    }
}
