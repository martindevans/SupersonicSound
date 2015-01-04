using System;

namespace SupersonicSound.LowLevel
{
    [Flags]
    [EquivalentEnum(typeof(FMOD.INITFLAGS))]
    public enum LowLevelInitFlags
        : uint
    {
        /// <summary>
        /// Initialize normally.
        /// </summary>
        Normal = FMOD.INITFLAGS.NORMAL,

        /// <summary>
        /// No stream thread is created internally.  Streams are driven from System::update.  Mainly used with non-realtime outputs.
        /// </summary>
        StreamFromUpdate = FMOD.INITFLAGS.STREAM_FROM_UPDATE,

        /// <summary>
        /// Win/Wii/PS3/Xbox/Xbox 360 Only - FMOD Mixer thread is woken up to do a mix when System::update is called rather than waking periodically on its own timer.
        /// </summary>
        MixFromUpdate = FMOD.INITFLAGS.MIX_FROM_UPDATE,

        /// <summary>
        /// FMOD will treat +X as right, +Y as up and +Z as backwards (towards you).
        /// </summary>
        RightHanded3D = FMOD.INITFLAGS._3D_RIGHTHANDED,

        /// <summary>
        /// All FMOD_3D based voices will add a software lowpass filter effect into the DSP chain which is automatically used when Channel.Set3DOcclusion is used or the geometry API.
        /// This also causes sounds to sound duller when the sound goes behind the listener, as a fake HRTF style effect.
        /// Use System.SetAdvancedSettings to disable or adjust cutoff frequency for this feature.
        /// </summary>
        ChannelLowPass = FMOD.INITFLAGS.CHANNEL_LOWPASS,

        /// <summary>
        /// All FMOD_3D based voices will add a software lowpass and highpass filter effect into the DSP chain which will act as a distance-automated bandpass filter.
        /// Use System.SetAdvancedSettings to adjust the center frequency.
        /// </summary>
        ChannelDistanceFilter = FMOD.INITFLAGS.CHANNEL_DISTANCEFILTER,

        /// <summary>
        /// Enable TCP/IP based host which allows FMOD Designer or FMOD Profiler to connect to it, and view memory, CPU and the DSP network graph in real-time.
        /// </summary>
        EnableProfiling = FMOD.INITFLAGS.PROFILE_ENABLE,

        /// <summary>
        /// Any sounds that are 0 volume will go virtual and not be processed except for having their positions updated virtually.
        /// Use System.SetAdvancedSettings to adjust what volume besides zero to switch to virtual at.
        /// </summary>
        VolumeZeroIsVirtual = FMOD.INITFLAGS.VOL0_BECOMES_VIRTUAL,

        /// <summary>
        /// With the geometry engine, only process the closest polygon rather than accumulating all polygons the sound to listener line intersects.
        /// </summary>
        UseClosestGeometry = FMOD.INITFLAGS.GEOMETRY_USECLOSEST,

        /// <summary>
        /// When using FMOD_SPEAKERMODE_5POINT1 with a stereo output device, use the Dolby Pro Logic II downmix algorithm instead of the SRS Circle Surround algorithm.
        /// </summary>
        PreferDolbyDownmix = FMOD.INITFLAGS.PREFER_DOLBY_DOWNMIX,

        /// <summary>
        /// Disables thread safety for API calls. Only use this if FMOD low level is being called from a single thread, and if Studio API is not being used!
        /// </summary>
        ThreadUnsafe = FMOD.INITFLAGS.THREAD_UNSAFE,

        /// <summary>
        /// Slower, but adds level metering for every single DSP unit in the graph.  Use DSP.SetMeteringEnabled to turn meters off individually.
        /// </summary>
        ProfileAll = FMOD.INITFLAGS.PROFILE_METER_ALL
    }
}
