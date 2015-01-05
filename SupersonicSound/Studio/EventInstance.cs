using System;
using FMOD.Studio;
using SupersonicSound.Wrapper;

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

        public EventDescription Description
        {
            get
            {
                FMOD.Studio.EventDescription desc;
                FmodEventInstance.getDescription(out desc).Check();
                return EventDescription.FromFmod(desc);
            }
        }

        public float Volume
        {
            get
            {
                float volume;
                FmodEventInstance.getVolume(out volume).Check();
                return volume;
            }
            set
            {
                FmodEventInstance.setVolume(value).Check();
            }
        }

        public float Pitch
        {
            get
            {
                float pitch;
                FmodEventInstance.getPitch(out pitch).Check();
                return pitch;
            }
            set
            {
                FmodEventInstance.setPitch(value).Check();
            }
        }

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

        public bool IsPaused
        {
            get
            {
                bool paused;
                FmodEventInstance.getPaused(out paused).Check();
                return paused;
            }
            set
            {
                FmodEventInstance.setPaused(value).Check();
            }
        }

        public void Start()
        {
            FmodEventInstance.start().Check();
        }

        public void Stop(bool allowFadeout)
        {
            FmodEventInstance.stop(allowFadeout ? STOP_MODE.ALLOWFADEOUT : STOP_MODE.IMMEDIATE).Check();
        }

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

        public void Release()
        {
            FmodEventInstance.release();
        }

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

        public void SetParameterValue(string name, float value)
        {
            FmodEventInstance.setParameterValue(name, value).Check();
        }

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

        public IntPtr UserData
        {
            get
            {
                IntPtr ptr;
                FmodEventInstance.getUserData(out ptr).Check();
                return ptr;
            }
            set
            {
                FmodEventInstance.setUserData(value).Check();
            }
        }
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
