using FMOD;
using System;

namespace SupersonicSound.LowLevel
{
    [EquivalentEnum(typeof(MODE))]
    [Flags]
    public enum Mode
        : uint
    {
        /// <summary>
        /// Default for all modes listed below. FMOD_LOOP_OFF, FMOD_2D, FMOD_3D_WORLDRELATIVE, FMOD_3D_INVERSEROLLOFF
        /// </summary>
        Default = MODE.DEFAULT,

        /// <summary>
        /// For non looping sounds. (default).  Overrides FMOD_LOOP_NORMAL / FMOD_LOOP_BIDI.
        /// </summary>
        LoopOff = MODE.LOOP_OFF,

        /// <summary>
        /// For forward looping sounds.
        /// </summary>
        LoopNormal = MODE.LOOP_NORMAL,

        /// <summary>
        /// For bidirectional looping sounds. (only works on software mixed static sounds).
        /// </summary>
        LoopBidirectional = MODE.LOOP_BIDI,

        /// <summary>
        /// Ignores any 3d processing. (default).
        /// </summary>
        TwoDimensional = MODE._2D,

        /// <summary>
        /// Makes the sound positionable in 3D.  Overrides FMOD_2D.
        /// </summary>
        ThreeDimensional = MODE._3D,

        /// <summary>
        /// Decompress at runtime, streaming from the source provided (standard stream).  Overrides FMOD_CREATESAMPLE.
        /// </summary>
        CreateStream = MODE.CREATESTREAM,

        /// <summary>
        /// Decompress at loadtime, decompressing or decoding whole file into memory as the target sample format. (standard sample).
        /// </summary>
        CreateSample = MODE.CREATESAMPLE,

        /// <summary>
        /// Load MP2, MP3, IMAADPCM or XMA into memory and leave it compressed.  During playback the FMOD software mixer will decode it in realtime as a 'compressed sample'.  Can only be used in combination with FMOD_SOFTWARE.
        /// </summary>
        CreateCompressedSample = MODE.CREATECOMPRESSEDSAMPLE,

        /// <summary>
        /// Opens a user created static sample or stream. Use FMOD_CREATESOUNDEXINFO to specify format and/or read callbacks.  If a user created 'sample' is created with no read callback, the sample will be empty.  Use FMOD_Sound_Lock and FMOD_Sound_Unlock to place sound data into the sound if this is the case.
        /// </summary>
        OpenUser = MODE.OPENUSER,

        /// <summary>
        /// "name_or_data" will be interpreted as a pointer to memory instead of filename for creating sounds.
        /// </summary>
        OpenMemory = MODE.OPENMEMORY,

        /// <summary>
        /// "name_or_data" will be interpreted as a pointer to memory instead of filename for creating sounds.  Use FMOD_CREATESOUNDEXINFO to specify length.  This differs to FMOD_OPENMEMORY in that it uses the memory as is, without duplicating the memory into its own buffers.  Cannot be freed after open, only after Sound::release.   Will not work if the data is compressed and FMOD_CREATECOMPRESSEDSAMPLE is not used.
        /// </summary>
        OpenMemoryPoint = MODE.OPENMEMORY_POINT,

        /// <summary>
        /// Will ignore file format and treat as raw pcm.  User may need to declare if data is FMOD_SIGNED or FMOD_UNSIGNED
        /// </summary>
        OpenRaw = MODE.OPENRAW,

        /// <summary>
        /// Just open the file, dont prebuffer or read.  Good for fast opens for info, or when sound::readData is to be used.
        /// </summary>
        OpenOnly = MODE.OPENONLY,

        /// <summary>
        /// For FMOD_CreateSound - for accurate FMOD_Sound_GetLength / FMOD_Channel_SetPosition on VBR MP3, AAC and MOD/S3M/XM/IT/MIDI files.  Scans file first, so takes longer to open. FMOD_OPENONLY does not affect this.
        /// </summary>
        AccurateTime = MODE.ACCURATETIME,

        /// <summary>
        /// For corrupted / bad MP3 files.  This will search all the way through the file until it hits a valid MPEG header.  Normally only searches for 4k.
        /// </summary>
        MpegSearch = MODE.MPEGSEARCH,

        /// <summary>
        /// For opening sounds and getting streamed subsounds (seeking) asyncronously.  Use Sound::getOpenState to poll the state of the sound as it opens or retrieves the subsound in the background.
        /// </summary>
        NonBlocking = MODE.NONBLOCKING,

        /// <summary>
        /// Unique sound, can only be played one at a time
        /// </summary>
        Unique = MODE.UNIQUE,

        /// <summary>
        /// Make the sound's position, velocity and orientation relative to the listener.
        /// </summary>
        HeadRelative3D = MODE._3D_HEADRELATIVE,

        /// <summary>
        /// Make the sound's position, velocity and orientation absolute (relative to the world). (DEFAULT)
        /// </summary>
        WorldRelative3D = MODE._3D_WORLDRELATIVE,

        /// <summary>
        /// This sound will follow the inverse rolloff model where mindistance = full volume, maxdistance = where sound stops attenuating, and rolloff is fixed according to the global rolloff factor.  (DEFAULT)
        /// </summary>
        InverseRollOff3D = MODE._3D_INVERSEROLLOFF,

        /// <summary>
        /// This sound will follow a linear rolloff model where mindistance = full volume, maxdistance = silence.
        /// </summary>
        LinearRollOff3D = MODE._3D_LINEARROLLOFF,

        /// <summary>
        /// This sound will follow a linear-square rolloff model where mindistance = full volume, maxdistance = silence.  Rolloffscale is ignored.
        /// </summary>
        LinearSquareRollOff3D = MODE._3D_LINEARSQUAREROLLOFF,

        /// <summary>
        /// This sound will follow the inverse rolloff model at distances close to mindistance and a linear-square rolloff close to maxdistance.
        /// </summary>
        InverseTaperedRollOff3D = MODE._3D_INVERSETAPEREDROLLOFF,

        /// <summary>
        /// This sound will follow a rolloff model defined by Sound::set3DCustomRolloff / Channel::set3DCustomRolloff.
        /// </summary>
        CustomRollOff3D = MODE._3D_CUSTOMROLLOFF,

        /// <summary>
        /// Is not affect by geometry occlusion.  If not specified in Sound::setMode, or Channel::setMode, the flag is cleared and it is affected by geometry again.
        /// </summary>
        IgnoreGeometry3D = MODE._3D_IGNOREGEOMETRY,

        /// <summary>
        /// Skips id3v2/asf/etc tag checks when opening a sound, to reduce seek/read overhead when opening files (helps with CD performance).
        /// </summary>
        IgnoreTags = MODE.IGNORETAGS,

        /// <summary>
        /// Removes some features from samples to give a lower memory overhead, like Sound::getName.
        /// </summary>
        LowMem = MODE.LOWMEM,

        /// <summary>
        /// Load sound into the secondary RAM of supported platform.  On PS3, sounds will be loaded into RSX/VRAM.
        /// </summary>
        LoadSecondaryRam = MODE.LOADSECONDARYRAM,

        /// <summary>
        /// For sounds that start virtual (due to being quiet or low importance), instead of swapping back to audible, and playing at the correct offset according to time, this flag makes the sound play from the start.
        /// </summary>
        VirtualPlayFromStart = MODE.VIRTUAL_PLAYFROMSTART
    }
}
