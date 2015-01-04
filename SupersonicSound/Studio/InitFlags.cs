using FMOD.Studio;
using System;

namespace SupersonicSound.Studio
{
    [Flags]
    [EquivalentEnum(typeof(INITFLAGS))]
    public enum InitFlags
        : uint
    {
        /// <summary>
        /// Initialize normally.
        /// </summary>
        Normal = INITFLAGS.NORMAL,

        /// <summary>
        /// Enable live update.
        /// </summary>
        LiveUpdate = INITFLAGS.LIVEUPDATE,

        /// <summary>
        /// Load banks even if they reference plugins that have not been loaded.
        /// </summary>
        AllowMissingPlugins = INITFLAGS.ALLOW_MISSING_PLUGINS,

        /// <summary>
        /// Disable asynchronous processing and perform all processing on the calling thread instead.
        /// </summary>
        SynchronousUpdate = INITFLAGS.SYNCHRONOUS_UPDATE,
    }
}
