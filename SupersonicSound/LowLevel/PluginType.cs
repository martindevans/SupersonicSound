using FMOD;

namespace SupersonicSound.LowLevel
{
    [EquivalentEnum(typeof(PLUGINTYPE), "MAX")]
    public enum PluginType
    {
        /// <summary>
        /// The plugin type is an output module.  FMOD mixed audio will play through one of these devices
        /// </summary>
        Output = PLUGINTYPE.OUTPUT,

        /// <summary>
        /// The plugin type is a file format codec.  FMOD will use these codecs to load file formats for playback.
        /// </summary>
        Codec = PLUGINTYPE.CODEC,

        /// <summary>
        /// The plugin type is a DSP unit.  FMOD will use these plugins as part of its DSP network to apply effects to output or generate sound in realtime.
        /// </summary>
        DSP = PLUGINTYPE.DSP,
    }
}
