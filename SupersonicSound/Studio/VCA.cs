using System;
using FMOD;
using SupersonicSound.Wrapper;

namespace SupersonicSound.Studio
{
    public struct VCA
        : IEquatable<VCA>
    {
        public FMOD.Studio.VCA FmodVca { get; private set; }

        private VCA(FMOD.Studio.VCA vca)
            : this()
        {
            FmodVca = vca;
        }

        public static VCA FromFmod(FMOD.Studio.VCA vca)
        {
            if (vca == null)
                throw new ArgumentNullException("vca");
            return new VCA(vca);
        }

        #region equality
        public bool Equals(VCA other)
        {
            return other.FmodVca == FmodVca;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is VCA))
                return false;

            return Equals((VCA)obj);
        }

        public override int GetHashCode()
        {
            return (FmodVca != null ? FmodVca.GetHashCode() : 0);
        }
        #endregion

        public Guid Id
        {
            get
            {
                Guid id;
                FmodVca.getID(out id).Check();
                return id;
            }
        }

        public string Path
        {
            get
            {
                string path;
                FmodVca.getPath(out path).Check();
                return path;
            }
        }

        public float FaderLevel
        {
            get
            {
                float volume;
                FmodVca.getFaderLevel(out volume).Check();
                return volume;
            }
            set
            {
                FmodVca.setFaderLevel(value).Check();
            }
        }
    }
}
