using FMOD;

namespace SupersonicSound.LowLevel
{
    [EquivalentEnum(typeof(CHANNELORDER), "MAX")]
    public enum ChannelOrder
    {
        /// <summary>
        /// Left, Right, Center, LFE, Surround Left, Surround Right, Back Left, Back Right (see FMOD_SPEAKER enumeration)
        /// </summary>
        Default = CHANNELORDER.DEFAULT,

        /// <summary>
        /// Left, Right, Center, LFE, Back Left, Back Right, Surround Left, Surround Right (as per Microsoft .wav WAVEFORMAT structure master order)
        /// </summary>
        WaveFormat = CHANNELORDER.WAVEFORMAT,

        /// <summary>
        /// Left, Center, Right, Surround Left, Surround Right, LFE
        /// </summary>
        ProTools = CHANNELORDER.PROTOOLS,

        /// <summary>
        /// Mono, Mono, Mono, Mono, Mono, Mono, ... (each channel all the way up to 32 channels are treated as if they were mono)
        /// </summary>
        AllMono = CHANNELORDER.ALLMONO,

        /// <summary>
        /// Left, Right, Left, Right, Left, Right, ... (each pair of channels is treated as stereo all the way up to 32 channels)
        /// </summary>
        AllStereo = CHANNELORDER.ALLSTEREO,

        /// <summary>
        /// Left, Right, Surround Left, Surround Right, Center, LFE (as per Linux ALSA channel order)
        /// </summary>
        ALSA = CHANNELORDER.ALSA,
    }
}
