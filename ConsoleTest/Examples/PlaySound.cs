using System;
using SupersonicSound.LowLevel;

namespace ConsoleTest.Examples
{
    public class PlaySound : TestBase
    {
        public PlaySound(string contentPath)
            : base(contentPath)
        {
        }

        public override void Execute()
        {
            // Create a LowLevelSystem - this is the most basic system and has no support for FMOD studio projects
            using (var system = new LowLevelSystem())
            {
                // Set up the demo to call update on the system
                Pump(system);

                // Create a new sound object
                using (var sound = system.CreateSound(name: GetContentPath("test.wav"), mode: Mode.LoopNormal))
                {
                    // Begin playing the sound, this returns the "channel" which is playing this sound
                    var channel = system.PlaySound(sound, null, false);

                    // Wait until any key is pressed
                    WaitForKeypress(() => Console.WriteLine("Position {0} ms", channel.GetPosition(TimeUnit.Milliseconds)));

                    // Stop the sound playing
                    channel.Stop();

                    Console.WriteLine(channel.IsPlaying);
                }
            }
        }
    }
}
