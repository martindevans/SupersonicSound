using FMOD.Studio;

namespace SupersonicSound.Studio
{
    public struct BufferUsage
    {
        /// <summary>
        ///  Information for the Studio Async Command buffer, controlled by FMOD_STUDIO_ADVANCEDSETTINGS commandQueueSize.
        /// </summary>
        public BufferInfo StudioCommandQueue { get; private set; }

        /// <summary>
        /// Information for the Studio handle table, controlled by FMOD_STUDIO_ADVANCEDSETTINGS handleInitialSize.
        /// </summary>
        public BufferInfo StudioHandle { get; private set; }

        public BufferUsage(BUFFER_USAGE usage)
            : this()
        {
            StudioCommandQueue = new BufferInfo(usage.studioCommandQueue);
            StudioHandle = new BufferInfo(usage.studioHandle);
        }
    }
}
