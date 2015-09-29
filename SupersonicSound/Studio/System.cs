using System.Collections.Generic;
using System.Linq;
using FMOD;
using FMOD.Studio;
using SupersonicSound.LowLevel;
using SupersonicSound.Wrapper;
using System;
using INITFLAGS = FMOD.Studio.INITFLAGS;
using SYSTEM_CALLBACK_TYPE = FMOD.Studio.SYSTEM_CALLBACK_TYPE;

namespace SupersonicSound.Studio
{
    public sealed class System
        : IDisposable
    {
        private readonly FMOD.Studio.System _system;

        public LowLevelSystem LowLevelSystem { get; private set; }

        private bool _disposed;

        public System(int maxChannels = 1024, InitFlags flags = InitFlags.LiveUpdate, LowLevelInitFlags lowLevelFlags = LowLevelInitFlags.Normal, AdvancedInitializationSettings advancedSettings = default(AdvancedInitializationSettings), Action<IPreInitilizeLowLevelSystem> preInit = null)
        {
            //Load native dependencies
            Native.Load();

            //Create studio system
            FMOD.Studio.System.create(out _system).Check();

            //Create low level system
            FMOD.System lowLevel;
            _system.getLowLevelSystem(out lowLevel).Check();
            LowLevelSystem = new LowLevelSystem(lowLevel);

            if (preInit != null)
                preInit(LowLevelSystem);

            //Set advanced settings
            LowLevelSystem.SetAdvancedSettings(advancedSettings);

            //Initialize
            _system.initialize(maxChannels, (INITFLAGS)flags, (FMOD.INITFLAGS)lowLevelFlags, IntPtr.Zero).Check();
        }

        public System(FMOD.Studio.System system)
        {
            //Load native dependencies
            Native.Load();

            _system = system;
        }

        public void Update()
        {
            _system.update().Check();
        }

        #region disposal
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
                    // Dispose our LowLevelSystem class
                    LowLevelSystem.Dispose();
                    LowLevelSystem = null;
                }

                _system.release().Check();

                _disposed = true;
            }
        }

        ~System()
        {
            Dispose(false);
        }
        #endregion

        public EventDescription GetEvent(string path)
        {
            FMOD.Studio.EventDescription evt;
            _system.getEvent(path, out evt).Check();

            return EventDescription.FromFmod(evt);
        }

        public EventDescription GetEvent(Guid id)
        {
            FMOD.Studio.EventDescription evt;
            _system.getEventByID(id, out evt).Check();

            return EventDescription.FromFmod(evt);
        }

        public Bus GetBus(string path)
        {
            FMOD.Studio.Bus bus;
            _system.getBus(path, out bus).Check();

            return Bus.FromFmod(bus);
        }

        public Bus GetBus(Guid guid)
        {
            FMOD.Studio.Bus bus;
            _system.getBusByID(guid, out bus).Check();

            return Bus.FromFmod(bus);
        }

        public VCA GetVCA(string path)
        {
            FMOD.Studio.VCA vca;
            _system.getVCA(path, out vca).Check();

            return VCA.FromFmod(vca);
        }

        public VCA GetVCA(Guid guid)
        {
            FMOD.Studio.VCA vca;
            _system.getVCAByID(guid, out vca).Check();

            return VCA.FromFmod(vca);
        }

        public Bank GetBank(string path)
        {
            FMOD.Studio.Bank bank;
            _system.getBank(path, out bank).Check();

            return Bank.FromFmod(bank);
        }

        public Bank GetBank(Guid guid)
        {
            FMOD.Studio.Bank bank;
            _system.getBankByID(guid, out bank).Check();

            return Bank.FromFmod(bank);
        }

        public SoundInfo GetSoundInfo(string key)
        {
            SOUND_INFO info;
            _system.getSoundInfo(key, out info).Check();
            return new SoundInfo(info);
        }

        public Guid LookupId(string path)
        {
            Guid guid;
            _system.lookupID(path, out guid).Check();
            return guid;
        }

        public string LookupPath(Guid id)
        {
            string path;
            _system.lookupPath(id, out path).Check();
            return path;
        }

        public Attributes3D GetListenerAttributes(int listener)
        {
            FMOD.Studio._3D_ATTRIBUTES attr;
            _system.getListenerAttributes(listener, out attr).Check();
            return new Attributes3D(ref attr);
        }

        public void SetListenerAttributes(int listener, Attributes3D value)
        {
            _system.setListenerAttributes(listener, value.ToFmod()).Check();
        }

        public Bank LoadBankFromFile(string name, BankLoadingFlags flags)
        {
            FMOD.Studio.Bank bank;
            _system.loadBankFile(name, (LOAD_BANK_FLAGS)flags, out bank).Check();
            return Bank.FromFmod(bank);
        }

        public Bank LoadBankFromMemory(byte[] buffer, BankLoadingFlags flags)
        {
            FMOD.Studio.Bank bank;
            _system.loadBankMemory(buffer, (LOAD_BANK_FLAGS)flags, out bank).Check();
            return Bank.FromFmod(bank);
        }

        //public RESULT loadBankCustom(BANK_INFO info, LOAD_BANK_FLAGS flags, out Bank bank)
        //{
        //    bank = null;

        //    info.size = Marshal.SizeOf(info);

        //    IntPtr newPtr = new IntPtr();
        //    RESULT result = FMOD_Studio_System_LoadBankCustom(rawPtr, ref info, flags, out newPtr);
        //    if (result != RESULT.OK)
        //    {
        //        return result;
        //    }

        //    bank = new Bank(newPtr);
        //    return result;
        //}

        public void UnloadAll()
        {
            _system.unloadAll();
        }

        public void FlushCommands()
        {
            _system.flushCommands();
        }

        public void StartCommandCapture(string path, CommandCaptureFlags flags = CommandCaptureFlags.Normal)
        {
            _system.startCommandCapture(path, (COMMANDCAPTURE_FLAGS)flags).Check();
        }

        public void StopCommandCapture()
        {
            _system.stopCommandCapture().Check(); ;
        }

        public CommandReplay LoadCommandReplay(string path, CommandReplayFlags flags = CommandReplayFlags.Normal)
        {
            FMOD.Studio.CommandReplay commandReplay;
            _system.loadCommandReplay(path, (COMMANDREPLAY_FLAGS)flags, out commandReplay).Check();

            return CommandReplay.FromFmod(commandReplay);
        }

        public int BankCount
        {
            get
            {
                int count;
                _system.getBankCount(out count).Check();
                return count;
            }
        }

        public IEnumerable<Bank> Banks
        {
            get
            {
                FMOD.Studio.Bank[] banks;
                _system.getBankList(out banks).Check();

                return banks.Select(Bank.FromFmod);
            }
        }

        public CpuUsage GetCPUUsage()
        {
            CPU_USAGE usage;
            _system.getCPUUsage(out usage).Check();
            return new CpuUsage(usage);
        }

        public BufferUsage GetBufferUsage()
        {
            BUFFER_USAGE buffer;
            _system.getBufferUsage(out buffer).Check();
            return new BufferUsage(buffer);
        }

        public void ResetBufferUsage()
        {
            _system.resetBufferUsage();
        }

        public void SetCallback(Action<System, SystemCallbackType, IntPtr> callback, SystemCallbackType callbackMask)
        {
            _system.setCallback((_, type, param, __) =>
            {
                callback(this, (SystemCallbackType)type, param);
                return RESULT.OK;
            }, (SYSTEM_CALLBACK_TYPE)callbackMask).Check();
        }

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
    }
}
