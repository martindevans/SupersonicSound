using FMOD;
using SupersonicSound.Wrapper;
using System;
using System.Text;

namespace SupersonicSound.LowLevel
{
    public struct Sound
        : IEquatable<Sound>, IDisposable//, IHandle
    {
        private readonly FMOD.Sound _fmodSound;

        public FMOD.Sound FmodSound
        {
            get
            {
                return _fmodSound;
            }
        }

        private Sound(FMOD.Sound sound)
            : this()
        {
            _fmodSound = sound;
        }

        public static Sound FromFmod(FMOD.Sound sound)
        {
            if (sound == null)
                throw new ArgumentNullException("sound");
            return new Sound(sound);
        }

        //public bool IsValid()
        //{
        //    return FmodSound.isValid();
        //}

        public void Dispose()
        {
            FmodSound.release().Check();
        }

        #region equality
        public bool Equals(Sound other)
        {
            return other._fmodSound == _fmodSound;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Sound))
                return false;

            return Equals((Sound)obj);
        }

        public override int GetHashCode()
        {
            return (_fmodSound != null ? _fmodSound.GetHashCode() : 0);
        }
        #endregion

        #region Standard sound manipulation functions.
        //todo: implement locking on Sound
        //public RESULT @lock(uint offset, uint length, out IntPtr ptr1, out IntPtr ptr2, out uint len1, out uint len2)
        //{
        //    return FMOD5_Sound_Lock(rawPtr, offset, length, out ptr1, out ptr2, out len1, out len2);
        //}
        //public RESULT unlock(IntPtr ptr1, IntPtr ptr2, uint len1, uint len2)
        //{
        //    return FMOD5_Sound_Unlock(rawPtr, ptr1, ptr2, len1, len2);
        //}

        public void SetDefaults(float frequency, int priority)
        {
            _fmodSound.setDefaults(frequency, priority).Check();
        }

        public void GetDefaults(out float frequency, out int priority)
        {
            _fmodSound.getDefaults(out frequency, out priority);
        }

        public void Set3DMinMaxDistance(float min, float max)
        {
            _fmodSound.set3DMinMaxDistance(min, max).Check();
        }

        public void Get3DMinMaxDistance(out float min, out float max)
        {
            _fmodSound.get3DMinMaxDistance(out min, out max);
        }

        public void Set3DConeSettings(float insideConeAngle, float outsideConeAngle, float outsideVolume)
        {
            _fmodSound.set3DConeSettings(insideConeAngle, outsideConeAngle, outsideVolume).Check();
        }

        public void Get3DConeSettings(out float insideConeAngle, out float outsideConeAngle, out float outsideVolume)
        {
            _fmodSound.get3DConeSettings(out insideConeAngle, out outsideConeAngle, out outsideVolume).Check();
        }

        // todo: Test Custom3DRollOff *very* carefully
        // Especially consider what happens when GC happens
        //public Vector3[] Custom3DRollOff
        //{
        //    get
        //    {
        //        int count;
        //        IntPtr ptr;
        //        _fmodSound.get3DCustomRolloff(out ptr, out count).Check();

        //        Vector3[] result = new Vector3[count];

        //        unsafe
        //        {
        //            VECTOR* v0 = (VECTOR*)ptr.ToPointer();
        //            for (int i = 0; i < count; i++)
        //            {
        //                VECTOR v = *(v0 + i);
        //                result[i] = new Vector3(v);
        //            }
        //        }

        //        return result;
        //    }
        //    set
        //    {
        //        VECTOR[] v = new VECTOR[value.Length];
        //        for (int i = 0; i < v.Length; i++)
        //            v[i] = value[i].ToFmod();

        //        _fmodSound.set3DCustomRolloff(ref v[0], v.Length).Check();
        //    }
        //}

        public struct SubSoundCollection
        {
            private readonly FMOD.Sound _fmodSound;

            public SubSoundCollection(FMOD.Sound fmodSound)
                : this()
            {
                _fmodSound = fmodSound;
            }

            public Sound this[int index]
            {
                get
                {
                    FMOD.Sound sound;
                    _fmodSound.getSubSound(index, out sound).Check();
                    return FromFmod(sound);
                }
                set
                {
                    throw new NotImplementedException();
                    //FIXME This was removed from the FMOD C# wrapper
                    //_fmodSound.setSubSound(index, value._fmodSound).Check();
                }
            }

            public Sound Parent
            {
                get
                {
                    FMOD.Sound parent;
                    _fmodSound.getSubSoundParent(out parent).Check();
                    return FromFmod(parent);
                }
            }

            public int Count
            {
                get
                {
                    int num;
                    _fmodSound.getNumSubSounds(out num).Check();
                    return num;
                }
            }
        }

        public SubSoundCollection SubSound
        {
            get
            {
                return new SubSoundCollection(FmodSound);
            }
        }

        public string Name
        {
            get
            {
                StringBuilder builder = new StringBuilder(128);
                _fmodSound.getName(builder, builder.Capacity).Check();
                return builder.ToString();
            }
        }

        public uint GetLength(TimeUnit unit)
        {
            uint length;
            _fmodSound.getLength(out length, (TIMEUNIT)unit).Check();
            return length;
        }

        public void GetFormat(out SoundType type, out SoundFormat format, out int channels, out int bits)
        {
            SOUND_TYPE t;
            SOUND_FORMAT f;
            _fmodSound.getFormat(out t, out f, out channels, out bits).Check();

            type = (SoundType)t;
            format = (SoundFormat)f;
        }

        public struct TagCollection
        {
            private readonly FMOD.Sound _fmodSound;

            public TagCollection(FMOD.Sound fmodSound)
                : this()
            {
                _fmodSound = fmodSound;
            }

            public void Count(out int numTags, out int numTagsUpdated)
            {
                _fmodSound.getNumTags(out numTags, out numTagsUpdated).Check();
            }

            public Tag this[string name, int index]
            {
                get
                {
                    TAG tag;
                    _fmodSound.getTag(name, index, out tag).Check();
                    return new Tag(tag);
                }
            }
        }

        public TagCollection Tags
        {
            get
            {
                return new TagCollection(FmodSound);
            }
        }

        public void GetOpenState(out OpenState openState, out uint percentBuffered, out bool starving, out bool diskBusy)
        {
            OPENSTATE oState;
            _fmodSound.getOpenState(out oState, out percentBuffered, out starving, out diskBusy).Check();
            openState = (OpenState)oState;
        }

        public uint ReadData(byte[] buffer, uint length)
        {
            if (length > buffer.Length)
                throw new ArgumentOutOfRangeException("Length to read must be less than buffer length");

            unsafe
            {
                fixed (byte* ptr = &buffer[0])
                {
                    uint read;
                    _fmodSound.readData(new IntPtr(ptr), length, out read).Check();
                    return read;
                }
            }
        }

        public void SeekData(uint pcm)
        {
            _fmodSound.seekData(pcm).Check();
        }

        public SoundGroup SoundGroup
        {
            get
            {
                FMOD.SoundGroup group;
                _fmodSound.getSoundGroup(out group).Check();
                return new SoundGroup(group);
            }
            set
            {
                _fmodSound.setSoundGroup(value.FmodGroup).Check();
            }
        }
        #endregion

        #region Synchronization point API.  These points can come from markers embedded in wav files, and can also generate channel callbacks.
        public int SyncPointCount
        {
            get
            {
                int num;
                _fmodSound.getNumSyncPoints(out num).Check();
                return num;
            }
        }

        public struct SyncPoint
            : IEquatable<SyncPoint>
        {
            private readonly IntPtr _pointer;
            private readonly FMOD.Sound _sound;

            internal SyncPoint(IntPtr ptr, FMOD.Sound sound)
            {
                _pointer = ptr;
                _sound = sound;
            }

            #region equality
            public bool Equals(SyncPoint other)
            {
                return _pointer == other._pointer;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is SyncPoint))
                    return false;

                return Equals((SyncPoint)obj);
            }

            public override int GetHashCode()
            {
                // ReSharper disable once ImpureMethodCallOnReadonlyValueField
                return _pointer.GetHashCode();
            }
            #endregion

            public void GetInfo(TimeUnit offsetType, out uint offset, out string name)
            {
                StringBuilder n = new StringBuilder(128);
                _sound.getSyncPointInfo(_pointer, n, n.Capacity, out offset, (TIMEUNIT)offsetType).Check();

                name = n.ToString();
            }

            public void Delete()
            {
                _sound.deleteSyncPoint(_pointer).Check();
            }
        }

        public SyncPoint GetSyncPoint(int index)
        {
            IntPtr ptr;
            _fmodSound.getSyncPoint(index, out ptr).Check();
            return new SyncPoint(ptr, _fmodSound);
        }

        public SyncPoint AddSyncPoint(uint offset, TimeUnit unit, string name)
        {
            IntPtr ptr;
            _fmodSound.addSyncPoint(offset, (TIMEUNIT)unit, name, out ptr).Check();
            return new SyncPoint(ptr, _fmodSound);
        }
        #endregion

        #region Functions also in Channel class but here they are the 'default' to save having to change it in Channel all the time.
        public Mode Mode
        {
            get
            {
                MODE mode;
                _fmodSound.getMode(out mode).Check();
                return (Mode)mode;
            }
            set
            {
                _fmodSound.setMode((MODE)value).Check();
            }
        }

        public int LoopCount
        {
            get
            {
                int count;
                _fmodSound.getLoopCount(out count).Check();
                return count;
            }
            set
            {
                _fmodSound.setLoopCount(value).Check();
            }
        }

        public void SetLoopPoints(uint start, TimeUnit startUnit, uint end, TimeUnit endUnit)
        {
            _fmodSound.setLoopPoints(start, (TIMEUNIT)startUnit, end, (TIMEUNIT)endUnit);
        }

        public void GetLoopPoints(out uint start, TimeUnit startUnit, out uint end, TimeUnit endUnit)
        {
            _fmodSound.getLoopPoints(out start, (TIMEUNIT)startUnit, out end, (TIMEUNIT)endUnit);
        }
        #endregion

        #region For MOD/S3M/XM/IT/MID sequenced formats only.
        public struct ChannelCollection
        {
            private readonly FMOD.Sound _fmodSound;

            public ChannelCollection(FMOD.Sound fmodSound)
                : this()
            {
                _fmodSound = fmodSound;
            }

            public int Count
            {
                get
                {
                    int channels;
                    _fmodSound.getMusicNumChannels(out channels).Check();
                    return channels;
                }
            }

            public float GetVolume(int index)
            {
                float volume;
                _fmodSound.getMusicChannelVolume(index, out volume).Check();
                return volume;
            }

            public void SetVolume(int index, float volume)
            {
                _fmodSound.setMusicChannelVolume(index, volume).Check();
            }
        }

        public ChannelCollection MusicChannels
        {
            get
            {
                return new ChannelCollection(FmodSound);
            }
        }

        public float MusicSpeed
        {
            get
            {
                float speed;
                _fmodSound.getMusicSpeed(out speed).Check();
                return speed;
            }
            set
            {
                _fmodSound.setMusicSpeed(value).Check();
            }
        }
        #endregion

        #region Userdata set/get.
        public IntPtr UserData
        {
            get
            {
                IntPtr ptr;
                _fmodSound.getUserData(out ptr).Check();
                return ptr;
            }
            set
            {
                _fmodSound.setUserData(value).Check();
            }
        }
        #endregion
    }
}
