
using System;
using SupersonicSound.Wrapper;

namespace SupersonicSound.LowLevel
{
    public class Channel
        : IEquatable<Channel>
    {
        public FMOD.Channel FmodChannel { get; private set; }

        public Channel(FMOD.Channel channel)
        {
            FmodChannel = channel;
        }

        #region equality
        public bool Equals(Channel other)
        {
            if (other == null)
                return false;

            return other.FmodChannel == FmodChannel;
        }

        public override bool Equals(object obj)
        {
            var c = obj as Channel;
            if (c == null)
                return false;

            return Equals(c);
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
        //public RESULT isVirtual(out bool isvirtual)
        //{
        //    return FMOD5_Channel_IsVirtual(getRaw(), out isvirtual);
        //}
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
            if (channel == null)
                return null;

            return channel.FmodChannel;
        }
    }
}
