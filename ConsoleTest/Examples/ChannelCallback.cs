using System;
using SupersonicSound.Exceptions;
using SupersonicSound.LowLevel;

namespace ConsoleTest.Examples
{
    public class ChannelCallback : TestBase
    {
        public ChannelCallback(string contentPath)
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
                using (var sound = system.CreateSound(name: GetContentPath("test.wav"), mode: Mode.Default))
                {
                    // Begin playing the sound, this returns the "channel" which is playing this sound
                    var channel = system.PlaySound(sound, null, false);

                    channel.SetCallback((type, data1, data2) =>
                    {
                        if (type == ChannelControlCallbackType.End)
                        {
                            Console.WriteLine("Callback: Finished playing sound");
                        }
                    });

                    GC.Collect();

                    // Wait until any key is pressed
                    WaitForKeypress(() =>
                    {
                        GC.Collect();
                        try
                        {
                            Console.WriteLine("Position {0} ms", channel.GetPosition(TimeUnit.Milliseconds));
                        }
                        catch (FmodInvalidHandleException)
                        {
                            //We need a better way of handling invalid channels!
                        }
                    });

                    // Stop the sound playing
                    channel.Stop();
                }
            }
        }
    }
}
