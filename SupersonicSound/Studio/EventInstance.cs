using FMOD.Studio;
using SupersonicSound.LowLevel;
using SupersonicSound.Wrapper;
using System;

namespace SupersonicSound.Studio
{
    public struct EventInstance
        : IEquatable<EventInstance>, IHandle
    {
        private readonly FMOD.Studio.EventInstance _fmodEventInstance;

        public FMOD.Studio.EventInstance FmodEventInstance
        {
            get
            {
                return _fmodEventInstance;
            }
        }

        private EventInstance(FMOD.Studio.EventInstance evtInst)
            : this()
        {
            _fmodEventInstance = evtInst;
        }

        public static EventInstance FromFmod(FMOD.Studio.EventInstance evtInst)
        {
            return new EventInstance(evtInst);
        }

        public bool IsValid()
        {
            return FmodEventInstance.isValid();
        }

        #region equality
        public bool Equals(EventInstance other)
        {
            return other._fmodEventInstance == _fmodEventInstance;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EventInstance))
                return false;

            return Equals((EventInstance)obj);
        }

        public override int GetHashCode()
        {
            return (_fmodEventInstance != null ? _fmodEventInstance.GetHashCode() : 0);
        }
        #endregion

        public EventDescription Description
        {
            get
            {
                FMOD.Studio.EventDescription desc;
                _fmodEventInstance.getDescription(out desc).Check();
                return EventDescription.FromFmod(desc);
            }
        }

        public float Volume
        {
            get
            {
                float volume;
                _fmodEventInstance.getVolume(out volume).Check();
                return volume;
            }
            set
            {
                _fmodEventInstance.setVolume(value).Check();
            }
        }

        public float Pitch
        {
            get
            {
                float pitch;
                _fmodEventInstance.getPitch(out pitch).Check();
                return pitch;
            }
            set
            {
                _fmodEventInstance.setPitch(value).Check();
            }
        }

        public Attributes3D Attributes3D
        {
            get
            {
                _3D_ATTRIBUTES attr;
                _fmodEventInstance.get3DAttributes(out attr).Check();
                return new Attributes3D(ref attr);
            }
            set
            {
                _fmodEventInstance.set3DAttributes(value.ToFmod());
            }
        }

        public struct PropertyCollection
        {
            private readonly FMOD.Studio.EventInstance _eventInstance;

            public PropertyCollection(FMOD.Studio.EventInstance eventInstance)
                : this()
            {
                _eventInstance = eventInstance;
            }

            public float this[EventProperty index]
            {
                get
                {
                    float value;
                    _eventInstance.getProperty((EVENT_PROPERTY) index, out value).Check();
                    return value;
                }
                set
                {
                    _eventInstance.setProperty((EVENT_PROPERTY)index, value);
                }
            }
        }

        public PropertyCollection Properties
        {
            get
            {
                return new PropertyCollection(FmodEventInstance);
            }
        }

        public bool IsPaused
        {
            get
            {
                bool paused;
                _fmodEventInstance.getPaused(out paused).Check();
                return paused;
            }
            set
            {
                _fmodEventInstance.setPaused(value).Check();
            }
        }

        public void Start()
        {
            _fmodEventInstance.start().Check();
        }

        public void Stop(bool allowFadeout)
        {
            _fmodEventInstance.stop(allowFadeout ? STOP_MODE.ALLOWFADEOUT : STOP_MODE.IMMEDIATE).Check();
        }

        public int TimelinePosition
        {
            get
            {
                int pos;
                _fmodEventInstance.getTimelinePosition(out pos).Check();
                return pos;
            }
            set
            {
                _fmodEventInstance.setTimelinePosition(value).Check();
            }
        }

        public PlaybackState PlaybackState
        {
            get
            {
                PLAYBACK_STATE state;
                _fmodEventInstance.getPlaybackState(out state).Check();
                return (PlaybackState)state;
            }
        }

        public ChannelGroup Group
        {
            get
            {
                FMOD.ChannelGroup group;
                _fmodEventInstance.getChannelGroup(out group).Check();
                return ChannelGroup.FromFmod(group);
            }
        }

        public void Release()
        {
            _fmodEventInstance.release().Check();
        }

        public bool IsVirtual
        {
            get
            {
                bool virt;
                _fmodEventInstance.isVirtual(out virt).Check();
                return virt;
            }
        }

        public struct ParameterCollection
        {
            private readonly FMOD.Studio.EventInstance _eventInstance;

            public ParameterCollection(FMOD.Studio.EventInstance eventInstance)
                : this()
            {
                _eventInstance = eventInstance;
            }

            public int Count
            {
                get
                {
                    int count;
                    _eventInstance.getParameterCount(out count).Check();
                    return count;
                }
            }

            public ParameterInstance this[string name]
            {
                get
                {
                    FMOD.Studio.ParameterInstance instance;
                    _eventInstance.getParameter(name, out instance).Check();
                    return ParameterInstance.FromFmod(instance);
                }
            }

            public ParameterInstance this[int index]
            {
                get
                {
                    FMOD.Studio.ParameterInstance instance;
                    _eventInstance.getParameterByIndex(index, out instance).Check();
                    return ParameterInstance.FromFmod(instance);
                }
            }

            public void SetValue(string name, float value)
            {
                _eventInstance.setParameterValue(name, value).Check();
            }

            public void SetValue(int index, float value)
            {
                _eventInstance.setParameterValueByIndex(index, value).Check();
            }
        }

        public ParameterCollection Parameters
        {
            get
            {
                return new ParameterCollection(FmodEventInstance);
            }
        }

        public struct CueInstanceCollection
        {
            private readonly FMOD.Studio.EventInstance _instance;

            public CueInstanceCollection(FMOD.Studio.EventInstance instance)
                : this()
            {
                _instance = instance;
            }

            public CueInstance this[string name]
            {
                get
                {
                    FMOD.Studio.CueInstance cue;
                    _instance.getCue(name, out cue).Check();
                    return CueInstance.FromFmod(cue);
                }
            }

            public CueInstance this[int index]
            {
                get
                {
                    FMOD.Studio.CueInstance cue;
                    _instance.getCueByIndex(index, out cue).Check();
                    return CueInstance.FromFmod(cue);
                }
            }
        }

        public CueInstanceCollection Cues
        {
            get
            {
                return new CueInstanceCollection(FmodEventInstance);
            }
        }

        public int CueCount
        {
            get
            {
                int count;
                _fmodEventInstance.getCueCount(out count).Check();
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
                _fmodEventInstance.getUserData(out ptr).Check();
                return ptr;
            }
            set
            {
                _fmodEventInstance.setUserData(value).Check();
            }
        }
    }
}
