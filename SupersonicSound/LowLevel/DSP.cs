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
        public FMOD.DSP FmodDsp { get; }

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
                throw new ArgumentNullException(nameof(dsp));
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
            return FmodDsp?.GetHashCode() ?? 0;
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

        public struct DspInput
        {
            public readonly DspConnection Connection;
            public readonly DSP Input;

            public DspInput(DspConnection connection, DSP input)
            {
                Connection = connection;
                Input = input;
            }
        }

        public DspInput? GetInput(int index)
        {
            DSPConnection connection;
            FMOD.DSP dsp;
            if (!FmodDsp.getInput(index, out dsp, out connection).Check(Suppressions()))
                return null;

            return new DspInput(
                new DspConnection(connection),
                FromFmod(dsp)
            );
        }

        public struct DspOutput
        {
            public readonly DspConnection Connection;
            public readonly DSP Output;

            public DspOutput(DspConnection connection, DSP output)
            {
                Connection = connection;
                Output = output;
            }
        }

        public DspOutput? GetOutput(int index)
        {
            DSPConnection connection;
            FMOD.DSP dsp;
            if (!FmodDsp.getOutput(index, out dsp, out connection).Check(Suppressions()))
                return null;

            return new DspOutput(
                new DspConnection(connection),
                FromFmod(dsp)
            );
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

        public struct WetDryMix
        {
            public readonly float Prewet;
            public readonly float Postwet;
            public readonly float Dry;

            public WetDryMix(float prewet, float postwet, float dry)
            {
                Prewet = prewet;
                Postwet = postwet;
                Dry = dry;
            }
        }

        public WetDryMix? GetWetDryMix()
        {
            float pre, post, dr;
            if (!FmodDsp.getWetDryMix(out pre, out post, out dr).Check(Suppressions()))
                return null;

            return new WetDryMix(pre, post, dr);
        }

        public void SetChannelFormat(ChannelMask channelMask, int numChannels, SpeakerMode sourceSpeakerMode)
        {
            FmodDsp.setChannelFormat(EquivalentEnum<ChannelMask, CHANNELMASK>.Cast(channelMask), numChannels, EquivalentEnum<SpeakerMode, SPEAKERMODE>.Cast(sourceSpeakerMode)).Check(Suppressions());
        }

        public struct ChannelFormat
        {
            public readonly ChannelMask ChannelMask;
            public readonly int NumChannels;
            public readonly SpeakerMode SourceSpeakerMode;

            public ChannelFormat(ChannelMask channelMask, int numChannels, SpeakerMode sourceSpeakerMode)
            {
                ChannelMask = channelMask;
                NumChannels = numChannels;
                SourceSpeakerMode = sourceSpeakerMode;
            }
        }

        public ChannelFormat? GetChannelFormat()
        {
            CHANNELMASK mask;
            SPEAKERMODE sourceMode;
            int nc;
            if (!FmodDsp.getChannelFormat(out mask, out nc, out sourceMode).Check(Suppressions()))
            {
                return null;
            }
            else
            {
                return new ChannelFormat(
                    EquivalentEnum<CHANNELMASK, ChannelMask>.Cast(mask),
                    nc,
                    EquivalentEnum<SPEAKERMODE, SpeakerMode>.Cast(sourceMode)
                );
            }
        }

        public struct OutputChannelFormat
        {
            public readonly ChannelMask OutMask;
            public readonly int OutChannels;
            public readonly SpeakerMode OutSpeakerMode;

            public OutputChannelFormat(ChannelMask outMask, int outChannels, SpeakerMode outSpeakerMode)
            {
                OutMask = outMask;
                OutChannels = outChannels;
                OutSpeakerMode = outSpeakerMode;
            }
        }

        public OutputChannelFormat? GetOutputChannelFormat(ChannelMask inmask, int inchannels, SpeakerMode inspeakermode)
        {
            CHANNELMASK mask;
            SPEAKERMODE mode;
            int c;
            if (!FmodDsp.getOutputChannelFormat(EquivalentEnum<ChannelMask, CHANNELMASK>.Cast(inmask), inchannels, EquivalentEnum<SpeakerMode, SPEAKERMODE>.Cast(inspeakermode), out mask, out c, out mode).Check(Suppressions()))
                return null;

            return new OutputChannelFormat(
                EquivalentEnum<CHANNELMASK, ChannelMask>.Cast(mask),
                c,
                EquivalentEnum<SPEAKERMODE, SpeakerMode>.Cast(mode)
            );
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
        public struct DspInfo
        {
            public readonly string Name;
            public readonly uint Version;
            public readonly int Channels;
            public readonly int ConfigWidth;
            public readonly int ConfigHeight;

            public DspInfo(string name, uint version, int channels, int configWidth, int configHeight)
            {
                Name = name;
                Version = version;
                Channels = channels;
                ConfigWidth = configWidth;
                ConfigHeight = configHeight;
            }
        }

        public DspInfo? GetInfo()
        {
            var n = new StringBuilder();
            uint v;
            int cha, cow, coh;
            if (!FmodDsp.getInfo(n, out v, out cha, out cow, out coh).Check(Suppressions()))
                return null;

            return new DspInfo(n.ToString(), v, cha, cow, coh);
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
