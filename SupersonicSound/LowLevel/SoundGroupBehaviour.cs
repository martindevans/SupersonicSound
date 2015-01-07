using FMOD;

namespace SupersonicSound.LowLevel
{
    [EquivalentEnum(typeof(SOUNDGROUP_BEHAVIOR), "MAX")]
    public enum SoundGroupBehaviour
    {
        /// <summary>
        /// Any sound played that puts the sound count over the SoundGroup::setMaxAudible setting, will simply fail during System::playSound.
        /// </summary>
        Fail = SOUNDGROUP_BEHAVIOR.BEHAVIOR_FAIL,

        /// <summary>
        /// Any sound played that puts the sound count over the SoundGroup::setMaxAudible setting, will be silent, then if another sound in the group stops the sound that was silent before becomes audible again.
        /// </summary>
        Mute = SOUNDGROUP_BEHAVIOR.BEHAVIOR_MUTE,

        /// <summary>
        /// Any sound played that puts the sound count over the SoundGroup::setMaxAudible setting, will steal the quietest / least important sound playing in the group.
        /// </summary>
        StealLowest = SOUNDGROUP_BEHAVIOR.BEHAVIOR_STEALLOWEST,
    }
}
