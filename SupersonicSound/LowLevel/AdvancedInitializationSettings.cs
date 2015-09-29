using FMOD;
using System.Runtime.InteropServices;

namespace SupersonicSound.LowLevel
{
    public struct AdvancedInitializationSettings
    {
        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. For use with FMOD_CREATECOMPRESSEDSAMPLE only.  MPEG   codecs consume 30,528 bytes per instance and this number will determine how many MPEG   channels can be played simultaneously. Default = 32.
        /// </summary>
        public int MaxMpegCodecs { get; private set; }

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. For use with FMOD_CREATECOMPRESSEDSAMPLE only.  ADPCM  codecs consume  3,128 bytes per instance and this number will determine how many ADPCM  channels can be played simultaneously. Default = 32.
        /// </summary>
        public int MaxAdpcmCodecs { get; private set; }

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. For use with FMOD_CREATECOMPRESSEDSAMPLE only.  XMA    codecs consume 14,836 bytes per instance and this number will determine how many XMA    channels can be played simultaneously. Default = 32.
        /// </summary>
        public int MaxXmaCodecs { get; private set; }

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. For use with FMOD_CREATECOMPRESSEDSAMPLE only.  Vorbis codecs consume 23,256 bytes per instance and this number will determine how many Vorbis channels can be played simultaneously. Default = 32.
        /// </summary>
        public int MaxVorbisCodecs { get; private set; }

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. For use with FMOD_CREATECOMPRESSEDSAMPLE only.  AT9    codecs consume  8,720 bytes per instance and this number will determine how many AT9    channels can be played simultaneously. Default = 32.
        /// </summary>
        public int MaxAT9Codecs { get; private set; }

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. Number of channels available on the ASIO device.
        /// </summary>
        public int AsioNumChannels { get; private set; }

        //public IntPtr ASIOChannelList;             /* [r/w] Optional. Specify 0 to ignore. Pointer to an array of strings (number of entries defined by ASIONumChannels) with ASIO channel names. */

        //public IntPtr ASIOSpeakerList;             /* [r/w] Optional. Specify 0 to ignore. Pointer to a list of speakers that the ASIO channels map to.  This can be called after System::init to remap ASIO output. */

        /// <summary>
        /// [r/w] Optional. For use with FMOD_INIT_HRTF_LOWPASS.  The angle range (0-360) of a 3D sound in relation to the listener, at which the HRTF function begins to have an effect. 0 = in front of the listener. 180 = from 90 degrees to the left of the listener to 90 degrees to the right. 360 = behind the listener. Default = 180.0.
        /// </summary>
        public float HrtfMinAngle { get; private set; }

        /// <summary>
        /// [r/w] Optional. For use with FMOD_INIT_HRTF_LOWPASS.  The angle range (0-360) of a 3D sound in relation to the listener, at which the HRTF function has maximum effect. 0 = front of the listener. 180 = from 90 degrees to the left of the listener to 90 degrees to the right. 360 = behind the listener. Default = 360.0.
        /// </summary>
        public float HrtfMaxAngle { get; private set; }

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. For use with FMOD_INIT_HRTF_LOWPASS.  The cutoff frequency of the HRTF's lowpass filter function when at maximum effect. (i.e. at HRTFMaxAngle).  Default = 4000.0.
        /// </summary>
        public float HrtfFreq { get; private set; }

        /// <summary>
        ///  [r/w] Optional. Specify 0 to ignore. For use with FMOD_INIT_VOL0_BECOMES_VIRTUAL.  If this flag is used, and the volume is below this, then the sound will become virtual.  Use this value to raise the threshold to a different point where a sound goes virtual.
        /// </summary>
        public float Vol0VirtualVol { get; private set; }

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. For streams. This determines the default size of the double buffer (in milliseconds) that a stream uses.  Default = 400ms
        /// </summary>
        public uint DefaultDecodeBufferSize { get; private set; }

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. For use with FMOD_INIT_ENABLE_PROFILE.  Specify the port to listen on for connections by the profiler application.
        /// </summary>
        public ushort ProfilePort { get; private set; }

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. The maximum time in miliseconds it takes for a channel to fade to the new level when its occlusion changes.
        /// </summary>
        public uint GeometryMaxFadeTime { get; private set; }

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. For use with FMOD_INIT_DISTANCE_FILTERING.  The default center frequency in Hz for the distance filtering effect. Default = 1500.0.
        /// </summary>
        public float DistanceFilterCenterFreq { get; private set; }

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. Out of 0 to 3, 3d reverb spheres will create a phyical reverb unit on this instance slot.  See FMOD_REVERB_PROPERTIES.
        /// </summary>
        public int Reverb3Dinstance { get; private set; }

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. Number of buffers in DSP buffer pool.  Each buffer will be DSPBlockSize * sizeof(float) * SpeakerModeChannelCount.  ie 7.1 @ 1024 DSP block size = 8 * 1024 * 4 = 32kb.  Default = 8.
        /// </summary>
        public int DSPBufferPoolSize { get; private set; }

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. Specify the stack size for the FMOD Stream thread in bytes.  Useful for custom codecs that use excess stack.  Default 49,152 (48kb)
        /// </summary>
        public uint StackSizeStream { get; private set; }

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. Specify the stack size for the FMOD_NONBLOCKING loading thread.  Useful for custom codecs that use excess stack.  Default 65,536 (64kb)
        /// </summary>
        public uint StackSizeNonBlocking { get; private set; }

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. Specify the stack size for the FMOD mixer thread.  Useful for custom dsps that use excess stack.  Default 49,152 (48kb)
        /// </summary>
        public uint StackSizeMixer { get; private set; }

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. Resampling method used with fmod's software mixer.  See FMOD_DSP_RESAMPLER for details on methods.
        /// </summary>
        public DSP_RESAMPLER ResamplerMethod { get; private set; }

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. Specify the command queue size for thread safe processing.  Default 2048 (2kb)
        /// </summary>
        public uint CommandQueueSize { get; private set; }

