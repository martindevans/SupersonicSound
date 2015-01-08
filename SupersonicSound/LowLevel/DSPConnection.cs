using System;
using FMOD;
using SupersonicSound.Wrapper;

namespace SupersonicSound.LowLevel
{
    public struct DSPConnection
        : IEquatable<DSPConnection>
    {
        public FMOD.DSPConnection FmodDSPConnection { get; private set; }

        private DSPConnection(FMOD.DSPConnection connection)
            : this()
        {
            FmodDSPConnection = connection;
        }

        public static DSPConnection FromFmod(FMOD.DSPConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            return new DSPConnection(connection);
        }

        #region equality
        public bool Equals(DSPConnection other)
        {
            return other.FmodDSPConnection == FmodDSPConnection;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DSPConnection))
                return false;

            return Equals((DSPConnection)obj);
        }

        public override int GetHashCode()
        {
            return (FmodDSPConnection != null ? FmodDSPConnection.GetHashCode() : 0);
        }
        #endregion

        public DSP Input
        {
            get
            {
                FMOD.DSP input;
                FmodDSPConnection.getInput(out input).Check();
                return DSP.FromFmod(input);
            }
        }

        public DSP Output
        {
            get
            {
                FMOD.DSP output;
                FmodDSPConnection.getOutput(out output).Check();
                return DSP.FromFmod(output);
            }
        }

        public float Mix
        {
            get
            {
                float volume;
                FmodDSPConnection.getMix(out volume).Check();
                return volume;
            }
            set
            {
                FmodDSPConnection.setMix(value).Check();
            }
        }

        public void SetMixMatrix(float[] matrix, int outChannels, int inChannels, int inChannelHop)
        {
            FmodDSPConnection.setMixMatrix(matrix, outChannels, inChannels, inChannelHop).Check();
        }

        public void GetMatrixMatrix(float[] matrix, out int outChannels, out int inChannels, int inChannelHop)
        {
            FmodDSPConnection.getMixMatrix(matrix, out outChannels, out inChannels, inChannelHop).Check();
        }

        public DspConnectionType ConnectionType
        {
            get
            {
                DSPCONNECTION_TYPE type;
                FmodDSPConnection.getType(out type).Check();
                return (DspConnectionType)type;
            }
        }

        #region Userdata set/get
        public IntPtr UserData
        {
            get
            {
                IntPtr ptr;
                FmodDSPConnection.getUserData(out ptr).Check();
                return ptr;
            }
            set
            {
                FmodDSPConnection.setUserData(value).Check();
            }
        }
        #endregion
    }
}
