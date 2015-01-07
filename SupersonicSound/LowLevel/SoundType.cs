using FMOD;

namespace SupersonicSound.LowLevel
{
    [EquivalentEnum(typeof(SOUND_TYPE),
        "MAX"    
    )]
    public enum SoundType
    {
        /// <summary>
        /// 3rd party / unknown plugin format.
        /// </summary>
        Unknown = SOUND_TYPE.UNKNOWN,

        /// <summary>
        /// Aiff
        /// </summary>
        AIFF = SOUND_TYPE.AIFF,

        /// <summary>
        /// Microsoft Advanced Systems Format (ie WMA/ASF/WMV).
        /// </summary>
        ASF = SOUND_TYPE.ASF,

        /// <summary>
        /// Sony ATRAC 3 format
        /// </summary>
        AT3 = SOUND_TYPE.AT3,

        /// <summary>
        /// Sound font / downloadable sound bank.
        /// </summary>
        DLS = SOUND_TYPE.DLS,

        /// <summary>
        /// FLAC lossless codec.
        /// </summary>
        FLAC = SOUND_TYPE.FLAC,

        /// <summary>
        /// FMOD Sample Bank.
        /// </summary>
        FSB = SOUND_TYPE.FSB,

        /// <summary>
        /// GameCube ADPCM
        /// </summary>
        GCADPCM = SOUND_TYPE.GCADPCM,

        /// <summary>
        /// Impulse Tracker.
        /// </summary>
        IT = SOUND_TYPE.IT,

        /// <summary>
        /// MIDI.
        /// </summary>
        MIDI = SOUND_TYPE.MIDI,

        /// <summary>
        /// Protracker / Fasttracker MOD.
        /// </summary>
        MOD = SOUND_TYPE.MOD,

        /// <summary>
        /// MP2/MP3 MPEG.
        /// </summary>
        MPEG = SOUND_TYPE.MPEG,

        /// <summary>
        /// Ogg vorbis.
        /// </summary>
        OggVorbis = SOUND_TYPE.OGGVORBIS,

        /// <summary>
        /// Information only from ASX/PLS/M3U/WAX playlists
        /// </summary>
        Playlist = SOUND_TYPE.PLAYLIST,

        /// <summary>
        /// Raw PCM data.
        /// </summary>
        Raw = SOUND_TYPE.RAW,

        /// <summary>
        /// ScreamTracker 3.
        /// </summary>
        S3M = SOUND_TYPE.S3M,

        /// <summary>
        /// User created sound.
        /// </summary>
        User = SOUND_TYPE.USER,

        /// <summary>
        /// Microsoft WAV.
        /// </summary>
        WAV = SOUND_TYPE.WAV,

        /// <summary>
        /// FastTracker 2 XM.
        /// </summary>
        XM = SOUND_TYPE.XM,

        /// <summary>
        /// Xbox360 XMA
        /// </summary>
        XMA = SOUND_TYPE.XMA,

        /// <summary>
        /// PlayStation Portable adpcm VAG format.
        /// </summary>
        VAG = SOUND_TYPE.VAG,

        /// <summary>
        /// iPhone hardware decoder, supports AAC, ALAC and MP3.
        /// </summary>
        AudioQueue = SOUND_TYPE.AUDIOQUEUE,

        /// <summary>
        /// Xbox360 XWMA
        /// </summary>
        XWMA = SOUND_TYPE.XWMA,

        /// <summary>
        /// 3DS BCWAV container format for DSP ADPCM and PCM
        /// </summary>
        BCWAV = SOUND_TYPE.BCWAV,

        /// <summary>
        /// NGP ATRAC 9 format
        /// </summary>
        AT9 = SOUND_TYPE.AT9,

        /// <summary>
        /// Raw vorbis
        /// </summary>
        Vorbis = SOUND_TYPE.VORBIS,

        /// <summary>
        /// Windows Store Application built in system codecs
        /// </summary>
        MediaFoundation = SOUND_TYPE.MEDIA_FOUNDATION,

        /// <summary>
        /// Android MediaCodec
        /// </summary>
        MediaCodec = SOUND_TYPE.MEDIACODEC,
    }
}
