
using FMOD;

namespace SupersonicSound.LowLevel
{
    public struct Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3(float x, float y, float z)
            : this()
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(VECTOR vector)
            : this(vector.x, vector.y, vector.z)
        {
        }

        public VECTOR ToFmod()
        {
            return new VECTOR {
                x = X,
                y = Y,
                z = Z
            };
        }
    }
}
