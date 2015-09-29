using System.Collections.Generic;
using System.Linq;
using FMOD;
using FMOD.Studio;
using SupersonicSound.Wrapper;
using System;

namespace SupersonicSound.Studio
{
    public struct Bank
        : IEquatable<Bank>
    {
        public FMOD.Studio.Bank FmodBank { get; private set; }

        private Bank(FMOD.Studio.Bank bank)
            : this()
        {
            FmodBank = bank;
        }

        public static Bank FromFmod(FMOD.Studio.Bank bank)
        {
            if (bank == null)
                throw new ArgumentNullException("bank");
            return new Bank(bank);
        }

        #region equality
        public bool Equals(Bank other)
        {
            return other.FmodBank == FmodBank;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Bank))
                return false;

            return Equals((Bank)obj);
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
                Guid id;
                FmodBank.getID(out id).Check();
                return id;
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
            FmodBank.unload().Check();
        }

        public void LoadSampleData()
        {
            FmodBank.loadSampleData().Check();
        }

        public void UnloadSampleData()
        {
            FmodBank.unloadSampleData().Check();
        }

        public LoadingState LoadingState
        {
            get
            {
                LOADING_STATE state;
                FmodBank.getLoadingState(out state).Check();
                return (LoadingState) state;
            }
        }

        public LoadingState SampleLoadingState
        {
            get
            {
                LOADING_STATE state;
                FmodBank.getSampleLoadingState(out state).Check();
                return (LoadingState) state;
            }
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
            string path;
            FmodBank.getStringInfo(index, out guid, out path).Check();

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

        public IEnumerable<EventDescription> Events
        {
            get
            {
                FMOD.Studio.EventDescription[] evts;
                FmodBank.getEventList(out evts).Check();
                return evts.Select(EventDescription.FromFmod);
            }
        }

        public int BusCount
        {
            get
            {
                int count;
                FmodBank.getBusCount(out count).Check();
                return count;
            }
        }

        public IEnumerable<Bus> Buses
        {
            get
            {
                FMOD.Studio.Bus[] buses;
                FmodBank.getBusList(out buses).Check();
                return buses.Select(Bus.FromFmod);
            }
        }

        public int VCACount
        {
            get
            {
                int count;
                FmodBank.getVCACount(out count).Check();
                return count;
            }
        }

        public IEnumerable<VCA> VCAs
        {
            get
            {
                FMOD.Studio.VCA[] vcas;
                FmodBank.getVCAList(out vcas).Check();
                return vcas.Select(VCA.FromFmod);
            }
        }
        #endregion
    }
}
