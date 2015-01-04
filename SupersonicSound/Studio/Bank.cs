using FMOD;
using FMOD.Studio;
using SupersonicSound.Wrapper;
using System;

namespace SupersonicSound.Studio
{
    public class Bank
        : IEquatable<Bank>
    {
        public FMOD.Studio.Bank FmodBank { get; private set; }

        public Bank(FMOD.Studio.Bank bank)
        {
            FmodBank = bank;
        }

        #region equality
        public bool Equals(Bank other)
        {
            if (other == null)
                return false;

            return other.FmodBank == FmodBank;
        }

        public override bool Equals(object obj)
        {
            var c = obj as Bank;
            if (c == null)
                return false;

            return Equals(c);
        }

        public override int GetHashCode()
        {
            return (FmodBank != null ? FmodBank.GetHashCode() : 0);
        }
        #endregion

        #region Property access
        public Guid Id
        {
            get
            {
                GUID id;
                FmodBank.getID(out id);
                return id.FromFmod();
            }
        }

        public string Path
        {
            get
            {
                string path;
                FmodBank.getPath(out path).Check();
                return path;
            }
        }

        public void Unload()
        {
            FmodBank.unload();
        }

        public void LoadSampleData()
        {
            FmodBank.loadSampleData();
        }

        public void UnloadSampleData()
        {
            FmodBank.unloadSampleData();
        }

        public LoadingState GetLoadingState()
        {
            LOADING_STATE state;
            FmodBank.getLoadingState(out state).Check();
            return (LoadingState)state;
        }

        public LoadingState GetSampleLoadingState()
        {
            LOADING_STATE state;
            FmodBank.getSampleLoadingState(out state).Check();
            return (LoadingState)state;
        }
        #endregion

        #region Enumeration
        public int StringCount
        {
            get
            {
                int count;
                FmodBank.getStringCount(out count).Check();
                return count;
            }
        }

        public string GetStringInfo(int index, out Guid guid)
        {
            GUID id;
            string path;
            FmodBank.getStringInfo(index, out id, out path).Check();

            guid = id.FromFmod();
            return path;
        }

        public int EventCount
        {
            get
            {
                int count;
                FmodBank.getEventCount(out count).Check();
                return count;
            }
        }

        //public RESULT getEventList(out EventDescription[] array)
        //{
        //    array = null;

        //    RESULT result;
        //    int capacity;
        //    result = FMOD_Studio_Bank_GetEventCount(rawPtr, out capacity);
        //    if (result != RESULT.OK)
        //    {
        //        return result;
        //    }
        //    if (capacity == 0)
        //    {
        //        array = new EventDescription[0];
        //        return result;
        //    }

        //    IntPtr[] rawArray = new IntPtr[capacity];
        //    int actualCount;
        //    result = FMOD_Studio_Bank_GetEventList(rawPtr, rawArray, capacity, out actualCount);
        //    if (result != RESULT.OK)
        //    {
        //        return result;
        //    }
        //    if (actualCount > capacity) // More items added since we queried just now?
        //    {
        //        actualCount = capacity;
        //    }
        //    array = new EventDescription[actualCount];
        //    for (int i = 0; i < actualCount; ++i)
        //    {
        //        array[i] = new EventDescription(rawArray[i]);
        //    }
        //    return RESULT.OK;
        //}

        public int BusCount
        {
            get
            {
                int count;
                FmodBank.getBusCount(out count).Check();
                return count;
            }
        }

        //public RESULT getBusList(out Bus[] array)
        //{
        //    array = null;

        //    RESULT result;
        //    int capacity;
        //    result = FMOD_Studio_Bank_GetBusCount(rawPtr, out capacity);
        //    if (result != RESULT.OK)
        //    {
        //        return result;
        //    }
        //    if (capacity == 0)
        //    {
        //        array = new Bus[0];
        //        return result;
        //    }

        //    IntPtr[] rawArray = new IntPtr[capacity];
        //    int actualCount;
        //    result = FMOD_Studio_Bank_GetBusList(rawPtr, rawArray, capacity, out actualCount);
        //    if (result != RESULT.OK)
        //    {
        //        return result;
        //    }
        //    if (actualCount > capacity) // More items added since we queried just now?
        //    {
        //        actualCount = capacity;
        //    }
        //    array = new Bus[actualCount];
        //    for (int i = 0; i < actualCount; ++i)
        //    {
        //        array[i] = new Bus(rawArray[i]);
        //    }
        //    return RESULT.OK;
        //}

        public int VCACount
        {
            get
            {
                int count;
                FmodBank.getVCACount(out count).Check();
                return count;
            }
        }

        //public RESULT getVCAList(out VCA[] array)
        //{
        //    array = null;

        //    RESULT result;
        //    int capacity;
        //    result = FMOD_Studio_Bank_GetVCACount(rawPtr, out capacity);
        //    if (result != RESULT.OK)
        //    {
        //        return result;
        //    }
        //    if (capacity == 0)
        //    {
        //        array = new VCA[0];
        //        return result;
        //    }

        //    IntPtr[] rawArray = new IntPtr[capacity];
        //    int actualCount;
        //    result = FMOD_Studio_Bank_GetVCAList(rawPtr, rawArray, capacity, out actualCount);
        //    if (result != RESULT.OK)
        //    {
        //        return result;
        //    }
        //    if (actualCount > capacity) // More items added since we queried just now?
        //    {
        //        actualCount = capacity;
        //    }
        //    array = new VCA[actualCount];
        //    for (int i = 0; i < actualCount; ++i)
        //    {
        //        array[i] = new VCA(rawArray[i]);
        //    }
        //    return RESULT.OK;
        //}
        #endregion
    }

    public static class BankExtensions
    {
        public static FMOD.Studio.Bank ToFmod(this Bank bank)
        {
            if (bank == null)
                return null;

            return bank.FmodBank;
        }
    }
}
