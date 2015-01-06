
using System;
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

        //public RESULT setPosition(uint position, TIMEUNIT postype)
        //{
        //    return FMOD5_Channel_SetPosition(getRaw(), position, postype);
        //}
        //public RESULT getPosition(out uint position, TIMEUNIT postype)
        //{
        //    return FMOD5_Channel_GetPosition(getRaw(), out position, postype);
        //}
        //public RESULT setChannelGroup(ChannelGroup channelgroup)
        //{
        //    return FMOD5_Channel_SetChannelGroup(getRaw(), channelgroup.getRaw());
        //}
        //public RESULT getChannelGroup(out ChannelGroup channelgroup)
        //{
        //    channelgroup = null;

        //    IntPtr channelgroupraw;
        //    RESULT result = FMOD5_Channel_GetChannelGroup(getRaw(), out channelgroupraw);
        //    channelgroup = new ChannelGroup(channelgroupraw);

        //    return result;
        //}
        //public RESULT setLoopCount(int loopcount)
        //{
        //    return FMOD5_Channel_SetLoopCount(getRaw(), loopcount);
        //}
        //public RESULT getLoopCount(out int loopcount)
        //{
        //    return FMOD5_Channel_GetLoopCount(getRaw(), out loopcount);
        //}
        //public RESULT setLoopPoints(uint loopstart, TIMEUNIT loopstarttype, uint loopend, TIMEUNIT loopendtype)
        //{
        //    return FMOD5_Channel_SetLoopPoints(getRaw(), loopstart, loopstarttype, loopend, loopendtype);
        //}
        //public RESULT getLoopPoints(out uint loopstart, TIMEUNIT loopstarttype, out uint loopend, TIMEUNIT loopendtype)
        //{
        //    return FMOD5_Channel_GetLoopPoints(getRaw(), out loopstart, loopstarttype, out loopend, loopendtype);
        //}
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

        //public RESULT getCurrentSound(out Sound sound)
        //{
        //    sound = null;

        //    IntPtr soundraw;
        //    RESULT result = FMOD5_Channel_GetCurrentSound(getRaw(), out soundraw);
        //    sound = new Sound(soundraw);

        //    return result;
        //}

        //public RESULT getIndex(out int index)
        //{
        //    return FMOD5_Channel_GetIndex(getRaw(), out index);
        //}
        #endregion
    }

    public static class ChannelExtensions
    {
        public static FMOD.Channel ToFmod(this Channel channel)
        {
            return channel.FmodChannel;
        }
    }
}
