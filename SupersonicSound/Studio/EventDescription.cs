using System;
using FMOD;
using FMOD.Studio;
using SupersonicSound.Wrapper;

namespace SupersonicSound.Studio
{
    public class EventDescription
        : IEquatable<EventDescription>
    {
        public FMOD.Studio.EventDescription FmodEventDescription { get; private set; }

        private EventDescription(FMOD.Studio.EventDescription evt)
        {
            FmodEventDescription = evt;
        }

        public static EventDescription FromFmod(FMOD.Studio.EventDescription evt)
        {
            if (evt == null)
                return null;
            return new EventDescription(evt);
        }

        #region equality
        public bool Equals(EventDescription other)
        {
            if (other == null)
                return false;

            return other.FmodEventDescription == FmodEventDescription;
        }

        public override bool Equals(object obj)
        {
            var c = obj as EventDescription;
            if (c == null)
                return false;

            return Equals(c);
        }

        public override int GetHashCode()
        {
            return (FmodEventDescription != null ? FmodEventDescription.GetHashCode() : 0);
        }
        #endregion

        public Guid Id
        {
            get
            {
                GUID id;
                FmodEventDescription.getID(out id).Check();
                return id.FromFmod();
            }
        }

        public string Path
        {
            get
            {
                string path;
                FmodEventDescription.getPath(out path).Check();
                return path;
            }
        }

        public int ParameterCount
        {
            get
            {
                int count;
                FmodEventDescription.getParameterCount(out count).Check();
                return count;
            }
        }

        //public RESULT getParameterByIndex(int index, out PARAMETER_DESCRIPTION parameter)
        //{
        //    parameter = new PARAMETER_DESCRIPTION();

        //    PARAMETER_DESCRIPTION_INTERNAL paramInternal;
        //    RESULT result = FMOD_Studio_EventDescription_GetParameterByIndex(rawPtr, index, out paramInternal);
        //    if (result != RESULT.OK)
        //    {
        //        return result;
        //    }
        //    paramInternal.assign(out parameter);
        //    return result;
        //}
        //public RESULT getParameter(string name, out PARAMETER_DESCRIPTION parameter)
        //{
        //    parameter = new PARAMETER_DESCRIPTION();

        //    PARAMETER_DESCRIPTION_INTERNAL paramInternal;
        //    RESULT result = FMOD_Studio_EventDescription_GetParameter(rawPtr, Encoding.UTF8.GetBytes(name + Char.MinValue), out paramInternal);
        //    if (result != RESULT.OK)
        //    {
        //        return result;
        //    }
        //    paramInternal.assign(out parameter);
        //    return result;
        //}
        //public RESULT getUserPropertyCount(out int count)
        //{
        //    return FMOD_Studio_EventDescription_GetUserPropertyCount(rawPtr, out count);
        //}
        //public RESULT getUserPropertyByIndex(int index, out USER_PROPERTY property)
        //{
        //    USER_PROPERTY_INTERNAL propertyInternal;

        //    RESULT result = FMOD_Studio_EventDescription_GetUserPropertyByIndex(rawPtr, index, out propertyInternal);
        //    if (result != RESULT.OK)
        //    {
        //        property = new USER_PROPERTY();
        //        return result;
        //    }

        //    property = propertyInternal.createPublic();

        //    return RESULT.OK;
        //}
        //public RESULT getUserProperty(string name, out USER_PROPERTY property)
        //{
        //    USER_PROPERTY_INTERNAL propertyInternal;

        //    RESULT result = FMOD_Studio_EventDescription_GetUserProperty(
        //        rawPtr, Encoding.UTF8.GetBytes(name + Char.MinValue), out propertyInternal);
        //    if (result != RESULT.OK)
        //    {
        //        property = new USER_PROPERTY();
        //        return result;
        //    }

        //    property = propertyInternal.createPublic();

        //    return RESULT.OK;
        //}
        //public RESULT getLength(out int length)
        //{
        //    return FMOD_Studio_EventDescription_GetLength(rawPtr, out length);
        //}
        //public RESULT getMinimumDistance(out float distance)
        //{
        //    return FMOD_Studio_EventDescription_GetMinimumDistance(rawPtr, out distance);
        //}
        //public RESULT getMaximumDistance(out float distance)
        //{
        //    return FMOD_Studio_EventDescription_GetMaximumDistance(rawPtr, out distance);
        //}
        //public RESULT isOneshot(out bool oneshot)
        //{
        //    return FMOD_Studio_EventDescription_IsOneshot(rawPtr, out oneshot);
        //}
        //public RESULT isStream(out bool isStream)
        //{
        //    return FMOD_Studio_EventDescription_IsStream(rawPtr, out isStream);
        //}
        //public RESULT is3D(out bool is3D)
        //{
        //    return FMOD_Studio_EventDescription_Is3D(rawPtr, out is3D);
        //}

        public EventInstance CreateInstance()
        {
            FMOD.Studio.EventInstance instance;
            FmodEventDescription.createInstance(out instance).Check();
            return EventInstance.FromFmod(instance);
        }

        //public RESULT createInstance(out EventInstance instance)
        //{
        //    instance = null;

        //    IntPtr newPtr = new IntPtr();
        //    RESULT result = FMOD_Studio_EventDescription_CreateInstance(rawPtr, out newPtr);
        //    if (result != RESULT.OK)
        //    {
        //        return result;
        //    }
        //    instance = new EventInstance(newPtr);
        //    return result;
        //}

        //public RESULT getInstanceCount(out int count)
        //{
        //    return FMOD_Studio_EventDescription_GetInstanceCount(rawPtr, out count);
        //}
        //public RESULT getInstanceList(out EventInstance[] array)
        //{
        //    array = null;

        //    RESULT result;
        //    int capacity;
        //    result = FMOD_Studio_EventDescription_GetInstanceCount(rawPtr, out capacity);
        //    if (result != RESULT.OK)
        //    {
        //        return result;
        //    }
        //    if (capacity == 0)
        //    {
        //        array = new EventInstance[0];
        //        return result;
        //    }

        //    IntPtr[] rawArray = new IntPtr[capacity];
        //    int actualCount;
        //    result = FMOD_Studio_EventDescription_GetInstanceList(rawPtr, rawArray, capacity, out actualCount);
        //    if (result != RESULT.OK)
        //    {
        //        return result;
        //    }
        //    if (actualCount > capacity) // More items added since we queried just now?
        //    {
        //        actualCount = capacity;
        //    }
        //    array = new EventInstance[actualCount];
        //    for (int i = 0; i < actualCount; ++i)
        //    {
        //        array[i] = new EventInstance(rawArray[i]);
        //    }
        //    return RESULT.OK;
        //}

        //public RESULT loadSampleData()
        //{
        //    return FMOD_Studio_EventDescription_LoadSampleData(rawPtr);
        //}

        //public RESULT unloadSampleData()
        //{
        //    return FMOD_Studio_EventDescription_UnloadSampleData(rawPtr);
        //}

        //public RESULT getSampleLoadingState(out LOADING_STATE state)
        //{
        //    return FMOD_Studio_EventDescription_GetSampleLoadingState(rawPtr, out state);
        //}

        //public RESULT releaseAllInstances()
        //{
        //    return FMOD_Studio_EventDescription_ReleaseAllInstances(rawPtr);
        //}
        //public RESULT setCallback(EVENT_CALLBACK callback)
        //{
        //    return FMOD_Studio_EventDescription_SetCallback(rawPtr, callback);
        //}

        public IntPtr UserData
        {
            get
            {
                IntPtr ptr;
                FmodEventDescription.getUserData(out ptr).Check();
                return ptr;
            }
            set
            {
                FmodEventDescription.setUserData(value).Check();
            }
        }
    }

    public static class EventDescriptionExtensions
    {
        public static FMOD.Studio.EventDescription ToFmod(this EventDescription evt)
        {
            if (evt == null)
                return null;

            return evt.FmodEventDescription;
        }
    }
}
