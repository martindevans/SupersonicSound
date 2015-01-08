
using System;

namespace SupersonicSound.Studio
{
    [EquivalentEnum(typeof(FMOD.Studio.SYSTEM_CALLBACK_TYPE))]
    [Flags]
    public enum SystemCallbackType
        : uint
    {
        /// <summary>
        /// Called before Studio main update.
        /// </summary>
        PreUpdate = FMOD.Studio.SYSTEM_CALLBACK_TYPE.PREUPDATE,

        /// <summary>
        /// Called after Studio main update.
        /// </summary>
        PostUpdate = FMOD.Studio.SYSTEM_CALLBACK_TYPE.POSTUPDATE
    }
}
