using FMOD.Studio;
using SupersonicSound.Wrapper;
using System;

namespace SupersonicSound.Studio
{
    public class ParameterInstance
        : IEquatable<ParameterInstance>
    {
        public FMOD.Studio.ParameterInstance FmodParameterInstance { get; private set; }

        private ParameterInstance(FMOD.Studio.ParameterInstance parameterInstance)
        {
            FmodParameterInstance = parameterInstance;
        }

        public static ParameterInstance FromFmod(FMOD.Studio.ParameterInstance parameterInstance)
        {
            if (parameterInstance == null)
                return null;
            return new ParameterInstance(parameterInstance);
        }

        #region equality
        public bool Equals(ParameterInstance other)
        {
            if (other == null)
                return false;

            return other.FmodParameterInstance == FmodParameterInstance;
        }

        public override bool Equals(object obj)
        {
            var c = obj as ParameterInstance;
            if (c == null)
                return false;

            return Equals(c);
        }

        public override int GetHashCode()
        {
            return (FmodParameterInstance != null ? FmodParameterInstance.GetHashCode() : 0);
        }
        #endregion

        public ParameterDescription Description
        {
            get
            {
                PARAMETER_DESCRIPTION desc;
                FmodParameterInstance.getDescription(out desc).Check();
                return new ParameterDescription(desc);
            }
        }

        public float Value
        {
            get
            {
                float val;
                FmodParameterInstance.getValue(out val).Check();
                return val;
            }
            set
            {
                FmodParameterInstance.setValue(value).Check();
            }
        }
    }

    public static class ParameterInstanceExtensions
    {
        public static FMOD.Studio.ParameterInstance ToFmod(this ParameterInstance bank)
        {
            if (bank == null)
                return null;

            return bank.FmodParameterInstance;
        }
    }
}
