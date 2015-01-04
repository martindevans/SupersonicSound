
namespace SupersonicSound.LowLevel
{
    public struct SoftwareFormat
    {
        public int SampleRate { get; private set; }

        public SpeakerMode SpeakerMode { get; private set; }

        public int NumRawSpeakers { get; private set; }

        public SoftwareFormat(int sampleRate, SpeakerMode speakerMode, int numRawSpeakers)
            :this()
        {
            SampleRate = sampleRate;
            SpeakerMode = speakerMode;
            NumRawSpeakers = numRawSpeakers;
        }
    }
}
