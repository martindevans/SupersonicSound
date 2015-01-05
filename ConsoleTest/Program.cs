using System.Threading;
using SupersonicSound.LowLevel;
using SupersonicSound.Studio;
using SupersonicSound.Wrapper;
using System;

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

        public Program()
        {
            using (SupersonicSound.Studio.System system = new SupersonicSound.Studio.System())
            {
                var bank = system.LoadBankFromFile("Music.bank", BankLoadingFlags.Normal);
                var evt = system.GetEvent(Guid.Parse("74bbba7c-76e8-488c-9cf9-67f000df1ffd"));
                var inst = evt.CreateInstance();

                inst.SetParameterValue("Progression", 0.53f);
                inst.SetParameterValue("Pickup", 1f);

                inst.Start();
                inst.Release();

                //var dsp = system.LowLevelSystem.CreateDSP(DspType.Oscillator);

                //Channel channel = system.LowLevelSystem.PlayDSP(dsp, system.LowLevelSystem.CreateChannelGroup("foo"), false);

                while (true)
                {
                    if (Console.KeyAvailable)
                        break;

                    Thread.Sleep(1);
                    system.Update();
                }
            }
        }
    }
}
