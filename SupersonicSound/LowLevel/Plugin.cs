using FMOD;
using SupersonicSound.Wrapper;
using System.Text;

namespace SupersonicSound.LowLevel
{
    public struct Plugin
    {
        private readonly FMOD.System _system;

        internal readonly uint Handle;

        public Plugin(uint handle, FMOD.System system)
        {
            Handle = handle;
            _system = system;
        }

        public void Unload()
        {
            _system.unloadPlugin(Handle).Check();
        }

        public PluginType Type
        {
            get
            {
                PluginType type;
                string name;
                uint version;
                GetInfo(out type, out name, out version);
                return type;
            }
        }

        public string Name
        {
            get
            {
                PluginType type;
                string name;
                uint version;
                GetInfo(out type, out name, out version);
                return name;
            }
        }

        public uint Version
        {
            get
            {
                PluginType type;
                string name;
                uint version;
                GetInfo(out type, out name, out version);
                return version;
            }
        }

        private void GetInfo(out PluginType type, out string name, out uint version)
        {
            PLUGINTYPE ptype;
            StringBuilder nameBuilder = new StringBuilder(128);
            _system.getPluginInfo(Handle, out ptype, nameBuilder, nameBuilder.Capacity, out version).Check();

            name = nameBuilder.ToString();
            type = (PluginType)ptype;
        }
    }
}
