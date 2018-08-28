using FMOD.Studio;

namespace SupersonicSound.Studio
{
    public struct CpuUsage
    {
        /// <summary>
        /// Returns the % CPU time taken by DSP processing on the low level mixer thread.
        /// </summary>
        public float DspUsage { get; }

        /// <summary>
        /// Returns the % CPU time taken by stream processing on the low level stream thread.
        /// </summary>
        public float StreamUsage { get; }

        /// <summary>
        /// Returns the % CPU time taken by geometry processing on the low level geometry thread.
        /// </summary>
        public float GeometryUsage { get; }

        /// <summary>
        /// Returns the % CPU time taken by low level update, called as part of the studio update.
        /// </summary>
        public float UpdateUsage { get; }

        /// <summary>
        /// Returns the % CPU time taken by studio update, called from the studio thread. Does not include low level update time.
        /// </summary>
        public float StudioUsage { get; }

        public CpuUsage(CPU_USAGE usage)
            : this()
        {
            DspUsage = usage.dspusage;
            StreamUsage = usage.streamusage;
            GeometryUsage = usage.geometryusage;
            UpdateUsage = usage.updateusage;
            StudioUsage = usage.studiousage;
        }

        public CpuUsage(float dsp, float stream, float geometry, float update, float studio)
            : this()
        {
            DspUsage = dsp;
            StreamUsage = stream;
            GeometryUsage = geometry;
            UpdateUsage = update;
            StudioUsage = studio;
        }
    }
}
