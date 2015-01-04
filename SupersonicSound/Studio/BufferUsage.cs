using FMOD.Studio;

namespace SupersonicSound.Studio
{
    public struct BufferUsage
    {
        /// <summary>
        ///  Information for the Studio Async Command buffer, controlled by FMOD_STUDIO_ADVANCEDSETTINGS commandQueueSize.
        /// </summary>
        public BufferInfo studioCommandQueue { get; private set; }

        /// <summary>
        /// Information for the Studio handle table, controlled by FMOD_STUDIO_ADVANCEDSETTINGS handleInitialSize.
        /// </summary>
        public BufferInfo studioHandle { get; private set; }

        public BufferUsage(BUFFER_USAGE usage)
            : this()
        {
            studioCommandQueue = new BufferInfo(usage.studioCommandQueue);
            studioHandle = new BufferInfo(usage.studioHandle);
        }
    }
}
