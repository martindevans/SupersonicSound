using System;
using FMOD;
using SupersonicSound.Wrapper;

namespace SupersonicSound.Studio
{
    public class VCA
        : IEquatable<VCA>
    {
        public FMOD.Studio.VCA FmodVCA { get; private set; }

        private VCA(FMOD.Studio.VCA vca)
        {
            FmodVCA = vca;
        }

        public static VCA FromFmod(FMOD.Studio.VCA vca)
        {
            if (vca == null)
                return null;
            return new VCA(vca);
        }

        #region equality
        public bool Equals(VCA other)
        {
            if (other == null)
                return false;

            return other.FmodVCA == FmodVCA;
        }

        public override bool Equals(object obj)
        {
            var c = obj as VCA;
            if (c == null)
                return false;

            return Equals(c);
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
            if (vca == null)
                return null;

            return vca.FmodVCA;
        }
    }
}
