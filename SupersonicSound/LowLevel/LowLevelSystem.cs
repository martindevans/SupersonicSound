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

        //public RESULT loadPlugin(string filename, out uint handle, uint priority)
        //{
        //    return FMOD5_System_LoadPlugin(rawPtr, Encoding.UTF8.GetBytes(filename + Char.MinValue), out handle, priority);
        //}
        //public RESULT loadPlugin(string filename, out uint handle)
        //{
        //    return loadPlugin(filename, out handle, 0);
        //}
        //public RESULT unloadPlugin(uint handle)
        //{
        //    return FMOD5_System_UnloadPlugin(rawPtr, handle);
        //}
        //public RESULT getNumPlugins(PLUGINTYPE plugintype, out int numplugins)
        //{
        //    return FMOD5_System_GetNumPlugins(rawPtr, plugintype, out numplugins);
        //}
        //public RESULT getPluginHandle(PLUGINTYPE plugintype, int index, out uint handle)
        //{
        //    return FMOD5_System_GetPluginHandle(rawPtr, plugintype, index, out handle);
        //}
        //public RESULT getPluginInfo(uint handle, out PLUGINTYPE plugintype, StringBuilder name, int namelen, out uint version)
        //{
        //    IntPtr stringMem = Marshal.AllocHGlobal(name.Capacity);

        //    RESULT result = FMOD5_System_GetPluginInfo(rawPtr, handle, out plugintype, stringMem, namelen, out version);

        //    StringMarshalHelper.NativeToBuilder(name, stringMem);
        //    Marshal.FreeHGlobal(stringMem);

        //    return result;
        //}
        //public RESULT setOutputByPlugin(uint handle)
        //{
        //    return FMOD5_System_SetOutputByPlugin(rawPtr, handle);
        //}
        //public RESULT getOutputByPlugin(out uint handle)
        //{
        //    return FMOD5_System_GetOutputByPlugin(rawPtr, out handle);
        //}
        //public RESULT createDSPByPlugin(uint handle, out DSP dsp)
        //{
        //    dsp = null;

        //    IntPtr dspraw;
        //    RESULT result = FMOD5_System_CreateDSPByPlugin(rawPtr, handle, out dspraw);
        //    dsp = new DSP(dspraw);

        //    return result;
        //}
        //public RESULT getDSPInfoByPlugin(uint handle, out IntPtr description)
        //{
        //    return FMOD5_System_GetDSPInfoByPlugin(rawPtr, handle, out description);
        //}
        ///*
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

        //public void setSpeakerPosition(SPEAKER speaker, float x, float y, bool active)
        //{
        //    return FMOD5_System_SetSpeakerPosition(rawPtr, speaker, x, y, active);
        //}
        //public RESULT getSpeakerPosition(SPEAKER speaker, out float x, out float y, out bool active)
        //{
        //    return FMOD5_System_GetSpeakerPosition(rawPtr, speaker, out x, out y, out active);
        //}
        //public RESULT setStreamBufferSize(uint filebuffersize, TIMEUNIT filebuffersizetype)
        //{
        //    return FMOD5_System_SetStreamBufferSize(rawPtr, filebuffersize, filebuffersizetype);
        //}
        //public RESULT getStreamBufferSize(out uint filebuffersize, out TIMEUNIT filebuffersizetype)
        //{
        //    return FMOD5_System_GetStreamBufferSize(rawPtr, out filebuffersize, out filebuffersizetype);
        //}
        //public RESULT set3DSettings(float dopplerscale, float distancefactor, float rolloffscale)
        //{
        //    return FMOD5_System_Set3DSettings(rawPtr, dopplerscale, distancefactor, rolloffscale);
        //}
        //public RESULT get3DSettings(out float dopplerscale, out float distancefactor, out float rolloffscale)
        //{
        //    return FMOD5_System_Get3DSettings(rawPtr, out dopplerscale, out distancefactor, out rolloffscale);
        //}
        //public RESULT set3DNumListeners(int numlisteners)
        //{
        //    return FMOD5_System_Set3DNumListeners(rawPtr, numlisteners);
        //}
        //public RESULT get3DNumListeners(out int numlisteners)
        //{
        //    return FMOD5_System_Get3DNumListeners(rawPtr, out numlisteners);
        //}
        //public RESULT set3DListenerAttributes(int listener, ref VECTOR pos, ref VECTOR vel, ref VECTOR forward, ref VECTOR up)
        //{
        //    return FMOD5_System_Set3DListenerAttributes(rawPtr, listener, ref pos, ref vel, ref forward, ref up);
        //}
        //public RESULT get3DListenerAttributes(int listener, out VECTOR pos, out VECTOR vel, out VECTOR forward, out VECTOR up)
        //{
        //    return FMOD5_System_Get3DListenerAttributes(rawPtr, listener, out pos, out vel, out forward, out up);
        //}
        //public RESULT set3DRolloffCallback(CB_3D_ROLLOFFCALLBACK callback)
        //{
        //    return FMOD5_System_Set3DRolloffCallback(rawPtr, callback);
        //}
        //public RESULT mixerSuspend()
        //{
        //    return FMOD5_System_MixerSuspend(rawPtr);
        //}
        //public RESULT mixerResume()
        //{
        //    return FMOD5_System_MixerResume(rawPtr);
        //}
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
        //public RESULT createSound(string name, MODE mode, ref CREATESOUNDEXINFO exinfo, out Sound sound)
        //{
        //    sound = null;

        //    byte[] stringData;
        //    stringData = Encoding.UTF8.GetBytes(name + Char.MinValue);

        //    exinfo.cbsize = Marshal.SizeOf(exinfo);

        //    IntPtr soundraw;
        //    RESULT result = FMOD5_System_CreateSound(rawPtr, stringData, mode, ref exinfo, out soundraw);
        //    sound = new Sound(soundraw);

        //    return result;
        //}
        //public RESULT createSound(byte[] data, MODE mode, ref CREATESOUNDEXINFO exinfo, out Sound sound)
        //{
        //    sound = null;

        //    exinfo.cbsize = Marshal.SizeOf(exinfo);

        //    IntPtr soundraw;
        //    RESULT result = FMOD5_System_CreateSound(rawPtr, data, mode, ref exinfo, out soundraw);
        //    sound = new Sound(soundraw);

        //    return result;
        //}
        //public RESULT createSound(string name, MODE mode, out Sound sound)
        //{
        //    CREATESOUNDEXINFO exinfo = new CREATESOUNDEXINFO();
        //    exinfo.cbsize = Marshal.SizeOf(exinfo);

        //    return createSound(name, mode, ref exinfo, out sound);
        //}
        //public RESULT createStream(string name, MODE mode, ref CREATESOUNDEXINFO exinfo, out Sound sound)
        //{
        //    sound = null;

        //    byte[] stringData;
        //    stringData = Encoding.UTF8.GetBytes(name + Char.MinValue);

        //    exinfo.cbsize = Marshal.SizeOf(exinfo);

        //    IntPtr soundraw;
        //    RESULT result = FMOD5_System_CreateStream(rawPtr, stringData, mode, ref exinfo, out soundraw);
        //    sound = new Sound(soundraw);

        //    return result;
        //}
        //public RESULT createStream(byte[] data, MODE mode, ref CREATESOUNDEXINFO exinfo, out Sound sound)
        //{
        //    sound = null;

        //    exinfo.cbsize = Marshal.SizeOf(exinfo);

        //    IntPtr soundraw;
        //    RESULT result = FMOD5_System_CreateStream(rawPtr, data, mode, ref exinfo, out soundraw);
        //    sound = new Sound(soundraw);

        //    return result;
        //}
        //public RESULT createStream(string name, MODE mode, out Sound sound)
        //{
        //    CREATESOUNDEXINFO exinfo = new CREATESOUNDEXINFO();
        //    exinfo.cbsize = Marshal.SizeOf(exinfo);

        //    return createStream(name, mode, ref exinfo, out sound);
        //}

        //public RESULT createDSP(ref DSP_DESCRIPTION description, out DSP dsp)
        //{
        //    dsp = null;

        //    IntPtr dspraw;
        //    RESULT result = FMOD5_System_CreateDSP(rawPtr, ref description, out dspraw);
        //    dsp = new DSP(dspraw);

        //    return result;
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

        //public RESULT createSoundGroup(string name, out SoundGroup soundgroup)
        //{
        //    soundgroup = null;

        //    byte[] stringData = Encoding.UTF8.GetBytes(name + Char.MinValue);

        //    IntPtr soundgroupraw;
        //    RESULT result = FMOD5_System_CreateSoundGroup(rawPtr, stringData, out soundgroupraw);
        //    soundgroup = new SoundGroup(soundgroupraw);

        //    return result;
        //}
        //public RESULT createReverb3D(out Reverb3D reverb)
        //{
        //    IntPtr reverbraw;
        //    RESULT result = FMOD5_System_CreateReverb3D(rawPtr, out reverbraw);
        //    reverb = new Reverb3D(reverbraw);

        //    return result;
        //}
        //public RESULT playSound(Sound sound, ChannelGroup channelGroup, bool paused, out Channel channel)
        //{
        //    channel = null;

        //    IntPtr channelGroupRaw = (channelGroup != null) ? channelGroup.getRaw() : IntPtr.Zero;

        //    IntPtr channelraw;
        //    RESULT result = FMOD5_System_PlaySound(rawPtr, sound.getRaw(), channelGroupRaw, paused, out channelraw);
        //    channel = new Channel(channelraw);

        //    return result;
        //}

        public Channel PlayDSP(DSP dsp, ChannelGroup channelGroup, bool paused)
        {
            FMOD.Channel channel;
            _system.playDSP(dsp.ToFmod(), channelGroup.ToFmod(), paused, out channel).Check();

            return Channel.FromFmod(channel);
        }

        public Channel GetChannel(int channelid)
        {
            FMOD.Channel channel;
            _system.getChannel(channelid, out channel).Check();

            return Channel.FromFmod(channel);
        }

        //public RESULT getMasterChannelGroup(out ChannelGroup channelgroup)
        //{
        //    channelgroup = null;

        //    IntPtr channelgroupraw;
        //    RESULT result = FMOD5_System_GetMasterChannelGroup(rawPtr, out channelgroupraw);
        //    channelgroup = new ChannelGroup(channelgroupraw);

        //    return result;
        //}
        //public RESULT getMasterSoundGroup(out SoundGroup soundgroup)
        //{
        //    soundgroup = null;

        //    IntPtr soundgroupraw;
        //    RESULT result = FMOD5_System_GetMasterSoundGroup(rawPtr, out soundgroupraw);
        //    soundgroup = new SoundGroup(soundgroupraw);

        //    return result;
        //}
        #endregion

        #region Routing to ports
        public void AttachChannelGroupToPort(uint portType, ulong portIndex, ChannelGroup channelgroup)
        {
            _system.attachChannelGroupToPort(portType, portIndex, channelgroup.ToFmod()).Check();
        }

        public void DetachChannelGroupFromPort(ChannelGroup channelgroup)
        {
            _system.detachChannelGroupFromPort(channelgroup.ToFmod()).Check();
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
            _system.recordStart(id, sound.ToFmod(), loop);
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

            return Geometry.FromFmod(geom);
        }

        //public RESULT setGeometrySettings(float maxworldsize)
        //{
        //    return FMOD5_System_SetGeometrySettings(rawPtr, maxworldsize);
        //}
        //public RESULT getGeometrySettings(out float maxworldsize)
        //{
        //    return FMOD5_System_GetGeometrySettings(rawPtr, out maxworldsize);
        //}
        //public RESULT loadGeometry(IntPtr data, int datasize, out Geometry geometry)
        //{
        //    geometry = null;

        //    IntPtr geometryraw;
        //    RESULT result = FMOD5_System_LoadGeometry(rawPtr, data, datasize, out geometryraw);
        //    geometry = new Geometry(geometryraw);

        //    return result;
        //}
        //public RESULT getGeometryOcclusion(ref VECTOR listener, ref VECTOR source, out float direct, out float reverb)
        //{
        //    return FMOD5_System_GetGeometryOcclusion(rawPtr, ref listener, ref source, out direct, out reverb);
        //}
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
