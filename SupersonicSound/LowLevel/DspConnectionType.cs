using FMOD;

namespace SupersonicSound.LowLevel
{
    [EquivalentEnum(typeof(DSPCONNECTION_TYPE),
        "MAX"
    )]
    public enum DspConnectionType
    {
        /// <summary>
        /// Default connection type. Audio is mixed from the input to the output DSP's audible buffer.
        /// </summary>
        Standard = DSPCONNECTION_TYPE.STANDARD,

        /// <summary>
        /// Sidechain connection type. Audio is mixed from the input to the output DSP's sidechain buffer.
        /// </summary>
        SideChain = DSPCONNECTION_TYPE.SIDECHAIN,

        /// <summary>
        /// Send connection type. Audio is mixed from the input to the output DSP's audible buffer, but the input is NOT executed, only copied from.
        /// A standard connection or sidechain needs to make an input execute to generate data.
        /// </summary>
        Send = DSPCONNECTION_TYPE.SEND,

        /// <summary>
        /// Send sidechain connection type. Audio is mixed from the input to the output DSP's sidechain buffer, but the input is NOT executed, only copied from.
        /// A standard connection or sidechain needs to make an input execute to generate data.
        /// </summary>
        SendSideChain = DSPCONNECTION_TYPE.SEND_SIDECHAIN,
    }
}
