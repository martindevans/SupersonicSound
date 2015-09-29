using FMOD;

namespace SupersonicSound.LowLevel
{
    [EquivalentEnum(typeof(DRIVER_STATE))]
    public enum DriverState : uint
    {
        /// <summary>
        /// Device is currently plugged in.
        /// </summary>
        Connected = DRIVER_STATE.CONNECTED,

        /// <summary>
        /// Device is the users preferred choice.
        /// </summary>
        Default = DRIVER_STATE.DEFAULT
    }
}
