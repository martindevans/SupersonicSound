using System;

namespace SupersonicSound.LowLevel
{
    public struct DriverInfo
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public Guid Guid { get; private set; }

        public int SystemRate { get; private set; }

        public SpeakerMode SpeakerMode { get; private set; }

        public int SpeakerModeChannels { get; private set; }

        public DriverInfo(int id, string name, Guid guid, int systemRate, SpeakerMode speakerMode, int speakerModeChannels)
            :this()
        {
            Id = id;
            Name = name;
            Guid = guid;
            SystemRate = systemRate;
            SpeakerMode = speakerMode;
            SpeakerModeChannels = speakerModeChannels;
        }
    }
}
