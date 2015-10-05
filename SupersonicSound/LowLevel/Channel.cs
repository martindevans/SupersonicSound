using FMOD;
using SupersonicSound.Wrapper;
using System;
using System.Collections.Generic;

namespace SupersonicSound.LowLevel
{
    public struct Channel
        : IEquatable<Channel>, IChannelControl
    {
        public FMOD.Channel FmodChannel { get; private set; }

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

        private IReadOnlyList<RESULT> Suppressions()
        {
            return ErrorChecking.Suppress(_throwHandle, _throwStolen);
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
        public float? Frequency
        {
            get
            {
                float freq;
                return FmodChannel.getFrequency(out freq).CheckBox(freq, Suppressions());
            }
            set
            {
                FmodChannel.setFrequency(value.Unbox()).Check(Suppressions());
            }
        }

        public int? Priority
        {
            get
            {
                int priority;
                return FmodChannel.getPriority(out priority).CheckBox(priority, Suppressions());
            }
            set
            {
                FmodChannel.setPriority(value.Unbox()).Check(Suppressions());
            }
        }

        public void SetPosition(uint position, TimeUnit unit)
        {
            FmodChannel.setPosition(position, (TIMEUNIT)unit).Check(Suppressions());
        }

        public uint? GetPosition(TimeUnit unit)
        {
            uint pos;
            return FmodChannel.getPosition(out pos, (TIMEUNIT)unit).CheckBox(pos, Suppressions());
        }

        public ChannelGroup? ChannelGroup
        {
            get
            {
                FMOD.ChannelGroup group;
                FmodChannel.getChannelGroup(out group).Check(Suppressions());
                if (group == null)
                    return null;
                else
                    return LowLevel.ChannelGroup.FromFmod(group);
            }
            set
            {
                var group = value.Unbox();
                FmodChannel.setChannelGroup(group.FmodGroup).Check(Suppressions());
            }
        }

        public int? LoopCount
        {
            get
            {
                int count;
                return FmodChannel.getLoopCount(out count).CheckBox(count, Suppressions());
            }
            set
            {
                FmodChannel.setLoopCount(value.Unbox()).Check(Suppressions());
            }
        }

        public void SetLoopPoints(uint start, TimeUnit startUnit, uint end, TimeUnit endUnit)
        {
            FmodChannel.setLoopPoints(start, (TIMEUNIT)startUnit, end, (TIMEUNIT)endUnit).Check(Suppressions());
        }

        public void GetLoopPoints(out uint? start, TimeUnit startUnit, out uint? end, TimeUnit endUnit)
        {
            uint startv;
            uint endv;
            bool ok = FmodChannel.getLoopPoints(out startv, (TIMEUNIT)startUnit, out endv, (TIMEUNIT)endUnit).Check(Suppressions());

            start = ok ? startv : (uint?)null;
            end = ok ? endv : (uint?)null;
        }

        public void Stop()
        {
            FmodChannel.stop().Check(Suppressions());
        }

        public bool? Pause
        {
            get
            {
                bool value;
                return FmodChannel.getPaused(out value).CheckBox(value, Suppressions());
            }
            set
            {
                FmodChannel.setPaused(value.Unbox()).Check(Suppressions());
            }
        }

        public float? Volume
        {
            get
            {
                float value;
                return FmodChannel.getVolume(out value).CheckBox(value, Suppressions());
            }
            set
            {
                FmodChannel.setVolume(value.Unbox()).Check(Suppressions());
            }
        }

        public bool? VolumeRamp
        {
            get
            {
                bool value;
                return FmodChannel.getVolumeRamp(out value).CheckBox(value, Suppressions());
            }
            set
            {
                FmodChannel.setVolumeRamp(value.Unbox()).Check(Suppressions());
            }
        }

        public float? Audibility
        {
            get
            {
                float value;
                return FmodChannel.getAudibility(out value).CheckBox(value, Suppressions());
            }
        }

        public float? Pitch
        {
            get
            {
                float value;
                return FmodChannel.getPitch(out value).CheckBox(value, Suppressions());
            }
            set
            {
                FmodChannel.setPitch(value.Unbox()).Check(Suppressions());
            }
        }

        public bool? Mute
        {
            get
            {
                bool value;
                return FmodChannel.getMute(out value).CheckBox(value, Suppressions());
            }
            set
            {
                FmodChannel.setMute(value.Unbox()).Check(Suppressions());
            }
        }

        public float Pan
        {
            set
            {
                FmodChannel.setPan(value).Check(Suppressions());
            }
        }

        public bool? IsPlaying
        {
            get
            {
                bool value;
                return FmodChannel.isPlaying(out value).Check(Suppressions());
            }
        }

        public Mode? Mode
        {
            get
            {
                MODE value;
                var nMode = FmodChannel.getMode(out value).CheckBox(value, Suppressions());

                return nMode.HasValue ? (Mode)nMode : (Mode?)null;
            }
            set
            {
                FmodChannel.setMode((MODE)value.Unbox()).Check(Suppressions());
            }
        }

        public float? LowPassGain
        {
            get
            {
                float value;
                return FmodChannel.getLowPassGain(out value).CheckBox(value, Suppressions());
            }
            set
            {
                FmodChannel.setLowPassGain(value.Unbox()).Check(Suppressions());
            }
        }
        #endregion

        #region Information only functions
        public bool? IsVirtual
        {
            get
            {
                bool virt;
                return FmodChannel.isVirtual(out virt).CheckBox(virt, Suppressions());
            }
        }

        public Sound? CurrentSound
        {
            get
            {
                FMOD.Sound sound;
                FmodChannel.getCurrentSound(out sound).Check(Suppressions());
                if (sound == null)
                    return null;
                else
                    return Sound.FromFmod(sound);
            }
        }

        public int? Index
        {
            get
            {
                int index;
                return FmodChannel.getIndex(out index).CheckBox(index, Suppressions());
            }
        }
        #endregion

        #region Callback functions
        public void SetCallback(Action<ChannelControlCallbackType, IntPtr, IntPtr> callback)
        {
            if (_callbackHandler == null)
                _callbackHandler = new CallbackHandler(FmodChannel);
            _callbackHandler.SetCallback(callback);
        }

        public void RemoveCallback()
        {
            _callbackHandler.RemoveCallback();
        }
        #endregion
    }
}
