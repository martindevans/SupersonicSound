using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using FMOD;
using SupersonicSound.Wrapper;

namespace SupersonicSound.LowLevel
{
    public struct DSP
        : IEquatable<DSP>//, IHandle
    {
        public FMOD.DSP FmodDsp { get; private set; }

        private bool _throwHandle;
        public bool SuppressInvalidHandle
        {
            get { return !_throwHandle; }
            set { _throwHandle = !value; }
        }

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

        private IReadOnlyList<RESULT> Suppressions()
        {
            return ErrorChecking.Suppress(_throwHandle, true);
        }

        //public bool IsValid()
        //{
        //    return FmodDsp.isValid();
        //}

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
        public DspConnection? AddInput(DSP target, DspConnectionType dspConnectionType = DspConnectionType.Standard)
        {
            DSPConnection connection;
            if (!FmodDsp.addInput(target.FmodDsp, out connection, EquivalentEnum<DspConnectionType, DSPCONNECTION_TYPE>.Cast(dspConnectionType)).Check(Suppressions()))
                return null;

            return new DspConnection(connection);
        }

        public void DisconnectFrom(DSP target, DspConnection connection)
        {
            FmodDsp.disconnectFrom(target.FmodDsp, connection.FmodDspConnection).Check(Suppressions());
        }

        public void DisconnectAll(bool inputs, bool outputs)
        {
            FmodDsp.disconnectAll(inputs, outputs).Check(Suppressions());
        }

        public int? InputCount
        {
            get
            {
                int num;
                return FmodDsp.getNumInputs(out num).CheckBox(num, Suppressions());
            }
        }

        public int? OutputCount
        {
            get
            {
                int num;
                return FmodDsp.getNumOutputs(out num).CheckBox(num, Suppressions());
            }
        }

        public DspConnection? GetInput(int index, out DSP? input)
        {
            DSPConnection connection;
            FMOD.DSP dsp;
            if (!FmodDsp.getInput(index, out dsp, out connection).Check(Suppressions()))
            {
                input = null;
                return null;
            }

            input = FromFmod(dsp);
            return new DspConnection(connection);
        }

        public DspConnection? GetOutput(int index, out DSP? output)
        {
            DSPConnection connection;
            FMOD.DSP dsp;
            if (!FmodDsp.getOutput(index, out dsp, out connection).Check(Suppressions()))
            {
                output = null;
                return null;
            }

            output = FromFmod(dsp);
            return new DspConnection(connection);
        }
        #endregion

        #region DSP unit control
        public bool? Active
        {
            get
            {
                bool active;
                return FmodDsp.getActive(out active).CheckBox(active, Suppressions());
            }
            set
            {
                FmodDsp.setActive(value.Unbox()).Check(Suppressions());
            }
        }

        public bool? Bypass
        {
            get
            {
                bool bypass;
                return FmodDsp.getBypass(out bypass).CheckBox(bypass, Suppressions());
            }
            set
            {
                FmodDsp.setBypass(value.Unbox()).Check(Suppressions());
            }
        }

        public void SetWetDryMix(float prewet, float postwet, float dry)
        {
            FmodDsp.setWetDryMix(prewet, postwet, dry).Check(Suppressions());
        }

        public void GetWetDryMix(out float? prewet, out float? postwet, out float? dry)
        {
            float pre, post, dr;
            if (!FmodDsp.getWetDryMix(out pre, out post, out dr).Check(Suppressions()))
            {
                prewet = null;
                postwet = null;
                dry = null;
            }
            else
            {
                prewet = pre;
                postwet = post;
                dry = dr;
            }
        }

        public void SetChannelFormat(ChannelMask channelMask, int numChannels, SpeakerMode sourceSpeakerMode)
        {
            FmodDsp.setChannelFormat(EquivalentEnum<ChannelMask, CHANNELMASK>.Cast(channelMask), numChannels, EquivalentEnum<SpeakerMode, SPEAKERMODE>.Cast(sourceSpeakerMode)).Check(Suppressions());
        }

        public void GetChannelFormat(out ChannelMask? channelMask, out int? numChannels, out SpeakerMode? sourceSpeakerMode)
        {
            CHANNELMASK mask;
            SPEAKERMODE sourceMode;
            int nc;
            if (!FmodDsp.getChannelFormat(out mask, out nc, out sourceMode).Check(Suppressions()))
            {
                channelMask = null;
                numChannels = null;
                sourceSpeakerMode = null;
            }
            else
            {
                channelMask = EquivalentEnum<CHANNELMASK, ChannelMask>.Cast(mask);
                numChannels = nc;
                sourceSpeakerMode = EquivalentEnum<SPEAKERMODE, SpeakerMode>.Cast(sourceMode);
            }
        }

        public void GetOutputChannelFormat(ChannelMask inmask, int inchannels, SpeakerMode inspeakermode, out ChannelMask? outmask, out int? outchannels, out SpeakerMode? outspeakermode)
        {
            CHANNELMASK mask;
            SPEAKERMODE mode;
            int c;
            if (!FmodDsp.getOutputChannelFormat(EquivalentEnum<ChannelMask, CHANNELMASK>.Cast(inmask), inchannels, EquivalentEnum<SpeakerMode, SPEAKERMODE>.Cast(inspeakermode), out mask, out c, out mode).Check(Suppressions()))
            {
                outmask = null;
                outspeakermode = null;
                outchannels = null;
            }
            else
            {
                outmask = EquivalentEnum<CHANNELMASK, ChannelMask>.Cast(mask);
                outspeakermode = EquivalentEnum<SPEAKERMODE, SpeakerMode>.Cast(mode);
                outchannels = c;
            }
        }

        public void Reset()
        {
            FmodDsp.reset().Check(Suppressions());
        }
        #endregion

        #region DSP parameter control
        public void SetParameter(int index, float value)
        {
            FmodDsp.setParameterFloat(index, value).Check(Suppressions());
        }

        public void SetParameter(int index, int value)
        {
            FmodDsp.setParameterInt(index, value).Check(Suppressions());
        }

        public void SetParameter(int index, bool value)
        {
            FmodDsp.setParameterBool(index, value).Check(Suppressions());
        }

        public void SetParameter(int index, byte[] value)
        {
            FmodDsp.setParameterData(index, value).Check(Suppressions());
        }

        public float? GetParameterFloat(int index)
        {
            float value;
            return FmodDsp.getParameterFloat(index, out value).CheckBox(value, Suppressions());
        }

        public int? GetParameterInt(int index)
        {
            int value;
            return FmodDsp.getParameterInt(index, out value).CheckBox(value, Suppressions());
        }

        public bool? GetParameterBool(int index)
        {
            bool value;
            return FmodDsp.getParameterBool(index, out value).CheckBox(value, Suppressions());
        }

        public byte[] GetParameterData(int index)
        {
            IntPtr ptr;
            uint length;
            if (!FmodDsp.getParameterData(index, out ptr, out length).Check(Suppressions()))
                return null;

            byte[] dst = new byte[length];
            Marshal.Copy(ptr, dst, 0, (int)length);

            return dst;
        }

        public int? ParameterCount
        {
            get
            {
                int num;
                return FmodDsp.getNumParameters(out num).CheckBox(num, Suppressions());
            }
        }

        public DspParameterDescription? GetParameterInfo(int index)
        {
            DSP_PARAMETER_DESC desc;
            if (!FmodDsp.getParameterInfo(index, out desc).Check(Suppressions()))
                return null;
            return new DspParameterDescription(ref desc);
        }

        public int? GetDataParameterIndex(int dataType)
        {
            int index;
            return FmodDsp.getDataParameterIndex(dataType, out index).CheckBox(index, Suppressions());
        }

        public void ShowConfigDialog(IntPtr hwnd, bool show)
        {
            FmodDsp.showConfigDialog(hwnd, show).Check(Suppressions());
        }
        #endregion

        #region  DSP attributes
        public void GetInfo(out string name, out uint? version, out int? channels, out int? configWidth, out int? configHeight)
        {
            StringBuilder n = new StringBuilder();
            uint v;
            int cha, cow, coh;
            if (!FmodDsp.getInfo(n, out v, out cha, out cow, out coh).Check(Suppressions()))
            {
                name = null;
                version = null;
                channels = null;
                configWidth = null;
                configHeight = null;
            }
            else
            {
                name = n.ToString();
                version = v;
                channels = cha;
                configWidth = cow;
                configHeight = coh;
            }
        }

        public DspType? Type
        {
            get
            {
                DSP_TYPE type;
                if (!FmodDsp.getType(out type).Check(Suppressions()))
                    return null;
                return EquivalentEnum<DSP_TYPE, DspType>.Cast(type);
            }
        }

        public bool? Idle
        {
            get
            {
                bool idle;
                return FmodDsp.getIdle(out idle).CheckBox(idle, Suppressions());
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
        public bool? IsInputMeteringEnabled
        {
            get
            {
                bool input;
                bool _;
                return FmodDsp.getMeteringEnabled(out input, out _).CheckBox(input, Suppressions());
            }
            set
            {
                FmodDsp.setMeteringEnabled(value.Unbox(), IsOutputMeteringEnabled.Unbox()).Check(Suppressions());
            }
        }

        public bool? IsOutputMeteringEnabled
        {
            get
            {
                bool _;
                bool output;
                return FmodDsp.getMeteringEnabled(out _, out output).CheckBox(output, Suppressions());
            }
            set
            {
                FmodDsp.setMeteringEnabled(IsInputMeteringEnabled.Unbox(), value.Unbox()).Check(Suppressions());
            }
        }

        public DspMeteringInfo? MeteringInputInfo
        {
            get
            {
                DSP_METERING_INFO info = new DSP_METERING_INFO
                {
                    peaklevel = new float[32],
                    rmslevel = new float[32]
                };
                if (!FmodDsp.getMeteringInfo(info, null).Check(Suppressions()))
                    return null;

                return new DspMeteringInfo(ref info);
            }
        }

        public DspMeteringInfo? MeteringOutputInfo
        {
            get
            {
                DSP_METERING_INFO info = new DSP_METERING_INFO
                {
                    peaklevel = new float[32],
                    rmslevel = new float[32]
                };
                if (!FmodDsp.getMeteringInfo(null, info).Check(Suppressions()))
                    return null;

                return new DspMeteringInfo(ref info);
            }
        }
        #endregion

    }
}
