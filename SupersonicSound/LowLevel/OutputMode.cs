using FMOD;

namespace SupersonicSound.LowLevel
{
    [EquivalentEnum(typeof(OUTPUTTYPE),
        "PS3",      // I don't anticipate this code running on any consoles, but these can be easily added if needed
        "WIIU",     //
        "AUDIOOUT", // <-- PS4+Vita
        "XAUDIO",   // <-- XBOX
        "WINSONIC", // <-- XBOX ONE
        "NNAUDIO",  // <-- Nintendo switch
        "AUDIO3D",  // <-- PS4

        "MAX"       // Denotes the last item in the enum, don't need this
    )]
    public enum OutputMode
    {
        /// <summary>
        /// Picks the best output mode for the platform. This is the default.
        /// </summary>
        AutoDetect  = OUTPUTTYPE.AUTODETECT,

        /// <summary>
        /// All Platforms - 3rd party plugin, unknown. This is for use with System::getOutput only.
        /// </summary>
        Unknown = OUTPUTTYPE.UNKNOWN,

        /// <summary>
        /// All Platforms - Perform all mixing but discard the final output.
        /// </summary>
        NoSound = OUTPUTTYPE.NOSOUND,

        /// <summary>
        /// All Platforms - Writes output to a .wav file.
        /// </summary>
        WavWriter = OUTPUTTYPE.WAVWRITER,

        /// <summary>
        /// All Platforms - Non-realtime version of NoSound. User can drive mixer with System::update at whatever rate they want.
        /// </summary>
        NoSoundNotRealtime = OUTPUTTYPE.NOSOUND_NRT,

        /// <summary>
        /// All Platforms - Non-realtime version of WavWriter. User can drive mixer with System::update at whatever rate they want.
        /// </summary>
        WavWriterNotRealtime = OUTPUTTYPE.WAVWRITER_NRT,

        /// <summary>
        /// Windows Only - Direct Sound. (Default on Windows XP and below)
        /// </summary>
        DirectSound = OUTPUTTYPE.DSOUND,

        /// <summary>
        /// Windows Only - Windows Multimedia.
        /// </summary>
        WindowsMultiMedia = OUTPUTTYPE.WINMM,

        /// <summary>
        /// Win/WinStore/XboxOne - Windows Audio Session API.           (Default on Windows Vista and above, Xbox One and Windows Store Applications)
        /// </summary>
        Wasapi = OUTPUTTYPE.WASAPI,

        /// <summary>
        /// /* Windows Only - Low latency ASIO 2.0. */
        /// </summary>
        Asio = OUTPUTTYPE.ASIO,

        /// <summary>
        /// Linux Only - Pulse Audio. (Default on Linux if available)
        /// </summary>
        PulseAudio = OUTPUTTYPE.PULSEAUDIO,

        /// <summary>
        /// Linux Only - Advanced Linux Sound Architecture. (Default on Linux if PulseAudio isn't available)
        /// </summary>
        Alsa = OUTPUTTYPE.ALSA,

        /// <summary>
        /// Mac/iOS Only - Core Audio. (Default on Mac and iOS)
        /// </summary>
        CoreAudio = OUTPUTTYPE.COREAUDIO,

        /// <summary>
        /// Android - Java Audio Track. (Default on Android 2.2 and below)
        /// </summary>
        AudioTrack = OUTPUTTYPE.AUDIOTRACK,

        /// <summary>
        /// Android - OpenSL ES. (Default on Android 2.3 and above)
        /// </summary>
        OpenSl = OUTPUTTYPE.OPENSL,

        /// <summary>
        /// Web Browser - JavaScript webaudio output. (Default on JavaScript)
        /// </summary>
        WebAudio = OUTPUTTYPE.WEBAUDIO,

        /// <summary>
        /// Win - Dolby Atmos (WASAPI).
        /// </summary>
        Atmos = OUTPUTTYPE.ATMOS,
    }
}
