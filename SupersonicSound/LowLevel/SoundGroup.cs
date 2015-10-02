using FMOD;
using SupersonicSound.Wrapper;
using System;
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

        public SoundGroup(FMOD.SoundGroup fmodSoundGroup)
        {
            if (fmodSoundGroup == null)
                throw new ArgumentNullException("fmodSoundGroup");

            _fmodGroup = fmodSoundGroup;
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
            _fmodGroup.release().Check();
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
        public int MaxAudible
        {
            get
            {
                int max;
                _fmodGroup.getMaxAudible(out max).Check();
                return max;
            }
            set
            {
                _fmodGroup.setMaxAudible(value).Check();
            }
        }

        public SoundGroupBehaviour MaxAudibleBehaviour
        {
            get
            {
                SOUNDGROUP_BEHAVIOR b;
                _fmodGroup.getMaxAudibleBehavior(out b).Check();
                return (SoundGroupBehaviour)b;
            }
            set
            {
                _fmodGroup.setMaxAudibleBehavior((SOUNDGROUP_BEHAVIOR)value).Check();
            }
        }

        public float MuteFadeSpeed
        {
            get
            {
                float speed;
                _fmodGroup.getMuteFadeSpeed(out speed).Check();
                return speed;
            }
            set
            {
                _fmodGroup.setMuteFadeSpeed(value).Check();
            }
        }

        public float Volume
        {
            get
            {
                float vol;
                _fmodGroup.getVolume(out vol).Check();
                return vol;
            }
            set
            {
                _fmodGroup.setVolume(value).Check();
            }
        }

        public void Stop()
        {
            _fmodGroup.stop().Check(Util.SuppressInvalidHandle);
        }
        #endregion

        #region Information only functions.
        public string Name
        {
            get
            {
                StringBuilder builder = new StringBuilder(128);
                _fmodGroup.getName(builder, builder.Capacity).Check();
                return builder.ToString();
            }
        }

        public int SoundCount
        {
            get
            {
                int num;
                _fmodGroup.getNumSounds(out num).Check();
                return num;
            }
        }

        public Sound GetSound(int index)
        {
            FMOD.Sound sound;
            _fmodGroup.getSound(index, out sound).Check();
            return Sound.FromFmod(sound);
        }

        public int SoundPlayingCount
        {
            get
            {
                int num;
                _fmodGroup.getNumPlaying(out num).Check();
                return num;
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
