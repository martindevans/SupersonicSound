using System.Collections.Generic;
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

        private CallbackHandler _callbackHandler;

        private bool _throwHandle;
        public bool SuppressInvalidHandle
        {
            get { return !_throwHandle; }
            set { _throwHandle = !value; }
        }

        private bool _throwStolen;
        public bool SuppressChannelStolen
        {
            get { return !_throwStolen; }
            set { _throwStolen = !value; }
        }

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

        private IReadOnlyList<RESULT> Suppressions()
        {
            return ErrorChecking.Suppress(_throwHandle, _throwStolen);
        }

        //public bool IsValid()
        //{
        //    return FmodGroup.isValid();
        //}

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
            FmodGroup.addGroup(group.FmodGroup).Check(Suppressions());
        }

        public int? GroupCount
        {
            get
            {
                int groups;
                return FmodGroup.getNumGroups(out groups).CheckBox(groups, Suppressions());
            }
        }

        public ChannelGroup? GetGroup(int index)
        {
            FMOD.ChannelGroup group;
            bool ok = FmodGroup.getGroup(index, out group).Check(Suppressions());
            return ok ? new ChannelGroup(group) : (ChannelGroup?)null;
        }

        public ChannelGroup? ParentGroup
        {
            get
            {
                FMOD.ChannelGroup group;
                bool ok = FmodGroup.getParentGroup(out group).Check(Suppressions());
                return ok ? new ChannelGroup(group) : (ChannelGroup?)null;
            }
        }
        #endregion

        #region Information only functions.
        public string Name
        {
            get
            {
                StringBuilder builder = new StringBuilder(128);
                bool ok = FmodGroup.getName(builder, builder.Capacity).Check(Suppressions());
                if (!ok)
                    return null;

                return builder.ToString();
            }
        }

        public int? ChannelCount
        {
            get
            {
                int channels;
                return FmodGroup.getNumChannels(out channels).CheckBox(channels, Suppressions());
            }
        }

        public Channel? GetChannel(int index)
        {
            FMOD.Channel channel;
            bool ok = FmodGroup.getChannel(index, out channel).Check(Suppressions());

            return ok ? Channel.FromFmod(channel) : (Channel?)null;
        }
        #endregion

        #region IChannelControl
        public void Stop()
        {
            FmodGroup.stop().Check(ErrorChecking.SuppressInvalidHandle);
        }

        public bool? Pause
        {
            get
            {
                bool value;
                return FmodGroup.getPaused(out value).CheckBox(value, Suppressions());
            }
            set
            {
                FmodGroup.setPaused(value.Unbox()).Check(Suppressions());
            }
        }

        public float? Volume
        {
            get
            {
                float value;
                return FmodGroup.getVolume(out value).CheckBox(value, Suppressions());
            }
            set
            {
                FmodGroup.setVolume(value.Unbox()).Check(Suppressions());
            }
        }

        public bool? VolumeRamp
        {
            get
            {
                bool value;
                return FmodGroup.getVolumeRamp(out value).CheckBox(value, Suppressions());
            }
            set
            {
                FmodGroup.setVolumeRamp(value.Unbox()).Check(Suppressions());
            }
        }

        public float? Audibility
        {
            get
            {
                float value;
                return FmodGroup.getAudibility(out value).CheckBox(value, Suppressions());
            }
        }

        public float? Pitch
        {
            get
            {
                float value;
                return FmodGroup.getPitch(out value).CheckBox(value, Suppressions());
            }
            set
            {
                FmodGroup.setPitch(value.Unbox()).Check(Suppressions());
            }
        }

        public bool? Mute
        {
            get
            {
                bool value;
                return FmodGroup.getMute(out value).CheckBox(value, Suppressions());
            }
            set
            {
                FmodGroup.setMute(value.Unbox()).Check(Suppressions());
            }
        }

        public float Pan
        {
            set
            {
                FmodGroup.setPan(value).Check(Suppressions());
            }
        }

        public bool? IsPlaying
        {
            get
            {
                bool value;
                return FmodGroup.isPlaying(out value).Check(Suppressions());
            }
        }

        public Mode? Mode
        {
            get
            {
                MODE value;
                var nMode = FmodGroup.getMode(out value).CheckBox(value, Suppressions());

                return nMode.HasValue ? EquivalentEnum<MODE, Mode>.Cast(nMode.Value) : (Mode?)null;
            }
            set
            {
                FmodGroup.setMode(EquivalentEnum<Mode, MODE>.Cast(value.Unbox())).Check(Suppressions());
            }
        }

        public float? LowPassGain
        {
            get
            {
                float value;
                return FmodGroup.getLowPassGain(out value).CheckBox(value, Suppressions());
            }
            set
            {
                FmodGroup.setLowPassGain(value.Unbox()).Check(Suppressions());
            }
        }
        #endregion

        #region Callback functions
        public void SetCallback(Action<ChannelControlCallbackType, IntPtr, IntPtr> callback)
        {
            if (_callbackHandler == null)
                _callbackHandler = new CallbackHandler(FmodGroup);
            _callbackHandler.SetCallback(callback);
        }

        public void RemoveCallback()
        {
            _callbackHandler.RemoveCallback();
        }
        #endregion
    }
}
