using System;
using System.Runtime.InteropServices;
using System.Text;
using FMOD;
using SupersonicSound.Wrapper;

namespace SupersonicSound.LowLevel
{
    public struct DSP
        : IEquatable<DSP>
    {
        public FMOD.DSP FmodDSP { get; private set; }

        private DSP(FMOD.DSP dsp)
            : this()
        {
            FmodDSP = dsp;
        }

        public static DSP FromFmod(FMOD.DSP dsp)
        {
            if (dsp == null)
                throw new ArgumentNullException("dsp");
            return new DSP(dsp);
        }

        #region equality

        public bool Equals(DSP other)
        {

            return other.FmodDSP == FmodDSP;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DSP))
                return false;

            return Equals((DSP)obj);
        }

        public override int GetHashCode()
        {
            return (FmodDSP != null ? FmodDSP.GetHashCode() : 0);
        }

        #endregion

        #region Connection / disconnection / input and output enumeration.
        public DSPConnection  AddInput(DSP target)
        {
            FMOD.DSPConnection connection;
            FmodDSP.addInput(target.FmodDSP, out connection).Check();
            return DSPConnection.FromFmod(connection);
        }

        public void DisconnectFrom(DSP target)
        {
            FmodDSP.disconnectFrom(target.FmodDSP);
        }

        public void DisconnectAll(bool inputs, bool outputs)
        {
            FmodDSP.disconnectAll(inputs, outputs);
        }

        public int InputCount
        {
            get
            {
                int num;
                FmodDSP.getNumInputs(out num).Check();
                return num;
            }
        }

        public int OutputCount
        {
            get
            {
                int num;
                FmodDSP.getNumOutputs(out num).Check();
                return num;
            }
        }

        public DSPConnection GetInput(int index, out DSP input)
        {
            FMOD.DSPConnection connection;
            FMOD.DSP dsp;
            FmodDSP.getInput(index, out dsp, out connection).Check();

            input = FromFmod(dsp);
            return DSPConnection.FromFmod(connection);
        }

        public DSPConnection GetOutput(int index, out DSP output)
        {
            FMOD.DSPConnection connection;
            FMOD.DSP dsp;
            FmodDSP.getOutput(index, out dsp, out connection).Check();

            output = FromFmod(dsp);
            return DSPConnection.FromFmod(connection);
        }
        #endregion

        #region DSP unit control
        public bool Active
        {
            get
            {
                bool active;
                FmodDSP.getActive(out active).Check();
                return active;
            }
            set
            {
                FmodDSP.setActive(value).Check();
            }
        }

        public bool Bypass
        {
            get
            {
                bool bypass;
                FmodDSP.getBypass(out bypass).Check();
                return bypass;
            }
            set
            {
                FmodDSP.setBypass(value).Check();
            }
        }

        public void SetWetDryMix(float wet, float dry)
        {
            FmodDSP.setWetDryMix(wet, dry).Check();
        }

        public void GetWetDryMix(out float wet, out float dry)
        {
            FmodDSP.getWetDryMix(out wet, out dry).Check();
        }

        public void SetChannelFormat(ChannelMask channelMask, int numChannels, SpeakerMode sourceSpeakerMode)
        {
            FmodDSP.setChannelFormat((CHANNELMASK)channelMask, numChannels, (SPEAKERMODE)sourceSpeakerMode).Check();
        }

        public void GetChannelFormat(out ChannelMask channelMask, out int numChannels, out SpeakerMode sourceSpeakerMode)
        {
            CHANNELMASK mask;
            SPEAKERMODE sourceMode;
            FmodDSP.getChannelFormat(out mask, out numChannels, out sourceMode).Check();

            channelMask = (ChannelMask)mask;
            sourceSpeakerMode = (SpeakerMode)sourceMode;
        }

        public void GetOutputChannelFormat(ChannelMask inmask, int inchannels, SpeakerMode inspeakermode, out ChannelMask outmask, out int outchannels, out SpeakerMode outspeakermode)
        {
            CHANNELMASK mask;
            SPEAKERMODE mode;
            FmodDSP.getOutputChannelFormat((CHANNELMASK)inmask, inchannels, (SPEAKERMODE)inspeakermode, out mask, out outchannels, out mode).Check();

            outmask = (ChannelMask)mask;
            outspeakermode = (SpeakerMode)mode;
        }

        public void Reset()
        {
            FmodDSP.reset().Check();
        }
        #endregion

        #region DSP parameter control
        public void SetParameter(int index, float value)
        {
            FmodDSP.setParameterFloat(index, value);
        }

        public void SetParameter(int index, int value)
        {
            FmodDSP.setParameterInt(index, value).Check();
        }

        public void SetParameter(int index, bool value)
        {
            FmodDSP.setParameterBool(index, value).Check();
        }

        public void SetParameter(int index, byte[] value)
        {
            FmodDSP.setParameterData(index, value).Check();
        }

        public float GetParameterFloat(int index)
        {
            float value;
            FmodDSP.getParameterFloat(index, out value).Check();
            return value;
        }

        public int GetParameterInt(int index)
        {
            int value;
            FmodDSP.getParameterInt(index, out value).Check();
            return value;
        }

        public bool GetParameterBool(int index)
        {
            bool value;
            FmodDSP.getParameterBool(index, out value).Check();
            return value;
        }

        public byte[] GetParameterData(int index)
        {
            IntPtr ptr;
            uint length;
            FmodDSP.getParameterData(index, out ptr, out length).Check();

            byte[] dst = new byte[length];
            Marshal.Copy(ptr, dst, 0, (int)length);

            return dst;
        }

        public int ParameterCount
        {
            get
            {
                int num;
                FmodDSP.getNumParameters(out num).Check();
                return num;
            }
        }

        public DspParameterDescription GetParameterInfo(int index)
        {
            DSP_PARAMETER_DESC desc;
            FmodDSP.getParameterInfo(index, out desc).Check();
            return new DspParameterDescription(desc);
        }

        public int GetDataParameterIndex(int dataType)
        {
            int index;
            FmodDSP.getDataParameterIndex(dataType, out index).Check();
            return index;
        }

        public void ShowConfigDialog(IntPtr hwnd, bool show)
        {
            FmodDSP.showConfigDialog(hwnd, show).Check();
        }
        #endregion

        #region  DSP attributes
        public void GetInfo(out string name, out uint version, out int channels, out int configWidth, out int configHeight)
        {
            StringBuilder n = new StringBuilder();
            FmodDSP.getInfo(n, out version, out channels, out configWidth, out configHeight).Check();
            name = n.ToString();
        }

        public DspType Type
        {
            get
            {
                DSP_TYPE type;
                FmodDSP.getType(out type).Check();
                return (DspType)type;
            }
        }

        public bool Idle
        {
            get
            {
                bool idle;
                FmodDSP.getIdle(out idle).Check();
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
                FmodDSP.getUserData(out ptr).Check();
                return ptr;
            }
            set
            {
                FmodDSP.setUserData(value).Check();
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
                FmodDSP.getMeteringEnabled(out input, out _).Check();
                return input;
            }
            set
            {
                FmodDSP.setMeteringEnabled(value, IsOutputMeteringEnabled).Check();
            }
        }

        public bool IsOutputMeteringEnabled
        {
            get
            {
                bool _;
                bool output;
                FmodDSP.getMeteringEnabled(out _, out output).Check();
                return output;
            }
            set
            {
                FmodDSP.setMeteringEnabled(IsInputMeteringEnabled, value).Check();
            }
        }

        public DspMeteringInfo MeteringInfo
        {
            get
            {
                DSP_METERING_INFO info;
                FmodDSP.getMeteringInfo(out info).Check();
                return new DspMeteringInfo(info);
            }
        }
        #endregion

    }

    public static class DSPExtensions
    {
        public static FMOD.DSP ToFmod(this DSP dsp)
        {
            return dsp.FmodDSP;
        }
    }
}
