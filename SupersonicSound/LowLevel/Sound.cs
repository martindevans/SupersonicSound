using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
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

        private bool _throwHandle;
        public bool SuppressInvalidHandle
        {
            get { return !_throwHandle; }
            set { _throwHandle = !value; }
        }

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

        private IReadOnlyList<RESULT> Suppressions()
        {
            return ErrorChecking.Suppress(_throwHandle, true);
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

        public struct Defaults
        {
            public readonly float Frequency;
            public readonly int Priority;

            public Defaults(int priority, float frequency)
                : this()
            {
                Priority = priority;
                Frequency = frequency;
            }
        }

        public bool SetDefaults(Defaults defaults)
        {
            return _fmodSound.setDefaults(defaults.Frequency, defaults.Priority).Check(Suppressions());
        }

        public Defaults? GetDefaults()
        {
            float freq;
            int prio;
            if (!_fmodSound.getDefaults(out freq, out prio).Check(Suppressions()))
                return null;

            return new Defaults(prio, freq);
        }

        public struct MinMax
        {
            public readonly float Min;
            public readonly float Max;

            public MinMax(float min, float max)
                : this()
            {
                Min = min;
                Max = max;
            }
        }

        public bool Set3DMinMaxDistance(MinMax minmax)
        {
            return _fmodSound.set3DMinMaxDistance(minmax.Min, minmax.Max).Check(Suppressions());
        }

        public MinMax? Get3DMinMaxDistance()
        {
            float min, max;
            if (!_fmodSound.get3DMinMaxDistance(out min, out max).Check(Suppressions()))
                return null;

            return new MinMax(min, max);
        }

        public struct ConeSettings
        {
            public readonly float InsideConeAngle;
            public readonly float OutsideConeAngle;
            public readonly float OutsideVolume;

            public ConeSettings(float insideConeAngle, float outsideConeAngle, float outsideVolume)
                : this()
            {
                InsideConeAngle = insideConeAngle;
                OutsideConeAngle = outsideConeAngle;
                OutsideVolume = outsideVolume;
            }
        }

        public bool Set3DConeSettings(ConeSettings settings)
        {
            return _fmodSound.set3DConeSettings(settings.InsideConeAngle, settings.OutsideConeAngle, settings.OutsideVolume).Check(Suppressions());
        }

        public ConeSettings? Get3DConeSettings()
        {
            float outsideConeAngle, insideConeAngle, outsideVolume;
            if (!_fmodSound.get3DConeSettings(out insideConeAngle, out outsideConeAngle, out outsideVolume).Check(Suppressions()))
                return null;

            return new ConeSettings(insideConeAngle, outsideConeAngle, outsideVolume);
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
            private readonly IReadOnlyList<RESULT> _suppressions;

            public SubSoundCollection(FMOD.Sound fmodSound, IReadOnlyList<RESULT> suppressions)
                : this()
            {
                _fmodSound = fmodSound;
                _suppressions = suppressions;
            }

            public Sound? this[int index]
            {
                get
                {
                    FMOD.Sound sound;
                    if (!_fmodSound.getSubSound(index, out sound).Check(_suppressions))
                        return null;
                    return FromFmod(sound);
                }
            }

            public Sound? Parent
            {
                get
                {
                    FMOD.Sound parent;
                    if (!_fmodSound.getSubSoundParent(out parent).Check(_suppressions))
                        return null;
                    return FromFmod(parent);
                }
            }

            public int? Count
            {
                get
                {
                    int num;
                    if (!_fmodSound.getNumSubSounds(out num).Check(_suppressions))
                        return null;
                    return num;
                }
            }
        }

        public SubSoundCollection SubSound
        {
            get
            {
                return new SubSoundCollection(FmodSound, Suppressions());
            }
        }

        public string Name
        {
            get
            {
                StringBuilder builder = new StringBuilder(128);
                if (!_fmodSound.getName(builder, builder.Capacity).Check(Suppressions()))
                    return null;
                return builder.ToString();
            }
        }

        public uint? GetLength(TimeUnit unit)
        {
            uint length;
            return _fmodSound.getLength(out length, (TIMEUNIT)unit).CheckBox(length, Suppressions());
        }

        public struct FormatInfo
        {
            public readonly SoundType Type;
            public readonly SoundFormat Format;
            public readonly int Channels;
            public readonly int Bits;

            public FormatInfo(SoundType type, SoundFormat format, int channels, int bits)
            {
                Type = type;
                Format = format;
                Channels = channels;
                Bits = bits;
            }
        }

        public FormatInfo? GetFormat()
        {
            SOUND_TYPE t;
            SOUND_FORMAT f;
            int channels, bits;
            if (!_fmodSound.getFormat(out t, out f, out channels, out bits).Check(Suppressions()))
                return null;

            return new FormatInfo(
                EquivalentEnum<SOUND_TYPE, SoundType>.Cast(t),
                EquivalentEnum<SOUND_FORMAT, SoundFormat>.Cast(f),
                channels, bits
            );
        }

        public struct TagCollection
        {
            private readonly FMOD.Sound _fmodSound;
            private readonly IReadOnlyList<RESULT> _suppressions;

            public TagCollection(FMOD.Sound fmodSound, IReadOnlyList<RESULT> suppressions)
                : this()
            {
                _fmodSound = fmodSound;
                _suppressions = suppressions;
            }

            public struct TagCount
            {
                public readonly int NumTags;
                public readonly int NumTagsUpdated;

                public TagCount(int numTags, int numTagsUpdated)
                    : this()
                {
                    NumTags = numTags;
                    NumTagsUpdated = numTagsUpdated;
                }
            }

            public TagCount? Count
            {
                get
                {
                    int numTags, numTagsUpdated;
                    if (!_fmodSound.getNumTags(out numTags, out numTagsUpdated).Check(_suppressions))
                        return null;

                    return new TagCount(numTags, numTagsUpdated);
                }
            }

            public Tag? this[string name, int index]
            {
                get
                {
                    TAG tag;
                    if (!_fmodSound.getTag(name, index, out tag).Check(_suppressions))
                        return null;

                    return new Tag(tag);
                }
            }
        }

        public TagCollection Tags
        {
            get
            {
                return new TagCollection(FmodSound, Suppressions());
            }
        }

        public struct OpenStateInfo
        {
            public readonly OpenState OpenState;
            public readonly uint PercentBuffered;
            public readonly bool Starving;
            public readonly bool DiskBusy;

            public OpenStateInfo(OpenState openState, uint percentBuffered, bool starving, bool diskBusy)
                : this()
            {
                OpenState = openState;
                PercentBuffered = percentBuffered;
                Starving = starving;
                DiskBusy = diskBusy;
            }
        }

        public OpenStateInfo? GetOpenState()
        {
            OPENSTATE oState;
            uint percentBuffered;
            bool starving;
            bool diskBusy;
            if (!_fmodSound.getOpenState(out oState, out percentBuffered, out starving, out diskBusy).Check(Suppressions()))
                return null;

            return new OpenStateInfo(
                EquivalentEnum<OPENSTATE, OpenState>.Cast(oState),
                percentBuffered,
                starving,
                diskBusy
            );
        }

        public uint ReadData(byte[] buffer, uint length)
        {
            if (length > buffer.Length)
                throw new ArgumentOutOfRangeException("length", string.Format("Cannot read {0} bytes into buffer with length {1}", length, buffer.Length));

            unsafe
            {
                fixed (byte* ptr = &buffer[0])
                {
                    uint read;
                    _fmodSound.readData(new IntPtr(ptr), length, out read).Check(Suppressions());
                    return read;
                }
            }
        }

        public bool SeekData(uint pcm)
        {
            return _fmodSound.seekData(pcm).Check(Suppressions());
        }

        public SoundGroup? SoundGroup
        {
            get
            {
                FMOD.SoundGroup group;
                if (!_fmodSound.getSoundGroup(out group).Check(Suppressions()))
                    return null;
                return new SoundGroup(group);
            }
            set
            {
                _fmodSound.setSoundGroup(value.Unbox().FmodGroup).Check(Suppressions());
            }
        }
        #endregion

        #region Synchronization point API.  These points can come from markers embedded in wav files, and can also generate channel callbacks.
        public int? SyncPointCount
        {
            get
            {
                int num;
                return _fmodSound.getNumSyncPoints(out num).CheckBox(num, Suppressions());
            }
        }

        public struct SyncPoint
            : IEquatable<SyncPoint>
        {
            private readonly IntPtr _pointer;
            private readonly FMOD.Sound _sound;
            private readonly IReadOnlyList<RESULT> _suppressions;

            internal SyncPoint(IntPtr ptr, FMOD.Sound sound, IReadOnlyList<RESULT> suppressions)
            {
                _pointer = ptr;
                _sound = sound;
                _suppressions = suppressions;
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

            public struct Info
            {
                public readonly uint Offset;
                public readonly string Name;

                public Info(uint offset, string name)
                    : this()
                {
                    Offset = offset;
                    Name = name;
                }
            }

            public Info? GetInfo(TimeUnit offsetType)
            {
                StringBuilder n = new StringBuilder(128);
                uint offset;
                if (!_sound.getSyncPointInfo(_pointer, n, n.Capacity, out offset, (TIMEUNIT)offsetType).Check(_suppressions))
                    return null;

                return new Info(
                    offset,
                    n.ToString()
                );
            }

            public bool Delete()
            {
                return _sound.deleteSyncPoint(_pointer).Check(_suppressions);
            }
        }

        public SyncPoint? GetSyncPoint(int index)
        {
            IntPtr ptr;
            if (!_fmodSound.getSyncPoint(index, out ptr).Check(Suppressions()))
                return null;
            return new SyncPoint(ptr, _fmodSound, Suppressions());
        }

        public SyncPoint? AddSyncPoint(uint offset, TimeUnit unit, string name)
        {
            IntPtr ptr;
            if (!_fmodSound.addSyncPoint(offset, (TIMEUNIT)unit, name, out ptr).Check(Suppressions()))
                return null;
            return new SyncPoint(ptr, _fmodSound, Suppressions());
        }
        #endregion

        #region Functions also in Channel class but here they are the 'default' to save having to change it in Channel all the time.
        public Mode? Mode
        {
            get
            {
                MODE mode;
                if (!_fmodSound.getMode(out mode).Check(Suppressions()))
                    return null;
                return EquivalentEnum<MODE, Mode>.Cast(mode);
            }
            set
            {
                _fmodSound.setMode(EquivalentEnum<Mode, MODE>.Cast(value.Unbox())).Check(Suppressions());
            }
        }

        public int? LoopCount
        {
            get
            {
                int count;
                return _fmodSound.getLoopCount(out count).CheckBox(count, Suppressions());
            }
            set
            {
                _fmodSound.setLoopCount(value.Unbox()).Check(Suppressions());
            }
        }

        public struct LoopPoint
        {
            public readonly uint Start;
            public readonly uint End;

            public LoopPoint(uint start, uint end)
                : this()
            {
                Start = start;
                End = end;
            }
        }

        public bool SetLoopPoints(LoopPoint point, TimeUnit startUnit, TimeUnit endUnit)
        {
            return _fmodSound.setLoopPoints(point.Start, EquivalentEnum<TimeUnit, TIMEUNIT>.Cast(startUnit), point.End, EquivalentEnum<TimeUnit, TIMEUNIT>.Cast(endUnit)).Check(Suppressions());
        }

        public LoopPoint? GetLoopPoints(TimeUnit startUnit, TimeUnit endUnit)
        {
            uint start;
            uint end;
            if (!_fmodSound.getLoopPoints(out start, (TIMEUNIT)startUnit, out end, (TIMEUNIT)endUnit).Check(Suppressions()))
                return null;

            return new LoopPoint(start, end);
        }
        #endregion

        #region For MOD/S3M/XM/IT/MID sequenced formats only.
        public struct ChannelCollection
        {
            private readonly FMOD.Sound _fmodSound;
            private readonly IReadOnlyList<RESULT> _suppressions;

            public ChannelCollection(FMOD.Sound fmodSound, IReadOnlyList<RESULT> suppressions)
                : this()
            {
                _fmodSound = fmodSound;
                _suppressions = suppressions;
            }

            public int? Count
            {
                get
                {
                    int channels;
                    return _fmodSound.getMusicNumChannels(out channels).CheckBox(channels, _suppressions);
                }
            }

            public float? GetVolume(int index)
            {
                float volume;
                return _fmodSound.getMusicChannelVolume(index, out volume).CheckBox(volume, _suppressions);
            }

            public bool SetVolume(int index, float volume)
            {
                return _fmodSound.setMusicChannelVolume(index, volume).Check(_suppressions);
            }
        }

        public ChannelCollection MusicChannels
        {
            get
            {
                return new ChannelCollection(FmodSound, Suppressions());
            }
        }

        public float? MusicSpeed
        {
            get
            {
                float speed;
                return _fmodSound.getMusicSpeed(out speed).CheckBox(speed, Suppressions());
            }
            set
            {
                _fmodSound.setMusicSpeed(value.Unbox()).Check();
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
