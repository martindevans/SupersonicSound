using System;
using FMOD.Studio;
using SupersonicSound.LowLevel;
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

            _parameterCollection = new ParameterCollection(this);
            _propertyCollection = new PropertyCollection(this);
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

        public Attributes3D Attributes3D
        {
            get
            {
                _3D_ATTRIBUTES attr;
                FmodEventInstance.get3DAttributes(out attr).Check();
                return new Attributes3D(attr);
            }
            set
            {
                FmodEventInstance.set3DAttributes(value.ToFmod());
            }
        }

        public struct PropertyCollection
        {
            private readonly EventInstance _eventInstance;

            public float this[EventProperty index]
            {
                get
                {
                    float value;
                    _eventInstance.FmodEventInstance.getProperty((EVENT_PROPERTY) index, out value).Check();
                    return value;
                }
                set
                {
                    _eventInstance.FmodEventInstance.setProperty((EVENT_PROPERTY)index, value);
                }
            }

            public PropertyCollection(EventInstance instance)
            {
                _eventInstance = instance;
            }
        }

        private readonly PropertyCollection _propertyCollection;
        public PropertyCollection Properties
        {
            get
            {
                return _propertyCollection;
            }
        }

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

        public int TimelinePosition
        {
            get
            {
                int pos;
                FmodEventInstance.getTimelinePosition(out pos).Check();
                return pos;
            }
            set
            {
                FmodEventInstance.setTimelinePosition(value).Check();
            }
        }

        public PlaybackState PlaybackState
        {
            get
            {
                PLAYBACK_STATE state;
                FmodEventInstance.getPlaybackState(out state).Check();
                return (PlaybackState)state;
            }
        }

        public ChannelGroup Group
        {
            get
            {
                FMOD.ChannelGroup group;
                FmodEventInstance.getChannelGroup(out group).Check();
                return ChannelGroup.FromFmod(group);
            }
        }

        public void Release()
        {
            FmodEventInstance.release();
        }

        public bool IsVirtual
        {
            get
            {
                bool virt;
                FmodEventInstance.isVirtual(out virt).Check();
                return virt;
            }
        }

        public struct ParameterCollection
        {
            private readonly EventInstance _eventInstance;

            public int Count
            {
                get
                {
                    int count;
                    _eventInstance.FmodEventInstance.getParameterCount(out count).Check();
                    return count;
                }
            }

            public ParameterInstance this[string name]
            {
                get
                {
                    FMOD.Studio.ParameterInstance instance;
                    _eventInstance.FmodEventInstance.getParameter(name, out instance).Check();
                    return ParameterInstance.FromFmod(instance);
                }
            }

            public ParameterInstance this[int index]
            {
                get
                {
                    FMOD.Studio.ParameterInstance instance;
                    _eventInstance.FmodEventInstance.getParameterByIndex(index, out instance).Check();
                    return ParameterInstance.FromFmod(instance);
                }
            }

            public ParameterCollection(EventInstance instance)
            {
                _eventInstance = instance;
            }

            public void SetValue(string name, float value)
            {
                _eventInstance.FmodEventInstance.setParameterValue(name, value).Check();
            }

            public void SetValue(int index, float value)
            {
                _eventInstance.FmodEventInstance.setParameterValueByIndex(index, value).Check();
            }
        }

        private readonly ParameterCollection _parameterCollection;
        public ParameterCollection Parameters
        {
            get
            {
                return _parameterCollection;
            }
        }

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

        public int CueCount
        {
            get
            {
                int count;
                FmodEventInstance.getCueCount(out count).Check();
                return count;
            }
        }

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
