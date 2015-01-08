using FMOD;
using System;

namespace SupersonicSound.LowLevel
{
    [EquivalentEnum(typeof(SYSTEM_CALLBACK_TYPE))]
    [Flags]
    public enum SystemCallbackType
        : uint
    {
        /// <summary>
        /// Called from System::update when the enumerated list of devices has changed.
        /// </summary>
        DeviceListChanged = SYSTEM_CALLBACK_TYPE.DEVICELISTCHANGED,

        /// <summary>
        /// Called from System::update when an output device has been lost due to control panel parameter changes and FMOD cannot automatically recover.
        /// </summary>
        DeviceLost = SYSTEM_CALLBACK_TYPE.DEVICELOST,

        /// <summary>
        /// Called directly when a memory allocation fails somewhere in FMOD.  (NOTE - 'system' will be NULL in this callback type.)
        /// </summary>
        MemoryAllocationFailed = SYSTEM_CALLBACK_TYPE.MEMORYALLOCATIONFAILED,

        /// <summary>
        /// Called directly when a thread is created. (NOTE - 'system' will be NULL in this callback type.)
        /// </summary>
        ThreadCreated = SYSTEM_CALLBACK_TYPE.THREADCREATED,
        
        /// <summary>
        /// Called when a bad connection was made with DSP::addInput. Usually called from mixer thread because that is where the connections are made.
        /// </summary>
        BadDspConnection = SYSTEM_CALLBACK_TYPE.BADDSPCONNECTION,

        /// <summary>
        /// Called each tick before a mix update happens.
        /// </summary>
        PreMix = SYSTEM_CALLBACK_TYPE.PREMIX,

        /// <summary>
        /// Called each tick after a mix update happens.
        /// </summary>
        PostMix = SYSTEM_CALLBACK_TYPE.POSTMIX,

        /// <summary>
        /// Called when each API function returns an error code, including delayed async functions.
        /// </summary>
        Error = SYSTEM_CALLBACK_TYPE.ERROR,

        /// <summary>
        /// Called each tick in mix update after clocks have been updated before the main mix occurs.
        /// </summary>
        MidiMix = SYSTEM_CALLBACK_TYPE.MIDMIX,

        /// <summary>
        /// Called directly when a thread is destroyed.
        /// </summary>
        ThreadDestroyed = SYSTEM_CALLBACK_TYPE.THREADDESTROYED,

        /// <summary>
        /// Called at start of System::update function.
        /// </summary>
        PreUpdate = SYSTEM_CALLBACK_TYPE.PREUPDATE,

        /// <summary>
        /// Called at end of System::update function.
        /// </summary>
        PostUpdate = SYSTEM_CALLBACK_TYPE.POSTUPDATE
    }
}
