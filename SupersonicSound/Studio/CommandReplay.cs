using System;
using FMOD;
using FMOD.Studio;
using SupersonicSound.Wrapper;

namespace SupersonicSound.Studio
{
    public struct CommandReplay
        : IEquatable<CommandReplay>
    {
        public FMOD.Studio.CommandReplay FmodCommandReplay { get; private set; }

        private CommandReplay(FMOD.Studio.CommandReplay commandReplay)
            : this()
        {
            FmodCommandReplay = commandReplay;
        }

        public static CommandReplay FromFmod(FMOD.Studio.CommandReplay commandReplay)
        {
            return new CommandReplay(commandReplay);
        }

        #region equality
        public bool Equals(CommandReplay other)
        {
            return other.FmodCommandReplay == FmodCommandReplay;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CommandReplay))
                return false;

            return Equals((CommandReplay)obj);
        }

        public override int GetHashCode()
        {
            return (FmodCommandReplay != null ? FmodCommandReplay.GetHashCode() : 0);
        }
        #endregion

        public bool IsPaused
        {
            get
            {
                bool paused;
                FmodCommandReplay.getPaused(out paused).Check();
                return paused;
            }
            set
            {
                FmodCommandReplay.setPaused(value).Check();
            }
        }
    }
}
