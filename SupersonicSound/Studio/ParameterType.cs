using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMOD.Studio;

namespace SupersonicSound.Studio
{
    [EquivalentEnum(typeof(PARAMETER_TYPE))]
    public enum ParameterType
    {
        /// <summary>
        /// Controlled via the API using Studio::ParameterInstance::setValue.
        /// </summary>
        GameControlled = PARAMETER_TYPE.GAME_CONTROLLED,

        /// <summary>
        /// Distance between the event and the listener.
        /// </summary>
        AutoDistance = PARAMETER_TYPE.AUTOMATIC_DISTANCE,

        /// <summary>
        /// Angle between the event's forward vector and the vector pointing from the event to the listener (0 to 180 degrees).
        /// </summary>
        AutoEventConeAngle = PARAMETER_TYPE.AUTOMATIC_EVENT_CONE_ANGLE,

        /// <summary>
        /// Horizontal angle between the event's forward vector and listener's forward vector (-180 to 180 degrees).
        /// </summary>
        AutoEventOrientation = PARAMETER_TYPE.AUTOMATIC_EVENT_ORIENTATION,

        /// <summary>
        /// Horizontal angle between the listener's forward vector and the vector pointing from the listener to the event (-180 to 180 degrees).
        /// </summary>
        AutoDirection = PARAMETER_TYPE.AUTOMATIC_DIRECTION,

        /// <summary>
        /// Angle between the listener's XZ plane and the vector pointing from the listener to the event (-90 to 90 degrees).
        /// </summary>
        AutoElevation = PARAMETER_TYPE.AUTOMATIC_ELEVATION,

        /// <summary>
        /// Horizontal angle between the listener's forward vector and the global positive Z axis (-180 to 180 degrees).
        /// </summary>
        AutoListenerOrientation = PARAMETER_TYPE.AUTOMATIC_LISTENER_ORIENTATION,
    }
}
