using FMOD;
using SupersonicSound.Wrapper;
using System;
using System.Text;

namespace SupersonicSound.LowLevel
{
    public struct ChannelGroup
        : IEquatable<ChannelGroup>, IChannelControl
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

        public bool IsValid()
        {
            return FmodGroup.isValid();
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
            FmodGroup.addGroup(group.FmodGroup).Check();
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

        #region IChannelControl
        public void Stop()
        {
            FmodGroup.stop().Check(Util.SuppressInvalidHandle);
        }

        public bool Pause
        {
            get
            {
                bool value;
                FmodGroup.getPaused(out value).Check();

                return value;
            }
            set
            {
                FmodGroup.setPaused(value).Check();
            }
        }

        public float Volume
        {
            get
            {
                float value;
                FmodGroup.getVolume(out value).Check();
                return value;
            }
            set
            {
                FmodGroup.setVolume(value).Check();
            }
        }

        public bool VolumeRamp
        {
            get
            {
                bool value;
                FmodGroup.getVolumeRamp(out value).Check();
                return value;
            }
            set
            {
                FmodGroup.setVolumeRamp(value).Check();
            }
        }

        public float Audibility
        {
            get
            {
                float value;
                FmodGroup.getAudibility(out value).Check();
                return value;
            }
        }

        public float Pitch
        {
            get
            {
                float value;
                FmodGroup.getPitch(out value).Check();
                return value;
            }
            set
            {
                FmodGroup.setPitch(value).Check();
            }
        }

        public bool Mute
        {
            get
            {
                bool value;
                FmodGroup.getMute(out value).Check();

                return value;
            }
            set
            {
                FmodGroup.setMute(value).Check();
            }
        }

        public float Pan
        {
            set
            {
                FmodGroup.setPan(value).Check();
            }
        }

        public bool IsPlaying
        {
            get
            {
                bool value;
                FmodGroup.isPlaying(out value).Check();

                return value;
            }
        }

        public Mode Mode
        {
            get
            {
                MODE value;
                FmodGroup.getMode(out value).Check();
                return (Mode)value;
            }
            set
            {
                FmodGroup.setMode((MODE)value).Check();
            }
        }

        public float LowPassGain
        {
            get
            {
                float value;
                FmodGroup.getLowPassGain(out value).Check();
                return value;
            }
            set
            {
                FmodGroup.setLowPassGain(value).Check();
            }
        }
        #endregion
    }
}
