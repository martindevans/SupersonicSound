using FMOD;
using SupersonicSound.Studio;
using SupersonicSound.Wrapper;
using System;
using System.Text;

namespace SupersonicSound.LowLevel
{
    public sealed class LowLevelSystem
        : IPreInitilizeLowLevelSystem, IDisposable
    {
        private readonly FMOD.System _system;

        private bool _disposed;

        public LowLevelSystem(FMOD.System system)
        {
            _listenerCollection = new ListenerCollection(this);
            _disposed = false;

            _system = system;

            _reverbController = new ReverbPropertiesController(_system);
        }

        public void SetAdvancedSettings(AdvancedInitializationSettings advancedInitializationSettings)
        {
            var init = advancedInitializationSettings.ToFmod();
            _system.setAdvancedSettings(ref init).Check();
        }

        #region pre initialize
        OutputMode IPreInitilizeLowLevelSystem.Output
        {
            get
            {
                OUTPUTTYPE output;
                _system.getOutput(out output).Check();
                return (OutputMode)output;
            }
            set
            {
                _system.setOutput((OUTPUTTYPE)value).Check();
            }
        }

        int IPreInitilizeLowLevelSystem.GetNumDrivers()
        {
            int drivers;
            _system.getNumDrivers(out drivers).Check();
            return drivers;
        }

        DriverInfo IPreInitilizeLowLevelSystem.GetDriverInfo(int id)
        {
            StringBuilder name = new StringBuilder(128);
            GUID guid;
            int systemRate;
            SPEAKERMODE speakerMode;
            int channels;
            _system.getDriverInfo(id, name, name.Capacity, out guid, out systemRate, out speakerMode, out channels).Check();

            return new DriverInfo(
                id,
                name.ToString(),
                guid.FromFmod(),
                systemRate,
                (SpeakerMode)speakerMode,
                channels
            );
        }

        int IPreInitilizeLowLevelSystem.Driver
        {
            get
            {
                int driver;
                _system.getDriver(out driver).Check();
                return driver;
            }
            set
            {
                _system.setDriver(value).Check();
            }
        }

        int IPreInitilizeLowLevelSystem.SoftwareChannels
        {
            get
            {
                int num;
                _system.getSoftwareChannels(out num).Check();
                return num;
            }
            set
            {
                _system.setSoftwareChannels(value).Check();
            }
        }

        SoftwareFormat IPreInitilizeLowLevelSystem.Format
        {
            get
            {
                int rate;
                SPEAKERMODE mode;
                int speakers;
                _system.getSoftwareFormat(out rate, out mode, out speakers).Check();

                return new SoftwareFormat(rate, (SpeakerMode)mode, speakers);
            }
            set
            {
                _system.setSoftwareFormat(value.SampleRate, (SPEAKERMODE)value.SpeakerMode, value.NumRawSpeakers).Check();
            }
        }

        DspBufferConfiguration IPreInitilizeLowLevelSystem.DspBufferConfiguration
        {
            get
            {
                uint length;
                int num;
                _system.getDSPBufferSize(out length, out num).Check();

                return new DspBufferConfiguration(length, num);
            }
            set
            {
                _system.setDSPBufferSize(value.BufferLength, value.NumBuffers).Check();
            }
        }

        private IFileSystemWrapper _fileSystem;
        void IPreInitilizeLowLevelSystem.SetFileSystem<THandle>(IFileSystem<THandle> fileSystem)
        {
            _fileSystem = new FileSystemWrapper<THandle>(fileSystem);
            _system.setFileSystem(
                _fileSystem.UserOpen,
                _fileSystem.UserClose,
                _fileSystem.UserRead,// null,                       // | Null because UserAsyncRead is specified
                _fileSystem.UserSeek,// null,                       // | and will be used instead
                null,//_fileSystem.UserAsyncRead,
                null,//_fileSystem.UserAsyncCancel,
                fileSystem.BlockAlign
            ).Check();
        }

        private Action<string, uint, IntPtr> _opened;
        private Action<IntPtr> _closed;
        private Action<IntPtr, uint, uint> _read;
        private Action<IntPtr, uint> _seeked;

        void IPreInitilizeLowLevelSystem.AttachFileSystem(
            Action<string, uint, IntPtr> opened,
            Action<IntPtr> closed,
            Action<IntPtr, uint, uint> read,
            Action<IntPtr, uint> seeked
        )
        {
            _opened = opened;
            _closed = closed;
            _read = read;
            _seeked = seeked;

            _system.attachFileSystem(
                OpenedCallback,
                ClosedCallback,
                ReadCallback,
                SeekedCallback
            ).Check();
        }

        private RESULT OpenedCallback(string name, ref uint filesize, ref IntPtr handle, IntPtr userdata)
        {
            if (_opened != null)
                _opened(name, filesize, handle);
            return RESULT.OK;
        }

        private RESULT ClosedCallback(IntPtr handle, IntPtr userdata)
        {
            if (_closed != null)
                _closed(handle);
            return RESULT.OK;
        }

        private RESULT ReadCallback(IntPtr handle, IntPtr buffer, uint sizebytes, ref uint bytesread, IntPtr userdata)
        {
            if (_read != null)
                _read(handle, sizebytes, bytesread);
            return RESULT.OK;
        }

        private RESULT SeekedCallback(IntPtr handle, uint pos, IntPtr userdata)
        {
            if (_seeked != null)
                _seeked(handle, pos);
            return RESULT.OK;
        }

        void IPreInitilizeLowLevelSystem.SetCallback(Action<LowLevelSystem, SystemCallbackType, IntPtr, IntPtr> callback, SystemCallbackType callbackMask)
        {
            _system.setCallback((_, type, cd1, cd2, __) => {

                callback(this, (SystemCallbackType)type, cd1, cd2);
                return RESULT.OK;
            }, (SYSTEM_CALLBACK_TYPE)callbackMask).Check();
        }
        #endregion

        #region init/close
        public void Initialize(int maxchannels, LowLevelInitFlags flags)
        {
            _system.init(maxchannels, (INITFLAGS) flags, IntPtr.Zero).Check();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                }

                _system.close().Check();

                _disposed = true;
            }
        }

        ~LowLevelSystem()
        {
            Dispose(false);
        }
        #endregion

        #region plugin support
        public void SetPluginPath(string path)
        {
            _system.setPluginPath(path).Check();
        }

        public Plugin LoadPlugin(string filename, uint priority)
        {
            uint handle;
            _system.loadPlugin(filename, out handle, priority).Check();
            return new Plugin(handle, _system);
        }

        public Plugin LoadPlugin(string filename)
        {
            uint handle;
            _system.loadPlugin(filename, out handle).Check();
            return new Plugin(handle, _system);
        }

        public int GetNumPlugins(PluginType pluginType)
        {
            int num;
            _system.getNumPlugins((PLUGINTYPE)pluginType, out num).Check();
            return num;
        }

        public Plugin GetPlugin(PluginType type, int index)
        {
            uint handle;
            _system.getPluginHandle((PLUGINTYPE)type, index, out handle).Check();
            return new Plugin(handle, _system);
        }

        public Plugin OutputByPlugin
        {
            get
            {
                uint handle;
                _system.getOutputByPlugin(out handle).Check();
                return new Plugin(handle, _system);
            }
            set
            {
                _system.setOutputByPlugin(value.Handle).Check();
            }
        }

        public DSP CreateDSPByPlugin(Plugin plugin)
        {
            FMOD.DSP dsp;
            _system.createDSPByPlugin(plugin.Handle, out dsp).Check();
            return DSP.FromFmod(dsp);
        }

        //public RESULT registerCodec(ref CODEC_DESCRIPTION description, out uint handle, uint priority)
        //{
        //    return FMOD5_System_RegisterCodec(rawPtr, ref description, out handle, priority);
        //}
        //*/
        //public RESULT registerDSP(ref DSP_DESCRIPTION description, out uint handle)
        //{
        //    return FMOD5_System_RegisterDSP(rawPtr, ref description, out handle);
        //}
        ///*
        //public RESULT registerOutput(ref OUTPUT_DESCRIPTION description, out uint handle)
        //{
        //    return FMOD5_System_RegisterOutput(rawPtr, ref description, out handle);
        //}
        //*/
        #endregion

        #region general post init functions
        public void Update()
        {
            _system.update().Check();
        }

        public void SetSpeakerPosition(Speaker speaker, float x, float y, bool active)
        {
            _system.setSpeakerPosition((SPEAKER)speaker, x, y, active).Check();
        }

        public void GetSpeakerPosition(Speaker speaker, out float x, out float y, out bool active)
        {
            _system.getSpeakerPosition((SPEAKER)speaker, out x, out y, out active).Check();
        }

        public void SetStreamBufferSize(uint size, TimeUnit unit)
        {
            _system.setStreamBufferSize(size, (TIMEUNIT)unit).Check();
        }

        public void GetStreamBufferSize(out uint size, out TimeUnit unit)
        {
            TIMEUNIT fu;
            _system.getStreamBufferSize(out size, out fu).Check();
            unit = (TimeUnit)fu;
        }

        public void Set3DSettings(float dopplerScale, float distanceFactor, float rolloffScale)
        {
            _system.set3DSettings(dopplerScale, distanceFactor, rolloffScale).Check();
        }

        public void Get3DSettings(out float dopplerScale, out float distanceFactor, out float rolloffScale)
        {
            _system.get3DSettings(out dopplerScale, out distanceFactor, out rolloffScale).Check();
        }

        public int Num3DListeners
        {
            get
            {
                int num;
                _system.get3DNumListeners(out num).Check();
                return num;
            }
            set
            {
                _system.set3DNumListeners(value).Check();
            }
        }

        private readonly ListenerCollection _listenerCollection;
        public struct ListenerCollection
        {
            private readonly LowLevelSystem _lowLevelSystem;

            public ListenerCollection(LowLevelSystem lowLevelSystem)
            {
                _lowLevelSystem = lowLevelSystem;
            }

            public Listener3D this[int index]
            {
                get
                {
                    return new Listener3D(_lowLevelSystem, index);
                }
            }
        }

        public struct Listener3D
        {
            private readonly LowLevelSystem _lowLevelSystem;
            private readonly int _index;

            public Listener3D(LowLevelSystem lowLevelSystem, int index)
            {
                _lowLevelSystem = lowLevelSystem;
                _index = index;
            }

            public void SetAttributes(Vector3 position, Vector3 velocity, Vector3 forward, Vector3 up)
            {
                var pos = position.ToFmod();
                var vel = velocity.ToFmod();
                var forv = forward.ToFmod();
                var upv = up.ToFmod();
                _lowLevelSystem._system.set3DListenerAttributes(_index, ref pos, ref vel, ref forv, ref upv).Check();
            }

            public void GetAttributes(out Vector3 position, out Vector3 velocity, out Vector3 forward, out Vector3 up)
            {
                VECTOR pos;
                VECTOR vel;
                VECTOR forv;
                VECTOR upv;
                _lowLevelSystem._system.get3DListenerAttributes(_index, out pos, out vel, out forv, out upv).Check();
                position = new Vector3(pos);
                velocity = new Vector3(vel);
                forward = new Vector3(forv);
                up = new Vector3(upv);
            }
        }

        public ListenerCollection Listeners
        {
            get
            {
                return _listenerCollection;
            }
        }

        public void MixerSuspend()
        {
            _system.mixerSuspend().Check();
        }

        public void MixerResume()
        {
            _system.mixerResume().Check();
        }
        #endregion

        #region system information functions
        public uint Version
        {
            get
            {
                uint v;
                _system.getVersion(out v).Check();
                return v;
            }
        }

        public IntPtr GetOutputHandle()
        {
            IntPtr ptr;
            _system.getOutputHandle(out ptr).Check();
            return ptr;
        }

        public int ChannelsPlaying
        {
            get
            {
                int channels;
                _system.getChannelsPlaying(out channels).Check();
                return channels;
            }
        }

        public CpuUsage GetCpuUsage()
        {
            float dsp;
            float stream;
            float geometry;
            float update;
            float total;
            _system.getCPUUsage(out dsp, out stream, out geometry, out update, out total).Check();

            return new CpuUsage(dsp, stream, geometry, update, 0);
        }

        public void GetRamUsage(out int currentalloced, out int maxalloced, out int total)
        {
            _system.getSoundRAM(out currentalloced, out maxalloced, out total).Check();
        }
        #endregion

        #region Sound/DSP/Channel/FX creation and retrieval
        public Sound CreateSound(string name, Mode mode, CreateSoundExInfo info)
        {
            FMOD.Sound sound;
            _system.createSound(name, (MODE)mode, ref info.FMODInfo, out sound).Check();
            return Sound.FromFmod(sound);
        }

        public Sound CreateSound(string name, Mode mode)
        {
            FMOD.Sound sound;
            _system.createSound(name, (MODE)mode, out sound).Check();
            return Sound.FromFmod(sound);
        }

        public Sound CreateSound(byte[] data, Mode mode, ref CreateSoundExInfo info)
        {
            FMOD.Sound sound;
            _system.createSound(data, (MODE)mode, ref info.FMODInfo, out sound).Check();
            return Sound.FromFmod(sound);
        }

        public Sound CreateStream(string name, Mode mode, ref CreateSoundExInfo info)
        {
            FMOD.Sound sound;
            _system.createStream(name, (MODE)mode, ref info.FMODInfo, out sound).Check();
            return Sound.FromFmod(sound);
        }

        public Sound CreateStream(string name, Mode mode)
        {
            FMOD.Sound sound;
            _system.createStream(name, (MODE)mode, out sound).Check();
            return Sound.FromFmod(sound);
        }

        public Sound CreateStream(byte[] data, Mode mode, ref CreateSoundExInfo info)
        {
            FMOD.Sound sound;
            _system.createStream(data, (MODE)mode, ref info.FMODInfo, out sound).Check();
            return Sound.FromFmod(sound);
        }

        //todo: create DSP from description
        //public DSP CreateDSP(ref DspDescription description)
        //{
        //    DSP_DESCRIPTION d = description.ToFmod();
        //    FMOD.DSP dsp;
        //    _system.createDSP(ref d, out dsp).Check();
        //    return DSP.FromFmod(dsp);
        //}

        public DSP CreateDSP(DspType type)
        {
            FMOD.DSP dsp;
            _system.createDSPByType((DSP_TYPE)type, out dsp).Check();

            return DSP.FromFmod(dsp);
        }

        public ChannelGroup CreateChannelGroup(string name)
        {
            FMOD.ChannelGroup group;
            _system.createChannelGroup(name, out group).Check();
            return ChannelGroup.FromFmod(group);
        }

        public SoundGroup CreateSoundGroup(string name)
        {
            FMOD.SoundGroup group;
            _system.createSoundGroup(name, out group).Check();
            return new SoundGroup(group);
        }

        public Reverb3D CreateReverb3D()
        {
            FMOD.Reverb3D reverb;
            _system.createReverb3D(out reverb).Check();
            return new Reverb3D(reverb);
        }

        public Channel PlaySound(Sound sound, ChannelGroup? channelGroup, bool paused)
        {
            FMOD.Channel channel;
            _system.playSound(sound.FmodSound, channelGroup.HasValue ? channelGroup.Value.FmodGroup : null, paused, out channel).Check();
            return Channel.FromFmod(channel);
        }

        public Channel PlayDSP(DSP dsp, ChannelGroup? channelGroup, bool paused)
        {
            FMOD.Channel channel;
            _system.playDSP(dsp.FmodDsp, channelGroup.HasValue ? channelGroup.Value.FmodGroup : null, paused, out channel).Check();

            return Channel.FromFmod(channel);
        }

        public Channel GetChannel(int channelid)
        {
            FMOD.Channel channel;
            _system.getChannel(channelid, out channel).Check();

            return Channel.FromFmod(channel);
        }

        public ChannelGroup MasterChannelGroup
        {
            get
            {
                FMOD.ChannelGroup group;
                _system.getMasterChannelGroup(out group).Check();
                return ChannelGroup.FromFmod(group);
            }
        }

        public SoundGroup MasterSoundGroup
        {
            get
            {
                FMOD.SoundGroup group;
                _system.getMasterSoundGroup(out group).Check();
                return new SoundGroup(group);
            }
        }
        #endregion

        #region Routing to ports
        public void AttachChannelGroupToPort(uint portType, ulong portIndex, ChannelGroup channelgroup)
        {
            _system.attachChannelGroupToPort(portType, portIndex, channelgroup.FmodGroup).Check();
        }

        public void DetachChannelGroupFromPort(ChannelGroup channelgroup)
        {
            _system.detachChannelGroupFromPort(channelgroup.FmodGroup).Check();
        }
        #endregion

        #region Reverb api
        private readonly ReverbPropertiesController _reverbController;

        public ReverbPropertiesController Reverb
        {
            get
            {
                return _reverbController;
            }
        }
        #endregion

        #region System level DSP functionality
        public void LockDSP()
        {
            _system.lockDSP().Check();
        }

        public void UnlockDSP()
        {
            _system.unlockDSP().Check();
        }
        #endregion

        #region Recording api
        public int GetRecordNumDrivers()
        {
            int num;
            _system.getRecordNumDrivers(out num).Check();
            return num;
        }

        public DriverInfo getRecordDriverInfo(int id)
        {
            StringBuilder name = new StringBuilder(128);
            GUID guid;
            int systemRate;
            SPEAKERMODE speakerMode;
            int channels;
            _system.getRecordDriverInfo(id, name, name.Capacity, out guid, out systemRate, out speakerMode, out channels).Check();

            return new DriverInfo(
                id,
                name.ToString(),
                guid.FromFmod(),
                systemRate,
                (SpeakerMode)speakerMode,
                channels
            );
        }

        public uint GetRecordPosition(int id)
        {
            uint pos;
            _system.getRecordPosition(id, out pos).Check();
            return pos;
        }

        public void RecordStart(int id, Sound sound, bool loop)
        {
            _system.recordStart(id, sound.FmodSound, loop);
        }

        public void recordStop(int id)
        {
            _system.recordStop(id).Check();
        }

        public bool IsRecording(int id)
        {
            bool recording;
            _system.isRecording(id, out recording).Check();
            return recording;
        }
        #endregion

        #region Geometry api
        public Geometry CreateGeometry(int maxpolygons, int maxvertices)
        {
            FMOD.Geometry geom;
            _system.createGeometry(maxpolygons, maxvertices, out geom).Check();

            return new Geometry(geom);
        }

        public float GeometryMaxWorldSize
        {
            get
            {
                float maxWorld;
                _system.getGeometrySettings(out maxWorld).Check();
                return maxWorld;
            }
            set
            {
                _system.setGeometrySettings(value).Check();
            }
        }

        public Geometry LoadGeometry(byte[] data)
        {
            unsafe
            {
                fixed (byte* ptr = &data[0])
                {
                    FMOD.Geometry geometry;
                    _system.loadGeometry(new IntPtr(ptr), data.Length, out geometry).Check();
                    return new Geometry(geometry);
                }
            }
        }

        public void GetGeometryOcclusion(Vector3 listener, Vector3 source, out float direct, out float reverb)
        {
            var l = listener.ToFmod();
            var s = source.ToFmod();
            _system.getGeometryOcclusion(ref l, ref s, out direct, out reverb).Check();
        }

        #endregion

        #region Network functions
        public string NetworkProxy
        {
            get
            {
                StringBuilder builder = new StringBuilder(1024);
                _system.getNetworkProxy(builder, builder.Capacity).Check();

                return builder.ToString();
            }
            set
            {
                _system.setNetworkProxy(value);
            }
        }

        public int NetworkTimeout
        {
            get
            {
                int timeout;
                _system.getNetworkTimeout(out timeout).Check();
                return timeout;
            }
            set
            {
                _system.setNetworkTimeout(value).Check();
            }
        }
        #endregion

        #region Userdata set/get
        public IntPtr UserData
        {
            get
            {
                IntPtr ptr;
                _system.getUserData(out ptr).Check();
                return ptr;
            }
            set
            {
                _system.setUserData(value).Check();
            }
        }
        #endregion
    }
}
