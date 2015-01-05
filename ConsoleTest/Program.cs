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
                var bank = system.LoadBankFromFile("Vehicles.bank", BankLoadingFlags.Normal);
                var evt = system.GetEvent(Guid.Parse("7aa5e8f1-8ec2-42c6-b465-1241a603a055"));
                var inst = evt.CreateInstance();

                var dsp = system.LowLevelSystem.CreateDSP(DspType.Oscillator);

                Channel channel = system.LowLevelSystem.PlayDSP(dsp, system.LowLevelSystem.CreateChannelGroup("foo"), false);

                Console.ReadKey();
            }
        }
    }
}
