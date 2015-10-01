
using System;
using FMOD;
using SupersonicSound.Wrapper;

namespace SupersonicSound.LowLevel
{
    public struct Channel
        : IEquatable<Channel>, IChannelControl
    {
        public FMOD.Channel FmodChannel { get; private set; }

        private Channel(FMOD.Channel channel)
            : this()
        {
            FmodChannel = channel;
        }

        public static Channel FromFmod(FMOD.Channel channel)
        {
            if (channel == null)
                throw new ArgumentNullException("channel");
            return new Channel(channel);
        }

        #region equality
        public bool Equals(Channel other)
        {
            return other.FmodChannel == FmodChannel;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Channel))
                return false;

            return Equals((Channel)obj);
        }

        public override int GetHashCode()
        {
            return (FmodChannel != null ? FmodChannel.GetHashCode() : 0);
        }
        #endregion

        #region Channel specific control functionality
        public float Frequency
        {
            get
            {
                float freq;
                FmodChannel.getFrequency(out freq).Check();
                return freq;
            }
            set
            {
                FmodChannel.setFrequency(value).Check();
            }
        }

        public int Priority
        {
            get
            {
                int priority;
                FmodChannel.getPriority(out priority).Check();
                return priority;
            }
            set
            {
                FmodChannel.setPriority(value).Check();
            }
        }

        public void SetPosition(uint position, TimeUnit unit)
        {
            FmodChannel.setPosition(position, (TIMEUNIT)unit).Check();
        }

        public uint GetPosition(TimeUnit unit)
        {
            uint pos;
            FmodChannel.getPosition(out pos, (TIMEUNIT)unit).Check();
            return pos;
        }

        public ChannelGroup ChannelGroup
        {
            get
            {
                FMOD.ChannelGroup group;
                FmodChannel.getChannelGroup(out group).Check();
                return ChannelGroup.FromFmod(group);
            }
            set
            {
                FmodChannel.setChannelGroup(value.FmodGroup).Check();
            }
        }

        public int LoopCount
        {
            get
            {
                int count;
                FmodChannel.getLoopCount(out count).Check();
                return count;
            }
            set
            {
                FmodChannel.setLoopCount(value).Check();
            }
        }

        public void SetLoopPoints(uint start, TimeUnit startUnit, uint end, TimeUnit endUnit)
        {
            FmodChannel.setLoopPoints(start, (TIMEUNIT)startUnit, end, (TIMEUNIT)endUnit);
        }

        public void GetLoopPoints(out uint start, TimeUnit startUnit, out uint end, TimeUnit endUnit)
        {
            FmodChannel.getLoopPoints(out start, (TIMEUNIT)startUnit, out end, (TIMEUNIT)endUnit);
        }

        public void Stop()
        {
            FmodChannel.stop().Check();
        }

        public bool Pause
        {
            get
            {
                bool value;
                FmodChannel.getPaused(out value).Check();

                return value;
            }
            set
            {
                FmodChannel.setPaused(value).Check();
            }
        }

        public float Volume
        {
            get
            {
                float value;
                FmodChannel.getVolume(out value).Check();
                return value;
            }
            set
            {
                FmodChannel.setVolume(value).Check();
            }
        }

        public bool VolumeRamp
        {
            get
            {
                bool value;
                FmodChannel.getVolumeRamp(out value).Check();
                return value;
            }
            set
            {
                FmodChannel.setVolumeRamp(value).Check();
            }
        }

        public float Audibility
        {
            get
            {
                float value;
                FmodChannel.getAudibility(out value).Check();
                return value;
            }
        }

        public float Pitch
        {
            get
            {
                float value;
                FmodChannel.getPitch(out value).Check();
                return value;
            }
            set
            {
                FmodChannel.setPitch(value).Check();
            }
        }

        public bool Mute
        {
            get
            {
                bool value;
                FmodChannel.getMute(out value).Check();

                return value;
            }
            set
            {
                FmodChannel.setMute(value).Check();
            }
        }

        public float Pan
        {
            set
            {
                FmodChannel.setPan(value).Check();
            }
        }

        public bool IsPlaying
        {
            get
            {
                bool value;
                FmodChannel.isPlaying(out value).Check();

                return value;
            }
        }

        public Mode Mode
        {
            get
            {
                MODE value;
                FmodChannel.getMode(out value).Check();
                return (Mode)value;
            }
            set
            {
                FmodChannel.setMode((MODE)value).Check();
            }
        }

        public float LowPassGain
        {
            get
            {
                float value;
                FmodChannel.getLowPassGain(out value).Check();
                return value;
            }
            set
            {
                FmodChannel.setLowPassGain(value).Check();
            }
        }
        #endregion

        #region Information only functions
        public bool IsVirtual
        {
            get
            {
                bool virt;
                FmodChannel.isVirtual(out virt).Check();
                return virt;
            }
        }

        public Sound CurrentSound
        {
            get
            {
                FMOD.Sound sound;
                FmodChannel.getCurrentSound(out sound).Check();
                return Sound.FromFmod(sound);
            }
        }

        public int Index
        {
            get
            {
                int index;
                FmodChannel.getIndex(out index).Check();
                return index;
            }
        }
        #endregion

        public void SetCallback(Action<ChannelControlCallbackType, IntPtr, IntPtr> callback)
        {
            FmodChannel.setCallback((channelraw, controltype, type, commanddata1, commanddata2) =>
            {
                callback((ChannelControlCallbackType)type, commanddata1, commanddata2);

                return RESULT.OK;
            }).Check();
        }
    }
}
