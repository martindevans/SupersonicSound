using FMOD;
using System.Numerics;

namespace SupersonicSound.Extensions
{
    internal static class Vector3Extensions
    {
        public static VECTOR ToFmod(this Vector3 vector)
        {
            return new VECTOR {
                x = vector.X,
                y = vector.Y,
                z = vector.Z
            };
        }

        public static Vector3 FromFmod(this VECTOR vector)
        {
            return new Vector3(
                vector.x,
                vector.y,
                vector.z
            );
        }
    }
}
