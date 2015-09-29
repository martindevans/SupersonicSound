
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
        PostUpdate = FMOD.Studio.SYSTEM_CALLBACK_TYPE.POSTUPDATE,

        /// <summary>
        /// Called when bank has just been unloaded, after all resources are freed. CommandData will be the bank handle.
        /// </summary>
        BankUnload = FMOD.Studio.SYSTEM_CALLBACK_TYPE.BANK_UNLOAD,

        /// <summary>
        /// Pass this mask to Studio::System::setCallback to receive all callback types.
        /// </summary>
        All = FMOD.Studio.SYSTEM_CALLBACK_TYPE.ALL
    }
}
