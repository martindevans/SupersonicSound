using System;
using FMOD;
using SupersonicSound.Wrapper;

namespace SupersonicSound.Studio
{
    public struct VCA
        : IEquatable<VCA>
    {
        public FMOD.Studio.VCA FmodVCA { get; private set; }

        private VCA(FMOD.Studio.VCA vca)
            : this()
        {
            FmodVCA = vca;
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
            return other.FmodVCA == FmodVCA;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is VCA))
                return false;

            return Equals((VCA)obj);
        }

        public override int GetHashCode()
        {
            return (FmodVCA != null ? FmodVCA.GetHashCode() : 0);
        }
        #endregion

        public Guid Id
        {
            get
            {
                GUID id;
                FmodVCA.getID(out id).Check();
                return id.FromFmod();
            }
        }

        public string Path
        {
            get
            {
                string path;
                FmodVCA.getPath(out path).Check();
                return path;
            }
        }

        public float FaderLevel
        {
            get
            {
                float volume;
                FmodVCA.getFaderLevel(out volume).Check();
                return volume;
            }
            set
            {
                FmodVCA.setFaderLevel(value).Check();
            }
        }
    }

    public static class VCAExtensions
    {
        public static FMOD.Studio.VCA ToFmod(this VCA vca)
        {
            return vca.FmodVCA;
        }
    }
}
