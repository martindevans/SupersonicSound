using System;
using SupersonicSound.Studio;

namespace ConsoleTest
{
    /// <summary>
    /// Play multiple banks. Note that this is too much for a Raspberry Pi 1 to handle
    /// </summary>
    public class PlayBank : TestBase
    {
        public PlayBank(SupersonicSound.Studio.System system, string contentPath)
            : base(system, contentPath)
        {
        }

        public override void Execute()
        {
            // Note that loading the banks won't unload when they are out of scope, you need to call unload, and they are loaded in the system
            var master = system.LoadBankFromFile(GetContentPath("Master Bank.bank"), BankLoadingFlags.Normal);
            var strings = system.LoadBankFromFile(GetContentPath("Master Bank.strings.bank"), BankLoadingFlags.Normal);
            var bank = system.LoadBankFromFile(GetContentPath("Surround_Ambience.bank"), BankLoadingFlags.Normal);

            var loopingAmbienceDescription = system.GetEvent("event:/Ambience/Country");
            var loopingAmbienceInstance = loopingAmbienceDescription.CreateInstance();
            var timeParam = loopingAmbienceInstance.Parameters["Time"];

            loopingAmbienceInstance.Start();

            WaitForKeypress(() =>
                {
                    float time = (float)(DateTime.Now.TimeOfDay.TotalSeconds * 0.025f) % 1;
                    Console.WriteLine("Time {0}", time);
                    timeParam.Value = time;
                });

            Console.WriteLine("Playback state: {0}", loopingAmbienceInstance.PlaybackState);

            loopingAmbienceInstance.Stop(true);

            WaitUntil(
                () => loopingAmbienceInstance.PlaybackState == PlaybackState.Stopped,
                () =>
                {
                    Console.WriteLine("Playback state: {0}", loopingAmbienceInstance.PlaybackState);
                });

            //TODO: Not sure what we have to release is
            loopingAmbienceInstance.Release();
            loopingAmbienceDescription.ReleaseAllInstances();
            bank.Unload();
            strings.Unload();
            master.Unload();
        }
    }
}
