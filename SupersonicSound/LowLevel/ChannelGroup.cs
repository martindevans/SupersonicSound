
using System;

namespace SupersonicSound.LowLevel
{
    public class ChannelGroup
        : IEquatable<ChannelGroup>
    {
        public FMOD.ChannelGroup FmodGroup { get; private set; }

        public ChannelGroup(FMOD.ChannelGroup group)
        {
            FmodGroup = group;
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
        //public RESULT addGroup(ChannelGroup group)
        //{
        //    return FMOD5_ChannelGroup_AddGroup(getRaw(), group.getRaw());
        //}
        //public RESULT getNumGroups(out int numgroups)
        //{
        //    return FMOD5_ChannelGroup_GetNumGroups(getRaw(), out numgroups);
        //}
        //public RESULT getGroup(int index, out ChannelGroup group)
        //{
        //    group = null;

        //    IntPtr groupraw;
        //    RESULT result = FMOD5_ChannelGroup_GetGroup(getRaw(), index, out groupraw);
        //    group = new ChannelGroup(groupraw);

        //    return result;
        //}
        //public RESULT getParentGroup(out ChannelGroup group)
        //{
        //    group = null;

        //    IntPtr groupraw;
        //    RESULT result = FMOD5_ChannelGroup_GetParentGroup(getRaw(), out groupraw);
        //    group = new ChannelGroup(groupraw);

        //    return result;
        //}
        #endregion

        #region Information only functions.
        //public RESULT getName(StringBuilder name, int namelen)
        //{
        //    IntPtr stringMem = Marshal.AllocHGlobal(name.Capacity);

        //    RESULT result = FMOD5_ChannelGroup_GetName(getRaw(), stringMem, namelen);

        //    StringMarshalHelper.NativeToBuilder(name, stringMem);
        //    Marshal.FreeHGlobal(stringMem);

        //    return result;
        //}
        //public RESULT getNumChannels(out int numchannels)
        //{
        //    return FMOD5_ChannelGroup_GetNumChannels(getRaw(), out numchannels);
        //}
        //public RESULT getChannel(int index, out Channel channel)
        //{
        //    channel = null;

        //    IntPtr channelraw;
        //    RESULT result = FMOD5_ChannelGroup_GetChannel(getRaw(), index, out channelraw);
        //    channel = new Channel(channelraw);

        //    return result;
        //}
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
