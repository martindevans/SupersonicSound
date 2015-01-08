
using System;
using FMOD;
using SupersonicSound.Wrapper;

namespace SupersonicSound.LowLevel
{
    public struct Channel
        : IEquatable<Channel>
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
    }
}
