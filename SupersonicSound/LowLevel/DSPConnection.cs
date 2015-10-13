using System;
using System.Collections.Generic;
using FMOD;
using SupersonicSound.Wrapper;

namespace SupersonicSound.LowLevel
{
    public struct DspConnection
        : IEquatable<DspConnection>//, IHandle
    {
        public DSPConnection FmodDspConnection { get; }

        private bool _throwHandle;
        public bool SuppressInvalidHandle
        {
            get { return !_throwHandle; }
            set { _throwHandle = !value; }
        }

        internal DspConnection(DSPConnection connection)
            : this()
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));
            FmodDspConnection = connection;
        }

        private IReadOnlyList<RESULT> Suppressions()
        {
            return ErrorChecking.Suppress(_throwHandle, true);
        }

        //public bool IsValid()
        //{
        //    return FmodDspConnection.isValid();
        //}

        #region equality
        public bool Equals(DspConnection other)
        {
            return other.FmodDspConnection == FmodDspConnection;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DspConnection))
                return false;

            return Equals((DspConnection)obj);
        }

        public override int GetHashCode()
        {
            return FmodDspConnection?.GetHashCode() ?? 0;
        }
        #endregion

        public DSP? Input
        {
            get
            {
                FMOD.DSP input;
                if (!FmodDspConnection.getInput(out input).Check(Suppressions()))
                    return null;
                return DSP.FromFmod(input);
            }
        }

        public DSP? Output
        {
            get
            {
                FMOD.DSP output;
                if (!FmodDspConnection.getOutput(out output).Check(Suppressions()))
                    return null;
                return DSP.FromFmod(output);
            }
        }

        public float? Mix
        {
            get
            {
                float volume;
                return FmodDspConnection.getMix(out volume).CheckBox(volume, Suppressions());
            }
            set
            {
                FmodDspConnection.setMix(value.Unbox()).Check(Suppressions());
            }
        }

        public void SetMixMatrix(float[] matrix, int outChannels, int inChannels, int inChannelHop)
        {
            FmodDspConnection.setMixMatrix(matrix, outChannels, inChannels, inChannelHop).Check(Suppressions());
        }

        public struct MixMatrix
        {
            public readonly float[] Matrix;
            public readonly int OutChannels;
            public readonly int InChannels;

            public MixMatrix(float[] matrix, int outChannels, int inChannels)
            {
                Matrix = matrix;
                OutChannels = outChannels;
                InChannels = inChannels;
            }
        }

        public MixMatrix? GetMatrixMatrix(float[] matrix, int inChannelHop)
        {
            int outch, inch;
            if (!FmodDspConnection.getMixMatrix(matrix, out outch, out inch, inChannelHop).Check(Suppressions()))
                return null;

            return new MixMatrix(matrix, outch, inch);
        }

        public DspConnectionType? ConnectionType
        {
            get
            {
                DSPCONNECTION_TYPE type;
                if (!FmodDspConnection.getType(out type).Check(Suppressions()))
                    return null;
                return EquivalentEnum<DSPCONNECTION_TYPE, DspConnectionType>.Cast(type);
            }
        }

        #region Userdata set/get
        public IntPtr UserData
        {
            get
            {
                IntPtr ptr;
                FmodDspConnection.getUserData(out ptr).Check();
                return ptr;
            }
            set
            {
                FmodDspConnection.setUserData(value).Check();
            }
        }
        #endregion
    }
}
