
using FMOD.Studio;

namespace SupersonicSound.LowLevel
{
    public struct Attributes3D
    {
        public Vector3 Position { get; private set; }
        public Vector3 Velocity { get; private set; }
        public Vector3 Forward { get; private set; }
        public Vector3 Up { get; private set; }

        public Attributes3D(Vector3 pos, Vector3 vel, Vector3 forward, Vector3 up)
            : this()
        {
            Position = pos;
            Velocity = vel;
            Forward = forward;
            Up = up;
        }

        public Attributes3D(_3D_ATTRIBUTES attr)
            : this(new Vector3(attr.position), new Vector3(attr.velocity), new Vector3(attr.forward), new Vector3(attr.up))
        {
        }

        public _3D_ATTRIBUTES ToFmod()
        {
            return new _3D_ATTRIBUTES {
                position = Position.ToFmod(),
                velocity = Velocity.ToFmod(),
                forward = Forward.ToFmod(),
                up = Up.ToFmod()
            };
        }
    }
}
