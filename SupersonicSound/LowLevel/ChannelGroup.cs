using SupersonicSound.Wrapper;
using System;
using System.Text;

namespace SupersonicSound.LowLevel
{
    public struct ChannelGroup
        : IEquatable<ChannelGroup>
    {
        public FMOD.ChannelGroup FmodGroup { get; private set; }

        private ChannelGroup(FMOD.ChannelGroup group)
            : this()
        {
            FmodGroup = group;
        }

        public static ChannelGroup FromFmod(FMOD.ChannelGroup group)
        {
            if (group == null)
                throw new ArgumentException("group");
            return new ChannelGroup(group);
        }

        #region equality
        public bool Equals(ChannelGroup other)
        {
            return other.FmodGroup == FmodGroup;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ChannelGroup))
                return false;

            return Equals((ChannelGroup)obj);
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
            return channelGroup.FmodGroup;
        }
    }
}
