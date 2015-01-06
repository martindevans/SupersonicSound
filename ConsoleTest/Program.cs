using SupersonicSound.LowLevel;
using SupersonicSound.Studio;
using SupersonicSound.Wrapper;
using System;
using System.Threading;

namespace ConsoleTest
{
    class Program
    {
        #region entry point
        static void Main(string[] args)
        {
            Native.Load();

            Program p = new Program();
        }
        #endregion

        //public Program()
        //{
        //    FMOD.Studio.System system;
        //    FMOD.Studio.System.create(out system).Check();

        //    FMOD.System lowLevelSystem;
        //    system.getLowLevelSystem(out lowLevelSystem).Check();
        //    lowLevelSystem.setSoftwareFormat(0, SPEAKERMODE._5POINT1, 0);

        //    system.initialize(32, INITFLAGS.NORMAL, FMOD.INITFLAGS.NORMAL, IntPtr.Zero);

        //    FMOD.Studio.Bank masterBank;
        //    system.loadBankFile("Master Bank.bank", LOAD_BANK_FLAGS.NORMAL, out masterBank).Check();

        //    FMOD.Studio.Bank stringsBank;
        //    system.loadBankFile("Master Bank.strings.bank", LOAD_BANK_FLAGS.NORMAL, out stringsBank).Check();

        //    FMOD.Studio.Bank ambienceBank;
        //    system.loadBankFile("Surround_Ambience.bank", LOAD_BANK_FLAGS.NORMAL, out ambienceBank).Check();

        //    FMOD.Studio.EventDescription loopingAmbienceDescription;
        //    system.getEvent("event:/Ambience/Country", out loopingAmbienceDescription).Check();

        //    FMOD.Studio.EventInstance loopingAmbienceInstance;
        //    loopingAmbienceDescription.createInstance(out loopingAmbienceInstance).Check();

        //    ParameterInstance timeParam;
        //    loopingAmbienceInstance.getParameter("Time", out timeParam).Check();

        //    loopingAmbienceInstance.start();

        //    while (!Console.KeyAvailable)
        //    {
        //        Thread.Sleep(1);
        //        system.update();

        //        float time = (float)(DateTime.Now.TimeOfDay.TotalSeconds * 0.025f) % 1;
        //        Console.Title = time.ToString();
        //        timeParam.setValue(time).Check();
        //    }
        //}

        public Program()
        {
            using (SupersonicSound.Studio.System system = new SupersonicSound.Studio.System(preInit: a => {
                a.Format = new SoftwareFormat(0, SpeakerMode.FivePointOne, 0);
            }))
            {
                var master = system.LoadBankFromFile("Master Bank.bank", BankLoadingFlags.Normal);
                var strings = system.LoadBankFromFile("Master Bank.strings.bank", BankLoadingFlags.Normal);
                var bank = system.LoadBankFromFile("Surround_Ambience.bank", BankLoadingFlags.Normal);

                var loopingAmbienceDescription = system.GetEvent("event:/Ambience/Country");

                var loopingAmbienceInstance = loopingAmbienceDescription.CreateInstance();

                //loopingAmbienceInstance.SetParameterValue();

                var timeParam = loopingAmbienceInstance.Parameters["Time"];

                loopingAmbienceInstance.Start();

                //var dsp = system.LowLevelSystem.CreateDSP(DspType.Oscillator);
                //Channel channel = system.LowLevelSystem.PlayDSP(dsp, system.LowLevelSystem.CreateChannelGroup("foo"), false);

                while (!Console.KeyAvailable)
                {
                    Thread.Sleep(1);
                    system.Update();

                    float time = (float)(DateTime.Now.TimeOfDay.TotalSeconds * 0.025f) % 1;
                    Console.Title = time.ToString();
                    timeParam.Value = time;
                }
            }
        }
    }
}
