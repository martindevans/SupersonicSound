using System;

namespace SupersonicSound.Studio
{
    public struct CueInstance
        : IEquatable<CueInstance>
    {
        public FMOD.Studio.CueInstance FmodCueInstance { get; private set; }

        private CueInstance(FMOD.Studio.CueInstance cue)
            : this()
        {
            FmodCueInstance = cue;
        }

        public static CueInstance FromFmod(FMOD.Studio.CueInstance cue)
        {
            if (cue == null)
                throw new ArgumentNullException("cue");

            return new CueInstance(cue);
        }

        #region equality

        public bool Equals(CueInstance other)
        {
            return other.FmodCueInstance == FmodCueInstance;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CueInstance))
                return false;

            return Equals((CueInstance) obj);
        }

        public override int GetHashCode()
        {
            return (FmodCueInstance != null ? FmodCueInstance.GetHashCode() : 0);
        }

        #endregion

        public void Trigger()
        {
            FmodCueInstance.trigger();
        }
    }

    public static class CueInstanceExtensions
    {
        public static FMOD.Studio.CueInstance ToFmod(this CueInstance cue)
        {
            return cue.FmodCueInstance;
        }
    }
}
