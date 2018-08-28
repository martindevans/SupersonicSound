using FMOD.Studio;
using System;

namespace SupersonicSound.Studio
{
    public struct BufferInfo
    {
        /// <summary>
        /// Current buffer usage in bytes.
        /// </summary>
        public int CurrentUsage { get; }
        /// <summary>
        /// Peak buffer usage in bytes.
        /// </summary>
        public int PeakUsage { get; }

        /// <summary>
        /// Buffer capacity in bytes.
        /// </summary>
        public int Capacity { get; }

        /// <summary>
        /// Number of stalls due to buffer overflow.
        /// </summary>
        public int StallCount { get; }

        /// <summary>
        /// Amount of time stalled due to buffer overflow, in seconds.
        /// </summary>
        public TimeSpan StallTime { get; }

        public BufferInfo(BUFFER_INFO info)
            : this()
        {
            CurrentUsage = info.currentusage;
            PeakUsage = info.peakusage;
            Capacity = info.capacity;
            StallCount = info.stallcount;
            StallTime = TimeSpan.FromSeconds(info.stalltime);
        }
    }
}
