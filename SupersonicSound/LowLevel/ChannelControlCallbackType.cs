using FMOD;

namespace SupersonicSound.LowLevel
{
    [EquivalentEnum(typeof(CHANNELCONTROL_CALLBACK_TYPE), "MAX")]
    public enum ChannelControlCallbackType
        : int
    {
        /// <summary>
        /// Called when a sound ends.
        /// </summary>
        End = CHANNELCONTROL_CALLBACK_TYPE.END,

        /// <summary>
        /// Called when a voice is swapped out or swapped in.
        /// </summary>
        VirtualVoice = CHANNELCONTROL_CALLBACK_TYPE.VIRTUALVOICE,

        /// <summary>
        /// Called when a syncpoint is encountered.  Can be from wav file markers.
        /// </summary>
        SyncPoint = CHANNELCONTROL_CALLBACK_TYPE.SYNCPOINT,

        /// <summary>
        /// Called when the channel has its geometry occlusion value calculated.  Can be used to clamp or change the value.
        /// </summary>
        Occlusion = CHANNELCONTROL_CALLBACK_TYPE.OCCLUSION

    }
}
