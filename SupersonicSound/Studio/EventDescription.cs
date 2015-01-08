using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using FMOD;
using FMOD.Studio;
using SupersonicSound.Wrapper;
using System;

namespace SupersonicSound.Studio
{
    [StructLayout(LayoutKind.Explicit)]
    public struct EventDescription
        : IEquatable<EventDescription>
    {
        // This trickery deserved some explanation!
        // FieldOffset sets the position of the field in the struct by bytes, notice all three of the first fields are in the *same place*
        // The first field in the two collections are also FMOD.Studio.EventDescription, so those fields have the same value as the field just below
        // This saves us having pointers inside the collections, which saves 8 (x86) or 16 (x64) bytes. Pretty important inside a struct!

        [FieldOffset(0)]
        private readonly FMOD.Studio.EventDescription _fmodEventDescription;

        [FieldOffset(0)]
        private readonly ParameterCollection _parameterCollection;

        [FieldOffset(0)]
        private readonly PropertyCollection _propertyCollection;

        public FMOD.Studio.EventDescription FmodEventDescription
        {
            get
            {
                return _fmodEventDescription;
            }
        }

        private EventDescription(FMOD.Studio.EventDescription evt)
            : this()
        {
            _fmodEventDescription = evt;
        }

        public static EventDescription FromFmod(FMOD.Studio.EventDescription evt)
        {
            return new EventDescription(evt);
        }

        #region equality
        public bool Equals(EventDescription other)
        {
            return other._fmodEventDescription == _fmodEventDescription;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EventDescription))
                return false;

            return Equals((EventDescription)obj);
        }

        public override int GetHashCode()
        {
            return (_fmodEventDescription != null ? _fmodEventDescription.GetHashCode() : 0);
        }
        #endregion

        public Guid Id
        {
            get
            {
                GUID id;
                _fmodEventDescription.getID(out id).Check();
                return id.FromFmod();
            }
        }

        public string Path
        {
            get
            {
                string path;
                _fmodEventDescription.getPath(out path).Check();
                return path;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ParameterCollection
        {
            private readonly FMOD.Studio.EventDescription _fmodEventDescription;

            public int Count
            {
                get
                {
                    int count;
                    _fmodEventDescription.getParameterCount(out count).Check();
                    return count;
                }
            }

            public ParameterDescription this[int index]
            {
                get
                {
                    PARAMETER_DESCRIPTION desc;
                    _fmodEventDescription.getParameterByIndex(index, out desc).Check();
                    return new ParameterDescription(desc);
                }
            }

            public ParameterDescription this[string name]
            {
                get
                {
                    PARAMETER_DESCRIPTION desc;
                    _fmodEventDescription.getParameter(name, out desc).Check();
                    return new ParameterDescription(desc);
                }
            }
        }

        public ParameterCollection Parameters
        {
            get
            {
                return _parameterCollection;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PropertyCollection
        {
            private readonly FMOD.Studio.EventDescription _fmodEventDescription;

            public int Count
            {
                get
                {
                    int count;
                    _fmodEventDescription.getUserPropertyCount(out count).Check();
                    return count;
                }
            }

            public UserProperty this[int index]
            {
                get
                {
                    USER_PROPERTY prop;
                    _fmodEventDescription.getUserPropertyByIndex(index, out prop).Check();
                    return new UserProperty(prop);
                }
            }

            public UserProperty this[string name]
            {
                get
                {
                    USER_PROPERTY prop;
                    _fmodEventDescription.getUserProperty(name, out prop).Check();
                    return new UserProperty(prop);
                }
            }
        }

        public PropertyCollection UserProperties
        {
            get
            {
                return _propertyCollection;
            }
        }

        public int Length
        {
            get
            {
                int length;
                _fmodEventDescription.getLength(out length).Check();
                return length;
            }
        }

        public float MinimumDistance
        {
            get
            {
                float max;
                _fmodEventDescription.getMinimumDistance(out max).Check();
                return max;
            }
        }

        public float MaximumDistance
        {
            get
            {
                float max;
                _fmodEventDescription.getMaximumDistance(out max).Check();
                return max;
            }
        }

        public bool IsOneshot
        {
            get
            {
                bool isOneshot;
                _fmodEventDescription.isOneshot(out isOneshot).Check();
                return isOneshot;
            }
        }

        public bool IsStream
        {
            get
            {
                bool isStream;
                _fmodEventDescription.isStream(out isStream).Check();
                return isStream;
            }
        }

        public bool Is3D
        {
            get
            {
                bool is3D;
                _fmodEventDescription.is3D(out is3D).Check();
                return is3D;
            }
        }

        public EventInstance CreateInstance()
        {
            FMOD.Studio.EventInstance instance;
            _fmodEventDescription.createInstance(out instance).Check();
            return EventInstance.FromFmod(instance);
        }

        public int InstanceCount
        {
            get
            {
                int count;
                _fmodEventDescription.getInstanceCount(out count).Check();
                return count;
            }
        }

        public IEnumerable<EventInstance> Instances
        {
            get
            {
                FMOD.Studio.EventInstance[] instances;
                _fmodEventDescription.getInstanceList(out instances).Check();
                return instances.Select(EventInstance.FromFmod);
            }
        }

        public void LoadSampleData()
        {
            _fmodEventDescription.loadSampleData();
        }

        public void UnloadSampleData()
        {
            _fmodEventDescription.unloadSampleData();
        }

        public LoadingState LoadingState
        {
            get
            {
                LOADING_STATE state;
                _fmodEventDescription.getSampleLoadingState(out state).Check();
                return (LoadingState)state;
            }
        }

        public void ReleaseAllInstances()
        {
            _fmodEventDescription.releaseAllInstances();
        }

        //public RESULT setCallback(EVENT_CALLBACK callback)
        //{
        //    return FMOD_Studio_EventDescription_SetCallback(rawPtr, callback);
        //}

        public IntPtr UserData
        {
            get
            {
                IntPtr ptr;
                _fmodEventDescription.getUserData(out ptr).Check();
                return ptr;
            }
            set
            {
                _fmodEventDescription.setUserData(value).Check();
            }
        }
    }
}
