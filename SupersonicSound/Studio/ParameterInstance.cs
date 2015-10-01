using FMOD.Studio;
using SupersonicSound.LowLevel;
using SupersonicSound.Wrapper;
using System;

namespace SupersonicSound.Studio
{
    public struct ParameterInstance
        : IEquatable<ParameterInstance>, IHandle
    {
        public FMOD.Studio.ParameterInstance FmodParameterInstance { get; private set; }

        private ParameterInstance(FMOD.Studio.ParameterInstance parameterInstance)
            : this()
        {
            FmodParameterInstance = parameterInstance;
        }

        public static ParameterInstance FromFmod(FMOD.Studio.ParameterInstance parameterInstance)
        {
            if (parameterInstance == null)
                throw new ArgumentNullException("parameterInstance");
            return new ParameterInstance(parameterInstance);
        }

        public bool IsValid()
        {
            return FmodParameterInstance.isValid();
        }

        #region equality
        public bool Equals(ParameterInstance other)
        {
            return other.FmodParameterInstance == FmodParameterInstance;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ParameterInstance))
                return false;

            return Equals((ParameterInstance)obj);
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
}
