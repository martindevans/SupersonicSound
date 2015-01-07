using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMOD;

namespace SupersonicSound.LowLevel
{
    [EquivalentEnum(typeof(SOUND_FORMAT),
        "MAX"    
    )]
    public enum SoundFormat
    {
        /// <summary>
        /// Unitialized / unknown
        /// </summary>
        None = SOUND_FORMAT.NONE,

        /// <summary>
        /// 8bit integer PCM data
        /// </summary>
        PCM8 = SOUND_FORMAT.PCM8,

        /// <summary>
        /// 16bit integer PCM data
        /// </summary>
        PCM16 = SOUND_FORMAT.PCM16,

        /// <summary>
        /// 24bit integer PCM data
        /// </summary>
        PCM24 = SOUND_FORMAT.PCM24,

        /// <summary>
        /// 32bit integer PCM data
        /// </summary>
        PCM32 = SOUND_FORMAT.PCM32,

        /// <summary>
        /// 32bit floating point PCM data
        /// </summary>
        PCMFloat = SOUND_FORMAT.PCMFLOAT,

        /// <summary>
        /// Compressed GameCube DSP data
        /// </summary>
        GCADPCM = SOUND_FORMAT.GCADPCM,

        /// <summary>
        /// Compressed XBox ADPCM data
        /// </summary>
        IMAADPCM = SOUND_FORMAT.IMAADPCM,

        /// <summary>
        /// Compressed PlayStation 2 ADPCM data
        /// </summary>
        VAG = SOUND_FORMAT.VAG,

        /// <summary>
        /// Compressed NGP ADPCM data.
        /// </summary>
        HEVAG = SOUND_FORMAT.HEVAG,

        /// <summary>
        /// Compressed Xbox360 data.
        /// </summary>
        XMA = SOUND_FORMAT.XMA,

        /// <summary>
        /// Compressed MPEG layer 2 or 3 data.
        /// </summary>
        MPEG = SOUND_FORMAT.MPEG,

        /// <summary>
        /// Compressed CELT data.
        /// </summary>
        CELT = SOUND_FORMAT.CELT,

        /// <summary>
        /// Compressed ATRAC9 data.
        /// </summary>
        AT9 = SOUND_FORMAT.AT9,

        /// <summary>
        /// Compressed Xbox360 xWMA data.
        /// </summary>
        XWMA = SOUND_FORMAT.XWMA,

        /// <summary>
        /// Compressed Vorbis data.
        /// </summary>
        Vorbis = SOUND_FORMAT.VORBIS,
    }
}
