

using System;

namespace SupersonicSound.LowLevel
{
    /// <summary>
    /// Equivalent signature to FMOD.ChannelControl
    /// </summary>
    public interface IChannelControl
        //: IHandle
    {
        #region General control functionality for Channels and ChannelGroups.
        bool Stop();

        bool? Pause { get; set; }

        float? Volume { get; set; }

        bool? VolumeRamp { get; set; }

        float? Audibility { get; }

        float? Pitch { get; set; }

        bool? Mute { get; set; }

        //Unsure how we want to implement these! Depending on what instance means we may want to have a `ReverbProperties[instance] = wet` style API
        //todo: public RESULT setReverbProperties(int instance, float wet);
        //todo: public RESULT getReverbProperties(int instance, out float wet);

        float? LowPassGain { get; set; }

        Mode? Mode { get; set; }

        void SetCallback(Action<ChannelControlCallbackType, IntPtr, IntPtr> callback);

        void RemoveCallback();

        bool? IsPlaying { get; }
        #endregion

        #region Panning and level adjustment.
        float Pan { set; }

        //How best to represent matrices?
        // - 2D array seems neatest, but means we need to allocate a 1D array and copy across the data
        //todo: public RESULT setMixLevelsOutput(float frontleft, float frontright, float center, float lfe, float surroundleft, float surroundright, float backleft, float backright)
        //todo: public RESULT setMixLevelsInput(float[] levels, int numlevels)
        //todo: public RESULT setMixMatrix(float[] matrix, int outchannels, int inchannels, int inchannel_hop)
        //todo: public RESULT getMixMatrix(float[] matrix, out int outchannels, out int inchannels, int inchannel_hop)
        #endregion

        #region clock based functionality
        DspClock GetDspClock();

        ChannelDelay Delay { get; set; }

        void AddFadePoint(ulong dspClock, float volume);

        void SetFadePointRamp(ulong dspClock, float volume);

        void RemoveFadePoints(ulong dspClockStart, ulong dspClockEnd);

        uint GetFadePointsCount();

        uint GetFadePoints(ulong[] pointDspClock, float[] pointVolume);
        #endregion

        #region DSP effects
        //todo:public RESULT getDSP(int index, out DSP dsp)
        //todo:public RESULT addDSP(int index, DSP dsp)
        //todo:public RESULT removeDSP(DSP dsp)
        //todo:public RESULT getNumDSPs(out int numdsps)
        //todo:public RESULT setDSPIndex(DSP dsp, int index)
        //todo:public RESULT getDSPIndex(DSP dsp, out int index)
        //todo:public RESULT overridePanDSP(DSP pan)
        #endregion

        #region 3D functionality.
        //todo:public RESULT set3DAttributes(ref VECTOR pos, ref VECTOR vel, ref VECTOR alt_pan_pos)
        //todo:public RESULT get3DAttributes(out VECTOR pos, out VECTOR vel, out VECTOR alt_pan_pos)
        //todo:public RESULT set3DMinMaxDistance(float mindistance, float maxdistance)
        //todo:public RESULT get3DMinMaxDistance(out float mindistance, out float maxdistance)
        //todo:public RESULT set3DConeSettings(float insideconeangle, float outsideconeangle, float outsidevolume)
        //todo:public RESULT get3DConeSettings(out float insideconeangle, out float outsideconeangle, out float outsidevolume)
        //todo:public RESULT set3DConeOrientation(ref VECTOR orientation)
        //todo:public RESULT get3DConeOrientation(out VECTOR orientation)
        //todo:public RESULT set3DCustomRolloff(ref VECTOR points, int numpoints)
        //todo:public RESULT get3DCustomRolloff(out IntPtr points, out int numpoints)
        //todo:public RESULT set3DOcclusion(float directocclusion, float reverbocclusion)
        //todo:public RESULT get3DOcclusion(out float directocclusion, out float reverbocclusion)
        //todo:public RESULT set3DSpread(float angle)
        //todo:public RESULT get3DSpread(out float angle)
        //todo:public RESULT set3DLevel(float level)OD_ChannelGroup_Set3DLevel(rawPtr, level);
        //todo:public RESULT get3DLevel(out float level)
        //todo:public RESULT set3DDopplerLevel(float level)
        //todo:public RESULT get3DDopplerLevel(out float level)
        //todo:public RESULT set3DDistanceFilter(bool custom, float customLevel, float centerFreq)
        //todo:public RESULT get3DDistanceFilter(out bool custom, out float customLevel, out float centerFreq)
        #endregion
    }
}
