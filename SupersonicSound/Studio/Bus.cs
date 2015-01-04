using System;
using FMOD;
using FMOD.Studio;
using SupersonicSound.Wrapper;

namespace SupersonicSound.Studio
{
    public class Bus
        : IEquatable<Bus>
    {
        public FMOD.Studio.Bus FmodBus { get; private set; }

        private Bus(FMOD.Studio.Bus bus)
        {
            FmodBus = bus;
        }

        public static Bus FromFmod(FMOD.Studio.Bus bus)
        {
            if (bus == null)
                return null;
            return new Bus(bus);
        }

        #region equality
        public bool Equals(Bus other)
        {
            if (other == null)
                return false;

            return other.FmodBus == FmodBus;
        }

        public override bool Equals(object obj)
        {
            var c = obj as Bus;
            if (c == null)
                return false;

            return Equals(c);
        }

        public override int GetHashCode()
        {
            return (FmodBus != null ? FmodBus.GetHashCode() : 0);
        }
        #endregion

        public Guid Id
        {
            get
            {
                GUID id;
                FmodBus.getID(out id).Check();
                return id.FromFmod();
            }
        }

        public string Path
        {
            get
            {
                string path;
                FmodBus.getPath(out path).Check();
                return path;
            }
        }

        public float FaderLevel
        {
            get
            {
                float volume;
                FmodBus.getFaderLevel(out volume).Check();
                return volume;
            }
            set
            {
                FmodBus.setFaderLevel(value).Check();
            }
        }

        public bool IsPaused
        {
            get
            {
                bool paused;
                FmodBus.getPaused(out paused).Check();
                return paused;
            }
            set
            {
                FmodBus.setPaused(value).Check();
            }
        }

        public bool IsMuted
        {
            get
            {
                bool mute;
                FmodBus.getMute(out mute).Check();
                return mute;
            }
            set
            {
                FmodBus.setMute(value).Check();
            }
        }

        public void StopAllEvents(bool allowFadeout)
        {
            FmodBus.stopAllEvents(allowFadeout ? STOP_MODE.ALLOWFADEOUT : STOP_MODE.IMMEDIATE).Check();
        }

        public void LockChannelGroup()
        {
            FmodBus.lockChannelGroup().Check();
        }

        public void UnlockChannelGroup()
        {
            FmodBus.unlockChannelGroup().Check();
        }

        public LowLevel.ChannelGroup ChannelGroup
        {
            get
            {
                ChannelGroup group;
                FmodBus.getChannelGroup(out group).Check();
                return LowLevel.ChannelGroup.FromFmod(group);
            }
        }
    }

    public static class BusExtensions
    {
        public static FMOD.Studio.Bus ToFmod(this Bus bus)
        {
            if (bus == null)
                return null;

            return bus.FmodBus;
        }
    }
}
