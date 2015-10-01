using System;
using System.Runtime.InteropServices;
using System.Text;
using FMOD;
using SupersonicSound.Wrapper;

namespace SupersonicSound.LowLevel
{
    public struct DSP
        : IEquatable<DSP>, IHandle
    {
        public FMOD.DSP FmodDsp { get; private set; }

        private DSP(FMOD.DSP dsp)
            : this()
        {
            FmodDsp = dsp;
        }

        public static DSP FromFmod(FMOD.DSP dsp)
        {
            if (dsp == null)
                throw new ArgumentNullException("dsp");
            return new DSP(dsp);
        }

        public bool IsValid()
        {
            return FmodDsp.isValid();
        }

        #region equality

        public bool Equals(DSP other)
        {

            return other.FmodDsp == FmodDsp;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DSP))
                return false;

            return Equals((DSP)obj);
        }

        public override int GetHashCode()
        {
            return (FmodDsp != null ? FmodDsp.GetHashCode() : 0);
        }

        #endregion

        #region Connection / disconnection / input and output enumeration.
        public DspConnection AddInput(DSP target, DspConnectionType dspConnectionType = DspConnectionType.Standard)
        {
            FMOD.DSPConnection connection;
            FmodDsp.addInput(target.FmodDsp, out connection, (DSPCONNECTION_TYPE)dspConnectionType).Check();
            return new DspConnection(connection);
        }

        public void DisconnectFrom(DSP target, DspConnection connection)
        {
            FmodDsp.disconnectFrom(target.FmodDsp, connection.FmodDspConnection);
        }

        public void DisconnectAll(bool inputs, bool outputs)
        {
            FmodDsp.disconnectAll(inputs, outputs);
        }

        public int InputCount
        {
            get
            {
                int num;
                FmodDsp.getNumInputs(out num).Check();
                return num;
            }
        }

        public int OutputCount
        {
            get
            {
                int num;
                FmodDsp.getNumOutputs(out num).Check();
                return num;
            }
        }

        public DspConnection GetInput(int index, out DSP input)
        {
            FMOD.DSPConnection connection;
            FMOD.DSP dsp;
            FmodDsp.getInput(index, out dsp, out connection).Check();

            input = FromFmod(dsp);
            return new DspConnection(connection);
        }

        public DspConnection GetOutput(int index, out DSP output)
        {
            FMOD.DSPConnection connection;
            FMOD.DSP dsp;
            FmodDsp.getOutput(index, out dsp, out connection).Check();

            output = FromFmod(dsp);
            return new DspConnection(connection);
        }
        #endregion

        #region DSP unit control
        public bool Active
        {
            get
            {
                bool active;
                FmodDsp.getActive(out active).Check();
                return active;
            }
            set
            {
                FmodDsp.setActive(value).Check();
            }
        }

        public bool Bypass
        {
            get
            {
                bool bypass;
                FmodDsp.getBypass(out bypass).Check();
                return bypass;
            }
            set
            {
                FmodDsp.setBypass(value).Check();
            }
        }

        public void SetWetDryMix(float prewet, float postwet, float dry)
        {
            FmodDsp.setWetDryMix(prewet, postwet, dry).Check();
        }

        public void GetWetDryMix(out float prewet, out float postwet, out float dry)
        {
            FmodDsp.getWetDryMix(out prewet, out postwet, out dry).Check();
        }

        public void SetChannelFormat(ChannelMask channelMask, int numChannels, SpeakerMode sourceSpeakerMode)
        {
            FmodDsp.setChannelFormat((CHANNELMASK)channelMask, numChannels, (SPEAKERMODE)sourceSpeakerMode).Check();
        }

        public void GetChannelFormat(out ChannelMask channelMask, out int numChannels, out SpeakerMode sourceSpeakerMode)
        {
            CHANNELMASK mask;
            SPEAKERMODE sourceMode;
            FmodDsp.getChannelFormat(out mask, out numChannels, out sourceMode).Check();

            channelMask = (ChannelMask)mask;
            sourceSpeakerMode = (SpeakerMode)sourceMode;
        }

        public void GetOutputChannelFormat(ChannelMask inmask, int inchannels, SpeakerMode inspeakermode, out ChannelMask outmask, out int outchannels, out SpeakerMode outspeakermode)
        {
            CHANNELMASK mask;
            SPEAKERMODE mode;
            FmodDsp.getOutputChannelFormat((CHANNELMASK)inmask, inchannels, (SPEAKERMODE)inspeakermode, out mask, out outchannels, out mode).Check();

            outmask = (ChannelMask)mask;
            outspeakermode = (SpeakerMode)mode;
        }

        public void Reset()
        {
            FmodDsp.reset().Check();
        }
        #endregion

        #region DSP parameter control
        public void SetParameter(int index, float value)
        {
            FmodDsp.setParameterFloat(index, value);
        }

        public void SetParameter(int index, int value)
        {
            FmodDsp.setParameterInt(index, value).Check();
        }

        public void SetParameter(int index, bool value)
        {
            FmodDsp.setParameterBool(index, value).Check();
        }

        public void SetParameter(int index, byte[] value)
        {
            FmodDsp.setParameterData(index, value).Check();
        }

        public float GetParameterFloat(int index)
        {
            float value;
            FmodDsp.getParameterFloat(index, out value).Check();
            return value;
        }

        public int GetParameterInt(int index)
        {
            int value;
            FmodDsp.getParameterInt(index, out value).Check();
            return value;
        }

        public bool GetParameterBool(int index)
        {
            bool value;
            FmodDsp.getParameterBool(index, out value).Check();
            return value;
        }

        public byte[] GetParameterData(int index)
        {
            IntPtr ptr;
            uint length;
            FmodDsp.getParameterData(index, out ptr, out length).Check();

            byte[] dst = new byte[length];
            Marshal.Copy(ptr, dst, 0, (int)length);

            return dst;
        }

        public int ParameterCount
        {
            get
            {
                int num;
                FmodDsp.getNumParameters(out num).Check();
                return num;
            }
        }

        public DspParameterDescription GetParameterInfo(int index)
        {
            DSP_PARAMETER_DESC desc;
            FmodDsp.getParameterInfo(index, out desc).Check();
            return new DspParameterDescription(ref desc);
        }

        public int GetDataParameterIndex(int dataType)
        {
            int index;
            FmodDsp.getDataParameterIndex(dataType, out index).Check();
            return index;
        }

        public void ShowConfigDialog(IntPtr hwnd, bool show)
        {
            FmodDsp.showConfigDialog(hwnd, show).Check();
        }
        #endregion

        #region  DSP attributes
        public void GetInfo(out string name, out uint version, out int channels, out int configWidth, out int configHeight)
        {
            StringBuilder n = new StringBuilder();
            FmodDsp.getInfo(n, out version, out channels, out configWidth, out configHeight).Check();
            name = n.ToString();
        }

        public DspType Type
        {
            get
            {
                DSP_TYPE type;
                FmodDsp.getType(out type).Check();
                return (DspType)type;
            }
        }

        public bool Idle
        {
            get
            {
                bool idle;
                FmodDsp.getIdle(out idle).Check();
                return idle;
            }
        }
        #endregion

        #region Userdata set/get
        public IntPtr UserData
        {
            get
            {
                IntPtr ptr;
                FmodDsp.getUserData(out ptr).Check();
                return ptr;
            }
            set
            {
                FmodDsp.setUserData(value).Check();
            }
        }
        #endregion

        #region Metering
        public bool IsInputMeteringEnabled
        {
            get
            {
                bool input;
                bool _;
                FmodDsp.getMeteringEnabled(out input, out _).Check();
                return input;
            }
            set
            {
                FmodDsp.setMeteringEnabled(value, IsOutputMeteringEnabled).Check();
            }
        }

        public bool IsOutputMeteringEnabled
        {
            get
            {
                bool _;
                bool output;
                FmodDsp.getMeteringEnabled(out _, out output).Check();
                return output;
            }
            set
            {
                FmodDsp.setMeteringEnabled(IsInputMeteringEnabled, value).Check();
            }
        }

        public DspMeteringInfo MeteringInputInfo
        {
            get
            {
                DSP_METERING_INFO info = new DSP_METERING_INFO
                {
                    peaklevel = new float[32],
                    rmslevel = new float[32]
                };
                FmodDsp.getMeteringInfo(info, null).Check();

                return new DspMeteringInfo(ref info);
            }
        }

        public DspMeteringInfo MeteringOutputInfo
        {
            get
            {
                DSP_METERING_INFO info = new DSP_METERING_INFO
                {
                    peaklevel = new float[32],
                    rmslevel = new float[32]
                };
                FmodDsp.getMeteringInfo(null, info).Check();

                return new DspMeteringInfo(ref info);
            }
        }
        #endregion

    }
}
