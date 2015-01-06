using System;
using System.Runtime.InteropServices;
using FMOD.Studio;
using SupersonicSound.LowLevel;
using SupersonicSound.Wrapper;

namespace SupersonicSound.Studio
{
    [StructLayout(LayoutKind.Explicit)]
    public struct EventInstance
        : IEquatable<EventInstance>
    {
        // This trickery deserved some explanation!
        // FieldOffset sets the position of the field in the struct by bytes, notice all four of the first fields are in the *same place*
        // The first field in the three collections are also FMOD.Studio.EventDescription, so those fields have the same value as the field just below
        // This saves us having pointers inside the collections, which saves 12 (x86) or 24 (x64) bytes. Pretty important inside a struct!

        [FieldOffset(0)]
        private readonly FMOD.Studio.EventInstance _fmodEventInstance;

        [FieldOffset(0)]
        private readonly ParameterCollection _parameterCollection;

        [FieldOffset(0)]
        private readonly PropertyCollection _propertyCollection;

        [FieldOffset(0)]
        private readonly CueInstanceCollection _cues;

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

        #region equality
        public bool Equals(EventInstance other)
        {
            return other.FmodEventInstance == FmodEventInstance;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EventInstance))
                return false;

            return Equals((EventInstance)obj);
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

        [StructLayout(LayoutKind.Explicit)]
        public struct PropertyCollection
        {
            [FieldOffset(0)]
            private readonly FMOD.Studio.EventInstance _eventInstance;

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

        [StructLayout(LayoutKind.Explicit)]
        public struct ParameterCollection
        {
            [FieldOffset(0)]
            private readonly FMOD.Studio.EventInstance _eventInstance;

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
                return _parameterCollection;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CueInstanceCollection
        {
            [FieldOffset(0)]
            private readonly FMOD.Studio.EventInstance _instance;

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
                return _cues;
            }
        }

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
            return evtInst.FmodEventInstance;
        }
    }
}
