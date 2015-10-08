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
        /// Sound data is in its native compressed format.
        /// </summary>
        BitStream = SOUND_FORMAT.BITSTREAM
    }
}
