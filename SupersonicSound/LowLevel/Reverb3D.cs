using System.Numerics;
using FMOD;
using SupersonicSound.Extensions;
using SupersonicSound.Wrapper;
using System;
using System.Collections.Generic;

namespace SupersonicSound.LowLevel
{
    public struct Reverb3D
        //: IHandle
    {
        public FMOD.Reverb3D FmodReverb { get; }

        private bool _throwHandle;
        public bool SuppressInvalidHandle
        {
            get { return !_throwHandle; }
            set { _throwHandle = !value; }
        }

        private Reverb3D(FMOD.Reverb3D reverb)
            : this()
        {
            FmodReverb = reverb;
        }

        public static Reverb3D FromFmod(FMOD.Reverb3D reverb)
        {
            if (reverb == null)
                throw new ArgumentNullException(nameof(reverb));
            return new Reverb3D(reverb);
        }

        private IReadOnlyList<RESULT> Suppressions()
        {
            return ErrorChecking.Suppress(_throwHandle, true);
        }

        //public bool IsValid()
        //{
        //    return FmodReverb.isValid();
        //}

        public Vector3? Position
        {
            get
            {
                var attr = Get3DAttributes();
                return attr?.Position;
            }
            set
            {
                var attr = Get3DAttributes();
                if (!attr.HasValue)
                    return;

                Set3DAttributes(value.Unbox(), attr.Min, attr.Max);
            }
        }

        public float? MinimumDistance
        {
            get
            {
                var attr = Get3DAttributes();
                return attr?.Min;
            }
            set
            {
                var attr = Get3DAttributes();
                if (!attr.HasValue)
                    return;

                Set3DAttributes(new Attributes(attr.Value.Position, value.Unbox(), attr.Value.Max));
            }
        }

        public float? MaximumDistance
        {
            get
            {
                var attr = Get3DAttributes();
                return attr?.Max;
            }
            set
            {
                var attr = Get3DAttributes();
                if (!attr.HasValue)
                    return;

                Set3DAttributes(new Attributes(attr.Value.Position, attr.Value.Min, value.Unbox()));
            }
        }

        public struct Attributes
        {
            public readonly Vector3 Position;
            public readonly float Min;
            public readonly float Max;

            public Attributes(Vector3 pos, float min, float max)
                : this()
            {
                Position = pos;
                Min = min;
                Max = max;
            }
        }

        public Attributes? Get3DAttributes()
        {
            VECTOR pos = new VECTOR();
            float min = 0, max = 0;
            if (!FmodReverb.get3DAttributes(ref pos, ref min, ref max).Check(Suppressions()))
                return null;

            return new Attributes(pos.FromFmod(), min, max);
        }

        public bool Set3DAttributes(Attributes attr)
        {
            var pos = attr.Position.ToFmod();
            return FmodReverb.set3DAttributes(ref pos, attr.Min, attr.Max).Check(Suppressions());
        }

        public ReverbProperties? Properties
        {
            get
            {
                var props = new REVERB_PROPERTIES();
                if (!FmodReverb.getProperties(ref props).Check(Suppressions()))
                    return null;
                return new ReverbProperties(ref props);
            }
            set
            {
                var prop = value.Unbox().ToFmod();
                FmodReverb.setProperties(ref prop).Check(Suppressions());
            }
        }

        public bool? Active
        {
            get
            {
                bool active;
                if (!FmodReverb.getActive(out active).Check(Suppressions()))
                    return null;
                return active;
            }
            set
            {
                FmodReverb.setActive(value.Unbox()).Check(Suppressions());
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
