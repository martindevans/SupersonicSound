using FMOD;

namespace SupersonicSound.LowLevel
{
    [EquivalentEnum(typeof(DSP_TYPE))]
    public enum DspType
    {
        /// <summary>
        /// This unit was created via a non FMOD plugin so has an unknown purpose.
        /// </summary>
        Unknown = DSP_TYPE.UNKNOWN,

        /// <summary>
        /// This unit does nothing but take inputs and mix them together then feed the result to the soundcard unit.
        /// </summary>
        Mixer = DSP_TYPE.MIXER,

        /// <summary>
        /// This unit generates sine/square/saw/triangle or noise tones.
        /// </summary>
        Oscillator = DSP_TYPE.OSCILLATOR,

        /// <summary>
        /// This unit filters sound using a high quality, resonant lowpass filter algorithm but consumes more CPU time.
        /// </summary>
        LowPass = DSP_TYPE.LOWPASS,

        /// <summary>
        /// This unit filters sound using a resonant lowpass filter algorithm that is used in Impulse Tracker, but with limited cutoff range (0 to 8060hz).
        /// </summary>
        ItLowPass = DSP_TYPE.ITLOWPASS,

        /// <summary>
        /// This unit filters sound using a resonant highpass filter algorithm.
        /// </summary>
        HighPass = DSP_TYPE.HIGHPASS,

        /// <summary>
        /// This unit produces an echo on the sound and fades out at the desired rate.
        /// </summary>
        Echo = DSP_TYPE.ECHO,

        /// <summary>
        /// This unit pans and scales the volume of a unit.
        /// </summary>
        Fader = DSP_TYPE.FADER,

        /// <summary>
        /// This unit produces a flange effect on the sound.
        /// </summary>
        Flange = DSP_TYPE.FLANGE,

        /// <summary>
        /// This unit distorts the sound.
        /// </summary>
        Distortion = DSP_TYPE.DISTORTION,

        /// <summary>
        /// This unit normalizes or amplifies the sound to a certain level.
        /// </summary>
        Normalize = DSP_TYPE.NORMALIZE,

        /// <summary>
        /// This unit limits the sound to a certain level.
        /// </summary>
        Limiter = DSP_TYPE.LIMITER,

        /// <summary>
        /// This unit attenuates or amplifies a selected frequency range.
        /// </summary>
        ParamEq = DSP_TYPE.PARAMEQ,

        /// <summary>
        /// This unit bends the pitch of a sound without changing the speed of playback.
        /// </summary>
        PitchShift = DSP_TYPE.PITCHSHIFT,

        /// <summary>
        /// This unit produces a chorus effect on the sound.
        /// </summary>
        Chorus = DSP_TYPE.CHORUS,

        /// <summary>
        /// This unit allows the use of Steinberg VST plugins
        /// </summary>
        VstPlugin = DSP_TYPE.VSTPLUGIN,
        
        /// <summary>
        /// This unit allows the use of Nullsoft Winamp plugins
        /// </summary>
        WinAmpPlugin = DSP_TYPE.WINAMPPLUGIN,

        /// <summary>
        /// This unit produces an echo on the sound and fades out at the desired rate as is used in Impulse Tracker.
        /// </summary>
        ItEcho = DSP_TYPE.ITECHO,

        /// <summary>
        /// This unit implements dynamic compression (linked multichannel, wideband)
        /// </summary>
        Compressor = DSP_TYPE.COMPRESSOR,

        /// <summary>
        /// This unit implements SFX reverb
        /// </summary>
        SfxReverb = DSP_TYPE.SFXREVERB,
        
        /// <summary>
        /// This unit filters sound using a simple lowpass with no resonance, but has flexible cutoff and is fast.
        /// </summary>
        LowPassSimple = DSP_TYPE.LOWPASS_SIMPLE,

        /// <summary>
        /// This unit produces different delays on individual channels of the sound.
        /// </summary>
        Delay = DSP_TYPE.DELAY,

        /// <summary>
        /// This unit produces a tremolo / chopper effect on the sound.
        /// </summary>
        Tremolo = DSP_TYPE.TREMOLO,

        /// <summary>
        /// This unit allows the use of LADSPA standard plugins.
        /// </summary>
        LadspaPlugin = DSP_TYPE.LADSPAPLUGIN,
        
        /// <summary>
        /// This unit sends a copy of the signal to a return DSP anywhere in the DSP tree.
        /// </summary>
        Send = DSP_TYPE.SEND,
        
        /// <summary>
        /// This unit receives signals from a number of send DSPs.
        /// </summary>
        Return = DSP_TYPE.RETURN,

        /// <summary>
        /// This unit filters sound using a simple highpass with no resonance, but has flexible cutoff and is fast.
        /// </summary>
        HighPassSimple = DSP_TYPE.HIGHPASS_SIMPLE,

        /// <summary>
        /// This unit pans the signal, possibly upmixing or downmixing as well.
        /// </summary>
        Pan = DSP_TYPE.PAN,
        
        /// <summary>
        /// This unit is a three-band equalizer.
        /// </summary>
        ThreeBandEq = DSP_TYPE.THREE_EQ,

        /// <summary>
        /// This unit simply analyzes the signal and provides spectrum information back through getParameter.
        /// </summary>
        Fft = DSP_TYPE.FFT,

        /// <summary>
        /// This unit analyzes the loudness and true peak of the signal.
        /// </summary>
        LoudnessMeter = DSP_TYPE.LOUDNESS_METER,

        /// <summary>
        /// This unit tracks the envelope of the input/sidechain signal
        /// </summary>
        EnvelopeFollower = DSP_TYPE.ENVELOPEFOLLOWER,

        /// <summary>
        /// This unit implements convolution reverb.
        /// </summary>
        ConvolutionReverb = DSP_TYPE.CONVOLUTIONREVERB,
    }
}
