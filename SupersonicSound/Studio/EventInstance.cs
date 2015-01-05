using System;

namespace SupersonicSound.Studio
{
    public class EventInstance
        : IEquatable<EventInstance>
    {
        public FMOD.Studio.EventInstance FmodEventInstance { get; private set; }

        private EventInstance(FMOD.Studio.EventInstance evtInst)
        {
            FmodEventInstance = evtInst;
        }

        public static EventInstance FromFmod(FMOD.Studio.EventInstance evtInst)
        {
            if (evtInst == null)
                return null;
            return new EventInstance(evtInst);
        }

        #region equality
        public bool Equals(EventInstance other)
        {
            if (other == null)
                return false;

            return other.FmodEventInstance == FmodEventInstance;
        }

        public override bool Equals(object obj)
        {
            var c = obj as EventInstance;
            if (c == null)
                return false;

            return Equals(c);
        }

        public override int GetHashCode()
        {
            return (FmodEventInstance != null ? FmodEventInstance.GetHashCode() : 0);
        }
        #endregion

        //public RESULT getDescription(out EventDescription description)
        //{
        //    description = null;

        //    IntPtr newPtr;
        //    RESULT result = FMOD_Studio_EventInstance_GetDescription(rawPtr, out newPtr);
        //    if (result != RESULT.OK)
        //    {
        //        return result;
        //    }
        //    description = new EventDescription(newPtr);
        //    return result;
        //}
        //public RESULT getVolume(out float volume)
        //{
        //    return FMOD_Studio_EventInstance_GetVolume(rawPtr, out volume);
        //}
        //public RESULT setVolume(float volume)
        //{
        //    return FMOD_Studio_EventInstance_SetVolume(rawPtr, volume);
        //}
        //public RESULT getPitch(out float pitch)
        //{
        //    return FMOD_Studio_EventInstance_GetPitch(rawPtr, out pitch);
        //}
        //public RESULT setPitch(float pitch)
        //{
        //    return FMOD_Studio_EventInstance_SetPitch(rawPtr, pitch);
        //}
        //public RESULT get3DAttributes(out _3D_ATTRIBUTES attributes)
        //{
        //    return FMOD_Studio_EventInstance_Get3DAttributes(rawPtr, out attributes);
        //}
        //public RESULT set3DAttributes(_3D_ATTRIBUTES attributes)
        //{
        //    return FMOD_Studio_EventInstance_Set3DAttributes(rawPtr, ref attributes);
        //}
        //public RESULT getProperty(EVENT_PROPERTY index, out float value)
        //{
        //    return FMOD_Studio_EventInstance_GetProperty(rawPtr, index, out value);
        //}
        //public RESULT setProperty(EVENT_PROPERTY index, float value)
        //{
        //    return FMOD_Studio_EventInstance_SetProperty(rawPtr, index, value);
        //}
        //public RESULT getPaused(out bool paused)
        //{
        //    return FMOD_Studio_EventInstance_GetPaused(rawPtr, out paused);
        //}
        //public RESULT setPaused(bool paused)
        //{
        //    return FMOD_Studio_EventInstance_SetPaused(rawPtr, paused);
        //}
        //public RESULT start()
        //{
        //    return FMOD_Studio_EventInstance_Start(rawPtr);
        //}
        //public RESULT stop(STOP_MODE mode)
        //{
        //    return FMOD_Studio_EventInstance_Stop(rawPtr, mode);
        //}
        //public RESULT getTimelinePosition(out int position)
        //{
        //    return FMOD_Studio_EventInstance_GetTimelinePosition(rawPtr, out position);
        //}
        //public RESULT setTimelinePosition(int position)
        //{
        //    return FMOD_Studio_EventInstance_SetTimelinePosition(rawPtr, position);
        //}
        //public RESULT getPlaybackState(out PLAYBACK_STATE state)
        //{
        //    return FMOD_Studio_EventInstance_GetPlaybackState(rawPtr, out state);
        //}
        //public RESULT getChannelGroup(out FMOD.ChannelGroup group)
        //{
        //    group = null;

        //    IntPtr groupraw = new IntPtr();
        //    RESULT result = FMOD_Studio_EventInstance_GetChannelGroup(rawPtr, out groupraw);
        //    if (result != RESULT.OK)
        //    {
        //        return result;
        //    }

        //    group = new FMOD.ChannelGroup(groupraw);

        //    return result;
        //}
        //public RESULT release()
        //{
        //    return FMOD_Studio_EventInstance_Release(rawPtr);
        //}
        //public RESULT isVirtual(out bool virtualState)
        //{
        //    return FMOD_Studio_EventInstance_IsVirtual(rawPtr, out virtualState);
        //}
        //public RESULT getParameter(string name, out ParameterInstance instance)
        //{
        //    instance = null;

        //    IntPtr newPtr = new IntPtr();
        //    RESULT result = FMOD_Studio_EventInstance_GetParameter(rawPtr, Encoding.UTF8.GetBytes(name + Char.MinValue), out newPtr);
        //    if (result != RESULT.OK)
        //    {
        //        return result;
        //    }
        //    instance = new ParameterInstance(newPtr);

        //    return result;
        //}
        //public RESULT getParameterCount(out int count)
        //{
        //    return FMOD_Studio_EventInstance_GetParameterCount(rawPtr, out count);
        //}
        //public RESULT getParameterByIndex(int index, out ParameterInstance instance)
        //{
        //    instance = null;

        //    IntPtr newPtr = new IntPtr();
        //    RESULT result = FMOD_Studio_EventInstance_GetParameterByIndex(rawPtr, index, out newPtr);
        //    if (result != RESULT.OK)
        //    {
        //        return result;
        //    }
        //    instance = new ParameterInstance(newPtr);

        //    return result;
        //}
        //public RESULT setParameterValue(string name, float value)
        //{
        //    return FMOD_Studio_EventInstance_SetParameterValue(rawPtr, Encoding.UTF8.GetBytes(name + Char.MinValue), value);
        //}
        //public RESULT setParameterValueByIndex(int index, float value)
        //{
        //    return FMOD_Studio_EventInstance_SetParameterValueByIndex(rawPtr, index, value);
        //}
        //public RESULT getCue(string name, out CueInstance instance)
        //{
        //    instance = null;

        //    IntPtr newPtr = new IntPtr();
        //    RESULT result = FMOD_Studio_EventInstance_GetCue(rawPtr, Encoding.UTF8.GetBytes(name + Char.MinValue), out newPtr);
        //    if (result != RESULT.OK)
        //    {
        //        return result;
        //    }
        //    instance = new CueInstance(newPtr);

        //    return result;
        //}
        //public RESULT getCueByIndex(int index, out CueInstance instance)
        //{
        //    instance = null;

        //    IntPtr newPtr = new IntPtr();
        //    RESULT result = FMOD_Studio_EventInstance_GetCueByIndex(rawPtr, index, out newPtr);
        //    if (result != RESULT.OK)
        //    {
        //        return result;
        //    }
        //    instance = new CueInstance(newPtr);

        //    return result;
        //}
        //public RESULT getCueCount(out int count)
        //{
        //    return FMOD_Studio_EventInstance_GetCueCount(rawPtr, out count);
        //}
        //public RESULT setCallback(EVENT_CALLBACK callback)
        //{
        //    return FMOD_Studio_EventInstance_SetCallback(rawPtr, callback);
        //}
        //public RESULT getUserData(out IntPtr userData)
        //{
        //    return FMOD_Studio_EventInstance_GetUserData(rawPtr, out userData);
        //}
        //public RESULT setUserData(IntPtr userData)
        //{
        //    return FMOD_Studio_EventInstance_SetUserData(rawPtr, userData);
        //}
    }

    public static class EventInstanceExtensions
    {
        public static FMOD.Studio.EventInstance ToFmod(this EventInstance evtInst)
        {
            if (evtInst == null)
                return null;

            return evtInst.FmodEventInstance;
        }
    }
}
