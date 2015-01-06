using FMOD;
using System;

namespace SupersonicSound.LowLevel
{
    [EquivalentEnum(typeof(TIMEUNIT))]
    [Flags]
    public enum TimeUnit
        : uint
    {
        /// <summary>
        /// Milliseconds.
        /// </summary>
        Milliseconds = TIMEUNIT.MS,

        /// <summary>
        /// PCM Samples, related to milliseconds * samplerate / 1000.
        /// </summary>
        PCM = TIMEUNIT.PCM,

        /// <summary>
        /// Bytes, related to PCM samples * channels * datawidth (ie 16bit = 2 bytes).
        /// </summary>
        PCMBytes = TIMEUNIT.PCMBYTES,

        /// <summary>
        /// Raw file bytes of (compressed) sound data (does not include headers).  Only used by Sound::getLength and Channel::getPosition.
        /// </summary>
        RawBytes = TIMEUNIT.RAWBYTES,

        /// <summary>
        /// Fractions of 1 PCM sample.  Unsigned int range 0 to 0xFFFFFFFF.  Used for sub-sample granularity for DSP purposes.
        /// </summary>
        PCMFraction = TIMEUNIT.PCMFRACTION,

        /// <summary>
        /// MOD/S3M/XM/IT.  Order in a sequenced module format.  Use Sound::getFormat to determine the format.
        /// </summary>
        MODOrder = TIMEUNIT.MODORDER,

        /// <summary>
        /// MOD/S3M/XM/IT.  Current row in a sequenced module format.  Sound::getLength will return the number if rows in the currently playing or seeked to pattern.
        /// </summary>
        MODRow = TIMEUNIT.MODROW,

        /// <summary>
        /// MOD/S3M/XM/IT.  Current pattern in a sequenced module format.  Sound::getLength will return the number of patterns in the song and Channel::getPosition will return the currently playing pattern.
        /// </summary>
        MODPattern = TIMEUNIT.MODPATTERN,

        /// <summary>
        /// Time value as seen by buffered stream.  This is always ahead of audible time, and is only used for processing.
        /// </summary>
        Buffered = TIMEUNIT.BUFFERED
    }
}
