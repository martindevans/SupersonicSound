using System;
using SupersonicSound.LowLevel;
using SupersonicSound.Studio;

namespace ConsoleTest.Examples
{
    /// <summary>
    /// Play multiple banks. Note that this is too much for a Raspberry Pi 1 to handle
    /// </summary>
    public class PlayBank : TestBase
    {
        public PlayBank(string contentPath)
            : base(contentPath)
        {
        }

        public override void Execute()
        {
            // Create a studio system, a high level system which can load FMOD studio projects
            // the preInit argument allows configuration of the system before it is initialized (see the bottom of this file)
            using (SupersonicSound.Studio.System system = new SupersonicSound.Studio.System(preInit: PreInit))
            {
                // Set up the demo to call update on the system
                Pump(system);

                // Load the banks into the system, you must do this before the system can play any sounds from the banks
                // - The "Master Bank" is required to play *any sounds* from a project, every project has a single one
                // - The "Master Bank.strings" contains all the string data associated with a bank, such as path names, every project has a single one
                //   - The string bank is required if you wish to address sounds by string (rather than by GUID)
                // - The "Surround_Ambience" bank contains the sounds from our project which we want to play (this is taken from the FMOD demo project)
                var master = system.LoadBankFromFile(GetContentPath("Master Bank.bank"), BankLoadingFlags.Normal);
                var strings = system.LoadBankFromFile(GetContentPath("Master Bank.strings.bank"), BankLoadingFlags.Normal);
                var bank = system.LoadBankFromFile(GetContentPath("Surround_Ambience.bank"), BankLoadingFlags.Normal);

                // Get a handle for the sound we want to play (by path name)
                // This contains metadata about the sound
                var loopingAmbienceDescription = system.GetEvent("event:/Ambience/Country");

                // Create an actual instance of this sound
                var loopingAmbienceInstance = loopingAmbienceDescription.CreateInstance();

                // Get a parameter from this instance for use later
                var timeParam = loopingAmbienceInstance.Parameters["Time"];

                // Start playing the sound, this sound loops so it will continue until stopped
                loopingAmbienceInstance.Start();

                // Wait until the user presses a key, perform the action every 50ms
                WaitForKeypress(() => {

                    // >> This happens every 50ms <<

                    // Vary the "time" parameter by simply setting the "Value" property on the parameter we got earlier
                    float time = (float)(DateTime.Now.TimeOfDay.TotalSeconds * 0.025f) % 1;
                    timeParam.Value = time;
                    Console.WriteLine("Time {0}", time);
                });

                // Tell the sound to stop playing, but allow it time to fade out naturally
                loopingAmbienceInstance.Stop(true);

                // Wait until the sound has stopped altogether
                WaitUntil(
                    () => loopingAmbienceInstance.PlaybackState == PlaybackState.Stopped,
                    () => Console.WriteLine("Playback state: {0}", loopingAmbienceInstance.PlaybackState)
                );

                // Release the handle, indicating to the FMOD system that it may reclaim the memory used by this
                // Releasing the handle of a playing sound is absolutely fine! All handles should be released *as soon as possible*
                loopingAmbienceInstance.Release();

                // Unload the banks now that they're not needed
                bank.Unload();
                strings.Unload();
                master.Unload();
            }
            // End of the using block, the sound system will be disposed and cleaned up
        }

        private static void PreInit(IPreInitilizeLowLevelSystem ll)
        {
            // This is called *before* the system is initialized (see the first line of the execute method)
            // You can set properties on the ll parameter to configure the system

            // Configure a platform specific output mode
            if (!SupersonicSound.Wrapper.Util.IsUnix)
                ll.Output = OutputMode.DirectSound;
        }
    }
}
