using System;
using System.Runtime.InteropServices;
using System.Text;
using FMOD;
using SupersonicSound.Wrapper;

namespace SupersonicSound.LowLevel
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Sound
        : IEquatable<Sound>
    {
        [FieldOffset(0)]
        private readonly FMOD.Sound _fmodSound;

        [FieldOffset(0)]
        private readonly ChannelCollection _musicChannelCollection;

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

        #region equality
        public bool Equals(Sound other)
        {
            return other.FmodSound == FmodSound;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Sound))
                return false;

            return Equals((Sound)obj);
        }

        public override int GetHashCode()
        {
            return (FmodSound != null ? FmodSound.GetHashCode() : 0);
        }
        #endregion

        #region Standard sound manipulation functions.
        //public RESULT @lock(uint offset, uint length, out IntPtr ptr1, out IntPtr ptr2, out uint len1, out uint len2)
        //{
        //    return FMOD5_Sound_Lock(rawPtr, offset, length, out ptr1, out ptr2, out len1, out len2);
        //}
        //public RESULT unlock(IntPtr ptr1, IntPtr ptr2, uint len1, uint len2)
        //{
        //    return FMOD5_Sound_Unlock(rawPtr, ptr1, ptr2, len1, len2);
        //}
        //public RESULT setDefaults(float frequency, int priority)
        //{
        //    return FMOD5_Sound_SetDefaults(rawPtr, frequency, priority);
        //}
        //public RESULT getDefaults(out float frequency, out int priority)
        //{
        //    return FMOD5_Sound_GetDefaults(rawPtr, out frequency, out priority);
        //}
        //public RESULT set3DMinMaxDistance(float min, float max)
        //{
        //    return FMOD5_Sound_Set3DMinMaxDistance(rawPtr, min, max);
        //}
        //public RESULT get3DMinMaxDistance(out float min, out float max)
        //{
        //    return FMOD5_Sound_Get3DMinMaxDistance(rawPtr, out min, out max);
        //}
        //public RESULT set3DConeSettings(float insideconeangle, float outsideconeangle, float outsidevolume)
        //{
        //    return FMOD5_Sound_Set3DConeSettings(rawPtr, insideconeangle, outsideconeangle, outsidevolume);
        //}
        //public RESULT get3DConeSettings(out float insideconeangle, out float outsideconeangle, out float outsidevolume)
        //{
        //    return FMOD5_Sound_Get3DConeSettings(rawPtr, out insideconeangle, out outsideconeangle, out outsidevolume);
        //}
        //public RESULT set3DCustomRolloff(ref VECTOR points, int numpoints)
        //{
        //    return FMOD5_Sound_Set3DCustomRolloff(rawPtr, ref points, numpoints);
        //}
        //public RESULT get3DCustomRolloff(out IntPtr points, out int numpoints)
        //{
        //    return FMOD5_Sound_Get3DCustomRolloff(rawPtr, out points, out numpoints);
        //}
        //public RESULT setSubSound(int index, Sound subsound)
        //{
        //    IntPtr subsoundraw = subsound.getRaw();

        //    return FMOD5_Sound_SetSubSound(rawPtr, index, subsoundraw);
        //}
        //public RESULT getSubSound(int index, out Sound subsound)
        //{
        //    subsound = null;

        //    IntPtr subsoundraw;
        //    RESULT result = FMOD5_Sound_GetSubSound(rawPtr, index, out subsoundraw);
        //    subsound = new Sound(subsoundraw);

        //    return result;
        //}
        //public RESULT getSubSoundParent(out Sound parentsound)
        //{
        //    parentsound = null;

        //    IntPtr subsoundraw;
        //    RESULT result = FMOD5_Sound_GetSubSoundParent(rawPtr, out subsoundraw);
        //    parentsound = new Sound(subsoundraw);

        //    return result;
        //}
        //public RESULT getName(StringBuilder name, int namelen)
        //{
        //    IntPtr stringMem = Marshal.AllocHGlobal(name.Capacity);

        //    RESULT result = FMOD5_Sound_GetName(rawPtr, stringMem, namelen);

        //    StringMarshalHelper.NativeToBuilder(name, stringMem);
        //    Marshal.FreeHGlobal(stringMem);

        //    return result;
        //}
        //public RESULT getLength(out uint length, TIMEUNIT lengthtype)
        //{
        //    return FMOD5_Sound_GetLength(rawPtr, out length, lengthtype);
        //}
        //public RESULT getFormat(out SOUND_TYPE type, out SOUND_FORMAT format, out int channels, out int bits)
        //{
        //    return FMOD5_Sound_GetFormat(rawPtr, out type, out format, out channels, out bits);
        //}
        //public RESULT getNumSubSounds(out int numsubsounds)
        //{
        //    return FMOD5_Sound_GetNumSubSounds(rawPtr, out numsubsounds);
        //}
        //public RESULT getNumTags(out int numtags, out int numtagsupdated)
        //{
        //    return FMOD5_Sound_GetNumTags(rawPtr, out numtags, out numtagsupdated);
        //}
        //public RESULT getTag(string name, int index, out TAG tag)
        //{
        //    return FMOD5_Sound_GetTag(rawPtr, name, index, out tag);
        //}
        //public RESULT getOpenState(out OPENSTATE openstate, out uint percentbuffered, out bool starving, out bool diskbusy)
        //{
        //    return FMOD5_Sound_GetOpenState(rawPtr, out openstate, out percentbuffered, out starving, out diskbusy);
        //}
        //public RESULT readData(IntPtr buffer, uint lenbytes, out uint read)
        //{
        //    return FMOD5_Sound_ReadData(rawPtr, buffer, lenbytes, out read);
        //}
        //public RESULT seekData(uint pcm)
        //{
        //    return FMOD5_Sound_SeekData(rawPtr, pcm);
        //}
        //public RESULT setSoundGroup(SoundGroup soundgroup)
        //{
        //    return FMOD5_Sound_SetSoundGroup(rawPtr, soundgroup.getRaw());
        //}
        //public RESULT getSoundGroup(out SoundGroup soundgroup)
        //{
        //    soundgroup = null;

        //    IntPtr soundgroupraw;
        //    RESULT result = FMOD5_Sound_GetSoundGroup(rawPtr, out soundgroupraw);
        //    soundgroup = new SoundGroup(soundgroupraw);

        //    return result;
        //}
        #endregion

        #region Synchronization point API.  These points can come from markers embedded in wav files, and can also generate channel callbacks.
        public int SyncPointCount
        {
            get
            {
                int num;
                FmodSound.getNumSyncPoints(out num).Check();
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

                return Equals((SyncPoint) obj);
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
            FmodSound.getSyncPoint(index, out ptr).Check();
            return new SyncPoint(ptr, FmodSound);
        }

        public SyncPoint AddSyncPoint(uint offset, TimeUnit unit, string name)
        {
            IntPtr ptr;
            FmodSound.addSyncPoint(offset, (TIMEUNIT)unit, name, out ptr).Check();
            return new SyncPoint(ptr, FmodSound);
        }
        #endregion

        #region Functions also in Channel class but here they are the 'default' to save having to change it in Channel all the time.
        public Mode Mode
        {
            get
            {
                MODE mode;
                FmodSound.getMode(out mode).Check();
                return (Mode)mode;
            }
            set
            {
                FmodSound.setMode((MODE)value).Check();
            }
        }

        public int LoopCount
        {
            get
            {
                int count;
                FmodSound.getLoopCount(out count).Check();
                return count;
            }
            set
            {
                FmodSound.setLoopCount(value).Check();
            }
        }

        //public RESULT setLoopPoints(uint loopstart, TIMEUNIT loopstarttype, uint loopend, TIMEUNIT loopendtype)
        //{
        //    return FMOD5_Sound_SetLoopPoints(rawPtr, loopstart, loopstarttype, loopend, loopendtype);
        //}
        //public RESULT getLoopPoints(out uint loopstart, TIMEUNIT loopstarttype, out uint loopend, TIMEUNIT loopendtype)
        //{
        //    return FMOD5_Sound_GetLoopPoints(rawPtr, out loopstart, loopstarttype, out loopend, loopendtype);
        //}
        #endregion

        #region For MOD/S3M/XM/IT/MID sequenced formats only.
        [StructLayout(LayoutKind.Explicit)]
        public struct ChannelCollection
        {
            [FieldOffset(0)]
            private readonly FMOD.Sound _fmodSound;

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
                return _musicChannelCollection;
            }
        }

        public float MusicSpeed
        {
            get
            {
                float speed;
                FmodSound.getMusicSpeed(out speed).Check();
                return speed;
            }
            set
            {
                FmodSound.setMusicSpeed(value).Check();
            }
        }
        #endregion

        #region Userdata set/get.
        public IntPtr UserData
        {
            get
            {
                IntPtr ptr;
                FmodSound.getUserData(out ptr).Check();
                return ptr;
            }
            set
            {
                FmodSound.setUserData(value).Check();
            }
        }
        #endregion
    }

    public static class SoundExtensions
    {
        public static FMOD.Sound ToFmod(this Sound sound)
        {
            return sound.FmodSound;
        }
    }
}
