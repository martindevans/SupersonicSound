using System;
using FMOD;
using SupersonicSound.Wrapper;

namespace SupersonicSound.LowLevel
{
    public struct DspConnection
        : IEquatable<DspConnection>
    {
        public DSPConnection FmodDspConnection { get; private set; }

        internal DspConnection(DSPConnection connection)
            : this()
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            FmodDspConnection = connection;
        }

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
            return (FmodDspConnection != null ? FmodDspConnection.GetHashCode() : 0);
        }
        #endregion

        public DSP Input
        {
            get
            {
                FMOD.DSP input;
                FmodDspConnection.getInput(out input).Check();
                return DSP.FromFmod(input);
            }
        }

        public DSP Output
        {
            get
            {
                FMOD.DSP output;
                FmodDspConnection.getOutput(out output).Check();
                return DSP.FromFmod(output);
            }
        }

        public float Mix
        {
            get
            {
                float volume;
                FmodDspConnection.getMix(out volume).Check();
                return volume;
            }
            set
            {
                FmodDspConnection.setMix(value).Check();
            }
        }

        public void SetMixMatrix(float[] matrix, int outChannels, int inChannels, int inChannelHop)
        {
            FmodDspConnection.setMixMatrix(matrix, outChannels, inChannels, inChannelHop).Check();
        }

        public void GetMatrixMatrix(float[] matrix, out int outChannels, out int inChannels, int inChannelHop)
        {
            FmodDspConnection.getMixMatrix(matrix, out outChannels, out inChannels, inChannelHop).Check();
        }

        public DspConnectionType ConnectionType
        {
            get
            {
                DSPCONNECTION_TYPE type;
                FmodDspConnection.getType(out type).Check();
                return (DspConnectionType)type;
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
