using FMOD.Studio;

namespace SupersonicSound.Studio
{
    [EquivalentEnum(typeof(EVENT_PROPERTY))]
    public enum EventProperty
    {
        /// <summary>
        /// Priority to set on low-level channels created by this event instance (-1 to 256).
        /// </summary>
        ChannelPriority = EVENT_PROPERTY.CHANNELPRIORITY
    }
}
