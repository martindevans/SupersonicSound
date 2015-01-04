using FMOD.Studio;
using System;

namespace SupersonicSound.Studio
{
    public struct BufferInfo
    {
        /// <summary>
        /// Current buffer usage in bytes.
        /// </summary>
        public int CurrentUsage { get; private set; }
        /// <summary>
        /// Peak buffer usage in bytes.
        /// </summary>
        public int PeakUsage { get; private set; }

        /// <summary>
        /// Buffer capacity in bytes.
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// Number of stalls due to buffer overflow.
        /// </summary>
        public int StallCount { get; private set; }

        /// <summary>
        /// Amount of time stalled due to buffer overflow, in seconds.
        /// </summary>
        public TimeSpan StallTime { get; private set; }

        public BufferInfo(BUFFER_INFO info)
            : this()
        {
            CurrentUsage = info.currentUsage;
            PeakUsage = info.peakUsage;
            Capacity = info.capacity;
            StallCount = info.stallCount;
            StallTime = TimeSpan.FromSeconds(info.stallTime);
        }
    }
}
