using FMOD;

namespace SupersonicSound.LowLevel
{
    [EquivalentEnum(typeof(SPEAKERMODE),
        "MAX"   // Denotes the last item in the enum, don't really need this
    )]
    public enum SpeakerMode
    {
        /// <summary>
        /// Default speaker mode based on operating system/output mode.  Windows = control panel setting, Xbox = 5.1, PS3 = 7.1 etc.
        /// </summary>
        Default = SPEAKERMODE.DEFAULT,

        /// <summary>
        /// There is no specific speakermode.  Sound channels are mapped in order of input to output.  Use System::setSoftwareFormat to specify speaker count. See remarks for more information.
        /// </summary>
        Raw = SPEAKERMODE.RAW,

        /// <summary>
        /// The speakers are monaural.
        /// </summary>
        Mono = SPEAKERMODE.MONO,

        /// <summary>
        /// The speakers are stereo.
        /// </summary>
        Stereo = SPEAKERMODE.STEREO,

        /// <summary>
        /// 4 speaker setup.  This includes front left, front right, surround left, surround right.
        /// </summary>
        Quad = SPEAKERMODE.QUAD,

        /// <summary>
        /// 5 speaker setup.  This includes front left, front right, center, surround left, surround right.
        /// </summary>
        Surround = SPEAKERMODE.SURROUND,
        
        /// <summary>
        /// 5.1 speaker setup.  This includes front left, front right, center, surround left, surround right and an LFE speaker.
        /// </summary>
        FivePointOne = SPEAKERMODE._5POINT1,
        
        /// <summary>
        /// 7.1 speaker setup.  This includes front left, front right, center, surround left, surround right, back left, back right and an LFE speaker.
        /// </summary>
        SevenPointOne = SPEAKERMODE._7POINT1,
    }
}