        /// <summary>
        /// [r/w] Optional. Specify 0 to ignore. Seed value that FMOD will use to initialize its internal random number generators.
        /// </summary>
        public uint RandomSeed { get; private set; }

        public AdvancedInitializationSettings(
            int maxMpegCodecs = 0, int maxAdpcmCodecs = 0, int maxXmaCodecs = 0, int maxVorbisCodecs = 0, int maxAT9Codecs = 0, int asioNumChannels = 0,
            float hrtfMinAngle = 0, float hrtfMaxAngle = 0, float hrtfFreq = 0, float vol0VirtualVol = 0, uint defaultDecodeBufferSize = 0, ushort profilePort = 0,
            uint geometryMaxFadeTime = 0, float distanceFilterCenterFreq = 0, int reverb3Dinstance = 0, int dspBufferPoolSize = 0, uint stackSizeStream = 0,
            uint stackSizeNonBlocking = 0, uint stackSizeMixer = 0, DSP_RESAMPLER resamplerMethod = DSP_RESAMPLER.DEFAULT, uint commandQueueSize = 0, uint randomSeed = 0)
            : this()
        {
            MaxMpegCodecs = maxMpegCodecs;
            MaxAdpcmCodecs = maxAdpcmCodecs;
            MaxXmaCodecs = maxXmaCodecs;
            MaxVorbisCodecs = maxVorbisCodecs;
            MaxAT9Codecs = maxAT9Codecs;
            AsioNumChannels = asioNumChannels;
            HrtfMinAngle = hrtfMinAngle;
            HrtfMaxAngle = hrtfMaxAngle;
            HrtfFreq = hrtfFreq;
            Vol0VirtualVol = vol0VirtualVol;
            DefaultDecodeBufferSize = defaultDecodeBufferSize;
            ProfilePort = profilePort;
            GeometryMaxFadeTime = geometryMaxFadeTime;
            DistanceFilterCenterFreq = distanceFilterCenterFreq;
            Reverb3Dinstance = reverb3Dinstance;
            DSPBufferPoolSize = dspBufferPoolSize;
            StackSizeStream = stackSizeStream;
            StackSizeNonBlocking = stackSizeNonBlocking;
            StackSizeMixer = stackSizeMixer;
            ResamplerMethod = resamplerMethod;
            CommandQueueSize = commandQueueSize;
            RandomSeed = randomSeed;
        }

        internal ADVANCEDSETTINGS ToFmod()
        {
            return new ADVANCEDSETTINGS
            {
                cbSize = Marshal.SizeOf(typeof(ADVANCEDSETTINGS)),
                maxMPEGCodecs = MaxMpegCodecs,
                maxADPCMCodecs = MaxAdpcmCodecs,
                maxXMACodecs = MaxXmaCodecs,
                maxVorbisCodecs = MaxVorbisCodecs,
                maxAT9Codecs = MaxAT9Codecs,
                ASIONumChannels = AsioNumChannels,
                HRTFMinAngle = HrtfMinAngle,
                HRTFMaxAngle = HrtfMaxAngle,
                HRTFFreq = HrtfFreq,
                vol0virtualvol = Vol0VirtualVol,
                defaultDecodeBufferSize = DefaultDecodeBufferSize,
                profilePort = ProfilePort,
                geometryMaxFadeTime = GeometryMaxFadeTime,
                distanceFilterCenterFreq = DistanceFilterCenterFreq,
                reverb3Dinstance = Reverb3Dinstance,
                DSPBufferPoolSize = DSPBufferPoolSize,
                stackSizeStream = StackSizeStream,
                stackSizeNonBlocking = StackSizeNonBlocking,
                stackSizeMixer = StackSizeMixer,
                resamplerMethod = ResamplerMethod,
                commandQueueSize = CommandQueueSize,
                randomSeed = RandomSeed
            };
        }
    }
}
