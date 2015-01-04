using SupersonicSound.LowLevel;
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
                var dsp = system.LowLevelSystem.CreateDSP(DspType.Oscillator);

                Channel channel = system.LowLevelSystem.PlayDSP(dsp, system.LowLevelSystem.CreateChannelGroup("foo"), false);

                Console.ReadKey();
            }
        }
    }
}
