using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMOD;
using FMOD.Studio;

namespace SupersonicSound.Studio
{
    public struct CpuUsage
    {
        /// <summary>
        /// Returns the % CPU time taken by DSP processing on the low level mixer thread.
        /// </summary>
        public float DSPUsage { get; private set; }

        /// <summary>
        /// Returns the % CPU time taken by stream processing on the low level stream thread.
        /// </summary>
        public float StreamUsage { get; private set; }

        /// <summary>
        /// Returns the % CPU time taken by geometry processing on the low level geometry thread.
        /// </summary>
        public float GeometryUsage { get; private set; }

        /// <summary>
        /// Returns the % CPU time taken by low level update, called as part of the studio update.
        /// </summary>
        public float UpdateUsage { get; private set; }

        /// <summary>
        /// Returns the % CPU time taken by studio update, called from the studio thread. Does not include low level update time.
        /// </summary>
        public float StudioUsage { get; private set; }

        public CpuUsage(CPU_USAGE usage)
            : this()
        {
            DSPUsage = usage.dspUsage;
            StreamUsage = usage.streamUsage;
            GeometryUsage = usage.geometryUsage;
            UpdateUsage = usage.updateUsage;
            StudioUsage = usage.studioUsage;
        }

        public CpuUsage(float dsp, float stream, float geometry, float update, float studio)
            : this()
        {
            DSPUsage = dsp;
            StreamUsage = stream;
            GeometryUsage = geometry;
            UpdateUsage = update;
            StudioUsage = studio;
        }
    }
}
