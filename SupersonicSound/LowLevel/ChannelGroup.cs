using SupersonicSound.Wrapper;
using System;
using System.Text;

namespace SupersonicSound.LowLevel
{
    public class ChannelGroup
        : IEquatable<ChannelGroup>
    {
        public FMOD.ChannelGroup FmodGroup { get; private set; }

        private ChannelGroup(FMOD.ChannelGroup group)
        {
            FmodGroup = group;
        }

        public static ChannelGroup FromFmod(FMOD.ChannelGroup group)
        {
            if (group == null)
                return null;
            return new ChannelGroup(group);
        }

        #region equality
        public bool Equals(ChannelGroup other)
        {
            if (other == null)
                return false;

            return other.FmodGroup == FmodGroup;
        }

        public override bool Equals(object obj)
        {
            var c = obj as ChannelGroup;
            if (c == null)
                return false;

            return Equals(c);
        }

        public override int GetHashCode()
        {
            return (FmodGroup != null ? FmodGroup.GetHashCode() : 0);
        }
        #endregion

        #region Nested channel groups.
        public void AddChannelGroup(ChannelGroup group)
        {
            FmodGroup.addGroup(group.ToFmod()).Check();
        }

        public int GroupCount
        {
            get
            {
                int groups;
                FmodGroup.getNumGroups(out groups).Check();
                return groups;
            }
        }

        public ChannelGroup GetGroup(int index)
        {
            FMOD.ChannelGroup group;
            FmodGroup.getGroup(index, out group).Check();
            return new ChannelGroup(group);
        }

        public ChannelGroup ParentGroup
        {
            get
            {
                FMOD.ChannelGroup group;
                FmodGroup.getParentGroup(out group).Check();
                return new ChannelGroup(group);
            }
        }
        #endregion

        #region Information only functions.
        public string Name
        {
            get
            {
                StringBuilder builder = new StringBuilder(128);
                FmodGroup.getName(builder, builder.Capacity).Check();

                return builder.ToString();
            }
        }

        public int ChannelCount
        {
            get
            {
                int channels;
                FmodGroup.getNumChannels(out channels).Check();
                return channels;
            }
        }

        public Channel GetChannel(int index)
        {
            FMOD.Channel channel;
            FmodGroup.getChannel(index, out channel).Check();
            return Channel.FromFmod(channel);
        }
        #endregion
    }

    public static class ChannelGroupExtensions
    {
        public static FMOD.ChannelGroup ToFmod(this ChannelGroup channelGroup)
        {
            if (channelGroup == null)
                return null;

            return channelGroup.FmodGroup;
        }
    }
}
