
using System;
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

        //public RESULT addInput(DSP target, out DSPConnection connection)
        //{
        //    connection = null;

        //    IntPtr dspconnectionraw;
        //    RESULT result = FMOD5_DSP_AddInput(rawPtr, target.getRaw(), out dspconnectionraw);
        //    connection = new DSPConnection(dspconnectionraw);

        //    return result;
        //}
        //public RESULT disconnectFrom(DSP target)
        //{
        //    return FMOD5_DSP_DisconnectFrom(rawPtr, target.getRaw());
        //}
        //public RESULT disconnectAll(bool inputs, bool outputs)
        //{
        //    return FMOD5_DSP_DisconnectAll(rawPtr, inputs, outputs);
        //}
        //public RESULT getNumInputs(out int numinputs)
        //{
        //    return FMOD5_DSP_GetNumInputs(rawPtr, out numinputs);
        //}
        //public RESULT getNumOutputs(out int numoutputs)
        //{
        //    return FMOD5_DSP_GetNumOutputs(rawPtr, out numoutputs);
        //}
        //public RESULT getInput(int index, out DSP input, out DSPConnection inputconnection)
        //{
        //    input = null;
        //    inputconnection = null;

        //    IntPtr dspinputraw;
        //    IntPtr dspconnectionraw;
        //    RESULT result = FMOD5_DSP_GetInput(rawPtr, index, out dspinputraw, out dspconnectionraw);
        //    input = new DSP(dspinputraw);
        //    inputconnection = new DSPConnection(dspconnectionraw);

        //    return result;
        //}
        //public RESULT getOutput(int index, out DSP output, out DSPConnection outputconnection)
        //{
        //    output = null;
        //    outputconnection = null;

        //    IntPtr dspoutputraw;
        //    IntPtr dspconnectionraw;
        //    RESULT result = FMOD5_DSP_GetOutput(rawPtr, index, out dspoutputraw, out dspconnectionraw);
        //    output = new DSP(dspoutputraw);
        //    outputconnection = new DSPConnection(dspconnectionraw);

        //    return result;
        //}

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
        //public RESULT reset()
        //{
        //    return FMOD5_DSP_Reset(rawPtr);
        //}
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
        //public RESULT setUserData(IntPtr userdata)
        //{
        //    return FMOD5_DSP_SetUserData(rawPtr, userdata);
        //}
        //public RESULT getUserData(out IntPtr userdata)
        //{
        //    return FMOD5_DSP_GetUserData(rawPtr, out userdata);
        //}
        #endregion

        #region Metering
        //public RESULT setMeteringEnabled(bool inputEnabled, bool outputEnabled)
        //{
        //    return FMOD5_DSP_SetMeteringEnabled(rawPtr, inputEnabled, outputEnabled);
        //}
        //public RESULT getMeteringEnabled(out bool inputEnabled, out bool outputEnabled)
        //{
        //    return FMOD5_DSP_GetMeteringEnabled(rawPtr, out inputEnabled, out outputEnabled);
        //}

        //public RESULT getMeteringInfo(out DSP_METERING_INFO info)
        //{
        //    return FMOD5_DSP_GetMeteringInfo(rawPtr, out info);
        //}
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
