using FMOD.Studio;
using SupersonicSound.LowLevel;

namespace SupersonicSound.Studio
{
    public struct SoundInfo
    {
        private readonly SOUND_INFO _fmodSoundInfo;

        public Mode Mode
        {
            get
            {
                return (Mode)_fmodSoundInfo.mode;
            }
        }

        public int SubSoundIndex
        {
            get
            {
                return _fmodSoundInfo.subsoundIndex;
            }
        }

        public string Name
        {
            get
            {
                return _fmodSoundInfo.name;
            }
        }

        public CreateSoundExInfo CreateSoundInfo
        {
            get
            {
                return new CreateSoundExInfo(ref _fmodSoundInfo.exinfo);
            }
        }

        public SoundInfo(SOUND_INFO info)
        {
            _fmodSoundInfo = info;
        }
    }
}
