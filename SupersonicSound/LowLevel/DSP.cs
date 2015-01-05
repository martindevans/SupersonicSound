using System;
using FMOD;
using SupersonicSound.Wrapper;

namespace SupersonicSound.LowLevel
{
    public class DSP
        : IEquatable<DSP>
    {
        public FMOD.DSP FmodDSP { get; private set; }

        private DSP(FMOD.DSP dsp)
        {
            FmodDSP = dsp;
        }

        public static DSP FromFmod(FMOD.DSP dsp)
        {
            if (dsp == null)
                return null;
            return new DSP(dsp);
        }

        #region equality

        public bool Equals(DSP other)
        {
            if (other == null)
                return false;

            return other.FmodDSP == FmodDSP;
        }

        public override bool Equals(object obj)
        {
            var c = obj as DSP;
            if (c == null)
                return false;

            return Equals(c);
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

        //public RESULT setWetDryMix(float wet, float dry)
        //{
        //    return FMOD5_DSP_SetWetDryMix(rawPtr, wet, dry);
        //}
        //public RESULT getWetDryMix(out float wet, out float dry)
        //{
        //    return FMOD5_DSP_GetWetDryMix(rawPtr, out wet, out dry);
        //}
        //public RESULT setChannelFormat(CHANNELMASK channelmask, int numchannels, SPEAKERMODE source_speakermode)
        //{
        //    return FMOD5_DSP_SetChannelFormat(rawPtr, channelmask, numchannels, source_speakermode);
        //}
        //public RESULT getChannelFormat(out CHANNELMASK channelmask, out int numchannels, out SPEAKERMODE source_speakermode)
        //{
        //    return FMOD5_DSP_GetChannelFormat(rawPtr, out channelmask, out numchannels, out source_speakermode);
        //}
        //public RESULT getOutputChannelFormat(CHANNELMASK inmask, int inchannels, SPEAKERMODE inspeakermode, out CHANNELMASK outmask, out int outchannels, out SPEAKERMODE outspeakermode)
        //{
        //    return FMOD5_DSP_GetOutputChannelFormat(rawPtr, inmask, inchannels, inspeakermode, out outmask, out outchannels, out outspeakermode);
        //}

        public void Reset()
        {
            FmodDSP.reset().Check();
        }
        #endregion

        #region DSP parameter control
//        public RESULT setParameterFloat(int index, float value)
//        {
//            return FMOD5_DSP_SetParameterFloat(rawPtr, index, value);
//        }
//        public RESULT setParameterInt(int index, int value)
//        {
//            return FMOD5_DSP_SetParameterInt(rawPtr, index, value);
//        }
//        public RESULT setParameterBool(int index, bool value)
//        {
//            return FMOD5_DSP_SetParameterBool(rawPtr, index, value);
//        }
//        public RESULT setParameterData(int index, byte[] data)
//        {
//            return FMOD5_DSP_SetParameterData(rawPtr, index, Marshal.UnsafeAddrOfPinnedArrayElement(data, 0), (uint)data.Length);
//        }
//        public RESULT getParameterFloat(int index, out float value)
//        {
//            IntPtr valuestr = IntPtr.Zero;
//            return FMOD5_DSP_GetParameterFloat(rawPtr, index, out value, valuestr, 0);
//        }
//        public RESULT getParameterInt(int index, out int value)
//        {
//            IntPtr valuestr = IntPtr.Zero;
//            return FMOD5_DSP_GetParameterInt(rawPtr, index, out value, valuestr, 0);
//        }
//        public RESULT getParameterBool(int index, out bool value)
//        {
//            return FMOD5_DSP_GetParameterBool(rawPtr, index, out value, IntPtr.Zero, 0);
//        }
//        public RESULT getParameterData(int index, out IntPtr data, out uint length)
//        {
//            return FMOD5_DSP_GetParameterData(rawPtr, index, out data, out length, IntPtr.Zero, 0);
//        }
//        public RESULT getNumParameters(out int numparams)
//        {
//            return FMOD5_DSP_GetNumParameters(rawPtr, out numparams);
//        }
//        public RESULT getParameterInfo(int index, out DSP_PARAMETER_DESC desc)
//        {
//            IntPtr descPtr;
//            RESULT result = FMOD5_DSP_GetParameterInfo(rawPtr, index, out descPtr);
//            if (result == RESULT.OK)
//            {
//#if NETFX_CORE
//                desc = Marshal.PtrToStructure<DSP_PARAMETER_DESC>(descPtr);
//#else
//                desc = (DSP_PARAMETER_DESC)Marshal.PtrToStructure(descPtr, typeof(DSP_PARAMETER_DESC));
//#endif
//            }
//            else
//            {
//                desc = new DSP_PARAMETER_DESC();
//            }
//            return result;
//        }
//        public RESULT getDataParameterIndex(int datatype, out int index)
//        {
//            return FMOD5_DSP_GetDataParameterIndex(rawPtr, datatype, out index);
//        }
//        public RESULT showConfigDialog(IntPtr hwnd, bool show)
//        {
//            return FMOD5_DSP_ShowConfigDialog(rawPtr, hwnd, show);
//        }
        #endregion

        #region  DSP attributes
        //public RESULT getInfo(StringBuilder name, out uint version, out int channels, out int configwidth, out int configheight)
        //{
        //    IntPtr nameMem = Marshal.AllocHGlobal(32);
        //    RESULT result = FMOD5_DSP_GetInfo(rawPtr, nameMem, out version, out channels, out configwidth, out configheight);
        //    StringMarshalHelper.NativeToBuilder(name, nameMem);
        //    Marshal.FreeHGlobal(nameMem);
        //    return result;
        //}

        //public RESULT getType(out DSP_TYPE type)
        //{
        //    return FMOD5_DSP_GetType(rawPtr, out type);
        //}

        //public RESULT getIdle(out bool idle)
        //{
        //    return FMOD5_DSP_GetIdle(rawPtr, out idle);
        //}
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
            if (dsp == null)
                return null;

            return dsp.FmodDSP;
        }
    }
}
