using FMOD;
using SupersonicSound.Wrapper;
using System;

namespace SupersonicSound.LowLevel
{
    public struct Reverb3D
    {
        public FMOD.Reverb3D FmodReverb { get; private set; }

        public Reverb3D(FMOD.Reverb3D reverb)
            : this()
        {
            FmodReverb = reverb;
        }

        public Vector3 Position
        {
            get
            {
                Vector3 position;
                float min;
                float max;
                Get3DAttributes(out position, out min, out max);
                return position;
            }
            set
            {
                Vector3 position;
                float min;
                float max;
                Get3DAttributes(out position, out min, out max);

                Set3DAttributes(value, min, max);
            }
        }

        public float MinimumDistance
        {
            get
            {
                Vector3 position;
                float min;
                float max;
                Get3DAttributes(out position, out min, out max);
                return min;
            }
            set
            {
                Vector3 position;
                float min;
                float max;
                Get3DAttributes(out position, out min, out max);

                Set3DAttributes(position, value, max);
            }
        }

        public float MaximumDistance
        {
            get
            {
                Vector3 position;
                float min;
                float max;
                Get3DAttributes(out position, out min, out max);
                return max;
            }
            set
            {
                Vector3 position;
                float min;
                float max;
                Get3DAttributes(out position, out min, out max);

                Set3DAttributes(position, min, value);
            }
        }

        public void Get3DAttributes(out Vector3 position, out float minDistance, out float maxDistance)
        {
            VECTOR pos = new VECTOR();
            float min = 0, max = 0;
            FmodReverb.get3DAttributes(ref pos, ref min, ref max).Check();

            position = new Vector3(pos);
            minDistance = min;
            maxDistance = max;
        }

        public void Set3DAttributes(Vector3 position, float minDistance, float maxDistance)
        {
            VECTOR pos = position.ToFmod();
            FmodReverb.set3DAttributes(ref pos, minDistance, maxDistance).Check();
        }

        public ReverbProperties Properties
        {
            get
            {
                REVERB_PROPERTIES props = new REVERB_PROPERTIES();
                FmodReverb.getProperties(ref props).Check();
                return new ReverbProperties(ref props);
            }
            set
            {
                REVERB_PROPERTIES prop = value.ToFmod();
                FmodReverb.setProperties(ref prop).Check();
            }
        }

        public bool Active
        {
            get
            {
                bool active;
                FmodReverb.getActive(out active).Check();
                return active;
            }
            set
            {
                FmodReverb.setActive(value).Check();
            }
        }

        #region Userdata set/get
        public IntPtr UserData
        {
            get
            {
                IntPtr ptr;
                FmodReverb.getUserData(out ptr).Check();
                return ptr;
            }
            set
            {
                FmodReverb.setUserData(value).Check();
            }
        }
        #endregion
    }
}
