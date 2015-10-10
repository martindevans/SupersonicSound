using FMOD;
using SupersonicSound.Wrapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupersonicSound.LowLevel
{
    public struct SoundGroup
        //: IHandle
    {
        private readonly FMOD.SoundGroup _fmodGroup;
        public FMOD.SoundGroup FmodGroup
        {
            get
            {
                return _fmodGroup;
            }
        }

        private bool _throwHandle;
        public bool SuppressInvalidHandle
        {
            get { return !_throwHandle; }
            set { _throwHandle = !value; }
        }

        public SoundGroup(FMOD.SoundGroup fmodSoundGroup)
            : this()
        {
            if (fmodSoundGroup == null)
                throw new ArgumentNullException(nameof(fmodSoundGroup));

            _fmodGroup = fmodSoundGroup;
        }

        private IReadOnlyList<RESULT> Suppressions()
        {
            return ErrorChecking.Suppress(_throwHandle, true);
        }

        //public bool IsValid()
        //{
        //    return FmodGroup.isValid();
        //}

        #region equality
        public bool Equals(SoundGroup other)
        {
            return other._fmodGroup == _fmodGroup;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SoundGroup))
                return false;

            return Equals((SoundGroup)obj);
        }

        public override int GetHashCode()
        {
            return (_fmodGroup != null ? _fmodGroup.GetHashCode() : 0);
        }
        #endregion

        public void Release()
        {
            _fmodGroup.release().Check(Suppressions());
        }

        //This is not implemented because it would be dangerous!
        //This would return *another* wrapper of the same low level system object, and when that was disposed it would release the low level object and break everything...
        //public LowLevelSystem System
        //{
        //    get
        //    {
        //        FMOD.System sys;
        //        _fmodGroup.getSystemObject(out sys).Check();
        //        return new LowLevelSystem(sys);
        //    }
        //}

        #region SoundGroup control functions.
        public int? MaxAudible
        {
            get
            {
                int max;
                return _fmodGroup.getMaxAudible(out max).CheckBox(max, Suppressions());
            }
            set
            {
                _fmodGroup.setMaxAudible(value.Unbox()).Check();
            }
        }

        public SoundGroupBehaviour? MaxAudibleBehaviour
        {
            get
            {
                SOUNDGROUP_BEHAVIOR b;
                if (!_fmodGroup.getMaxAudibleBehavior(out b).Check(Suppressions()))
                    return null;
                return EquivalentEnum<SOUNDGROUP_BEHAVIOR, SoundGroupBehaviour>.Cast(b);
            }
            set
            {
                _fmodGroup.setMaxAudibleBehavior(EquivalentEnum<SoundGroupBehaviour, SOUNDGROUP_BEHAVIOR>.Cast(value.Unbox())).Check();
            }
        }

        public float? MuteFadeSpeed
        {
            get
            {
                float speed;
                return _fmodGroup.getMuteFadeSpeed(out speed).CheckBox(speed, Suppressions());
            }
            set
            {
                _fmodGroup.setMuteFadeSpeed(value.Unbox()).Check(Suppressions());
            }
        }

        public float? Volume
        {
            get
            {
                float vol;
                return _fmodGroup.getVolume(out vol).CheckBox(vol, Suppressions());
            }
            set
            {
                _fmodGroup.setVolume(value.Unbox()).Check();
            }
        }

        public bool Stop()
        {
            return _fmodGroup.stop().Check(Suppressions());
        }
        #endregion

        #region Information only functions.
        public string Name
        {
            get
            {
                var builder = new StringBuilder(128);
                if (!_fmodGroup.getName(builder, builder.Capacity).Check(Suppressions()))
                    return null;
                return builder.ToString();
            }
        }

        public int? SoundCount
        {
            get
            {
                int num;
                return _fmodGroup.getNumSounds(out num).CheckBox(num, Suppressions());
            }
        }

        public Sound? GetSound(int index)
        {
            FMOD.Sound sound;
            if (!_fmodGroup.getSound(index, out sound).Check(Suppressions()))
                return null;
            return Sound.FromFmod(sound);
        }

        public int? SoundPlayingCount
        {
            get
            {
                int num;
                return _fmodGroup.getNumPlaying(out num).CheckBox(num, Suppressions());
            }
        }
        #endregion

        #region Userdata set/get.
        public IntPtr UserData
        {
            get
            {
                IntPtr ptr;
                _fmodGroup.getUserData(out ptr).Check();
                return ptr;
            }
            set
            {
                _fmodGroup.setUserData(value).Check();
            }
        }
        #endregion
    }
}
