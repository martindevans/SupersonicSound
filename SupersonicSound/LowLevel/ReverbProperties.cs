using System;
using FMOD;
using SupersonicSound.Wrapper;

namespace SupersonicSound.LowLevel
{
    public struct ReverbProperties
    {
        /// <summary>
        /// Reverberation decay time in ms
        /// </summary>
        public float DecayTime { get; private set; }

        /// <summary>
        /// Initial reflection delay time
        /// </summary>
        public float EarlyDelay { get; private set; }

        /// <summary>
        /// Late reverberation delay time relative to initial reflection
        /// </summary>
        public float LateDelay { get; private set; }

        /// <summary>
        /// Reference high frequency (hz)
        /// </summary>
        public float HFReference { get; private set; }

        /// <summary>
        /// High-frequency to mid-frequency decay time ratio
        /// </summary>
        public float HFDecayRatio { get; private set; }

        /// <summary>
        /// Value that controls the echo density in the late reverberation decay.
        /// </summary>
        public float Diffusion { get; private set; }

        /// <summary>
        /// Value that controls the modal density in the late reverberation decay
        /// </summary>
        public float Density { get; private set; }

        /// <summary>
        /// Reference low frequency (hz)
        /// </summary>
        public float LowShelfFrequency { get; private set; }

        /// <summary>
        /// Relative room effect level at low frequencies
        /// </summary>
        public float LowShelfGain { get; private set; }

        /// <summary>
        /// Relative room effect level at high frequencies
        /// </summary>
        public float HighCut { get; private set; }

        /// <summary>
        /// Early reflections level relative to room effect
        /// </summary>
        public float EarlyLateMix { get; private set; }

        /// <summary>
        /// Room effect level (at mid frequencies)
        /// </summary>
        public float WetLevel { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="decayTime">Reverberation decay time in ms, Min:0, Max:20000</param>
        /// <param name="earlyDelay">Initial reflection delay time, Min:0, Max:300</param>
        /// <param name="lateDelay">Late reverberation delay time relative to initial reflection, Min:0, Max:100</param>
        /// <param name="hfReference">Reference high frequency (hz), Min:20, Max:20000</param>
        /// <param name="hfDecayRatio">High-frequency to mid-frequency decay time ratio, Min:10, Max:100</param>
        /// <param name="diffusion">Value that controls the echo density in the late reverberation decay, Min:0, Max:100</param>
        /// <param name="density">Value that controls the modal density in the late reverberation decay, Min:0, Max:100</param>
        /// <param name="lowShelfFrequency">Reference low frequency (hz), Min:20, Max:1000</param>
        /// <param name="lowShelfGain">Relative room effect level at low frequencies, Min:-36, Max:12</param>
        /// <param name="highCut">Relative room effect level at high frequencies, Min:20, Max:20000</param>
        /// <param name="earlyLateMix">Early reflections level relative to room effect, Min:0, Max:100</param>
        /// <param name="wetLevel">Room effect level (at mid frequencies), Min:-80, Max:20</param>
        public ReverbProperties(
            float decayTime = 1500f, float earlyDelay = 7f, float lateDelay = 11f, float hfReference = 5000f,
            float hfDecayRatio = 50f, float diffusion = 100f, float density = 100f, float lowShelfFrequency = 250f, float lowShelfGain = 0,
            float highCut = 20000f, float earlyLateMix = 50f, float wetLevel = -6f
        )
            : this()
        {
            if (decayTime < 0 || decayTime > 20000)
                throw new ArgumentOutOfRangeException("decayTime");

            if (earlyDelay < 0 || earlyDelay > 300)
                throw new ArgumentOutOfRangeException("earlyDelay");

            if (lateDelay < 0 || LateDelay > 100)
                throw new ArgumentOutOfRangeException("lateDelay");

            if (hfReference < 20 || hfReference > 20000)
                throw new ArgumentOutOfRangeException("hfReference");

            if (hfDecayRatio < 10 || hfDecayRatio > 100)
                throw new ArgumentOutOfRangeException("hfDecayRatio");

            if (diffusion < 0 || diffusion > 100)
                throw new ArgumentOutOfRangeException("diffusion");

            if (density < 0 || density > 100)
                throw new ArgumentOutOfRangeException("density");

            if (lowShelfFrequency < 20 || lowShelfFrequency > 1000)
                throw new ArgumentOutOfRangeException("lowShelfFrequency");

            if (lowShelfGain < -36 || lowShelfGain > 12)
                throw new ArgumentOutOfRangeException("lowShelfGain");

            if (highCut < 20 || highCut > 20000)
                throw new ArgumentOutOfRangeException("highCut");

            if (earlyLateMix < 0 || earlyLateMix > 100)
                throw new ArgumentOutOfRangeException("earlyLateMix");

            if (wetLevel < -80 || wetLevel > 20)
                throw new ArgumentOutOfRangeException("wetLevel");

            DecayTime = decayTime;
            EarlyDelay = earlyDelay;
            LateDelay = lateDelay;
            HFReference = hfReference;
            HFDecayRatio = hfDecayRatio;
            Diffusion = diffusion;
            Density = density;
            LowShelfFrequency = lowShelfFrequency;
            LowShelfGain = lowShelfGain;
            HighCut = highCut;
            EarlyLateMix = earlyLateMix;
            WetLevel = wetLevel;
        }

        internal ReverbProperties(ref REVERB_PROPERTIES props)
            : this()
        {
            // All field of REVERB_PROPERTIES are private in the wrapper, I had to modify the wrapper here to expose those fields

            DecayTime = props.DecayTime;
            EarlyDelay = props.EarlyDelay;
            LateDelay = props.LateDelay;
            HFReference = props.HFReference;
            HFDecayRatio = props.HFDecayRatio;
            Diffusion = props.Diffusion;
            Density = props.Density;
            LowShelfFrequency = props.LowShelfFrequency;
            LowShelfGain = props.LowShelfGain;
            HighCut = props.HighCut;
            EarlyLateMix = props.EarlyLateMix;
            WetLevel = props.WetLevel;
        }

        internal REVERB_PROPERTIES ToFmod()
        {
            return new REVERB_PROPERTIES(
                DecayTime, EarlyDelay, LateDelay, HFReference, HFDecayRatio, Diffusion, Density, LowShelfFrequency, LowShelfGain, HighCut, EarlyLateMix, WetLevel
            );
        }
    }

    public class ReverbPropertiesController
    {
        private readonly FMOD.System _system;

        internal ReverbPropertiesController(FMOD.System system)
        {
            _system = system;
        }

        public ReverbProperties this[int instance]
        {
            get
            {
                REVERB_PROPERTIES prop;
                _system.getReverbProperties(instance, out prop).Check();
                return new ReverbProperties(ref prop);
            }
            set
            {
                var prop = value.ToFmod();
                _system.setReverbProperties(instance, ref prop).Check();
            }
        }
    }
}
