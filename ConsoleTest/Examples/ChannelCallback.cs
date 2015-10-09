using System;
using SupersonicSound.Exceptions;
using SupersonicSound.LowLevel;

namespace ConsoleTest.Examples
{
    public class ChannelCallback : TestBase
    {
        private readonly string[] _sounds =
        {
            "Front_Center.wav",
            "Front_Left.wav",
            "Front_Right.wav",
            "Rear_Center.wav",
            "Rear_Left.wav",
            "Rear_Right.wav",
            "Side_Left.wav",
            "Side_Right.wav"
        };

        private Sound? _currentSound;
        private Channel? _currentChannel;
        private int _soundIndex;

        public ChannelCallback(string contentPath)
            : base(contentPath)
        {
        }

        private Sound LoadNextSound(LowLevelSystem system)
        {
            if (++_soundIndex >= _sounds.Length)
                _soundIndex = 0;

            string fileName = GetContentPath(_sounds[_soundIndex]);

            Console.WriteLine("Loading {0}", _sounds[_soundIndex]);

            var sound = system.CreateStream(name: fileName, mode: Mode.Default);

            _currentSound?.Dispose();

            _currentSound = sound;

            return sound;
        }

        private Channel PlaySound(LowLevelSystem system, Sound sound)
        {
            Console.WriteLine("Playing");

            var channel = system.PlaySound(sound, null, false);

            channel.SetCallback((type, data1, data2) =>
            {
                if (type == ChannelControlCallbackType.End)
                {
                    Console.WriteLine("Callback: Finished playing sound");

                    var nextSound = LoadNextSound(system);

                    PlaySound(system, nextSound);
                }
            });

            _currentChannel = channel;

            return channel;
        }

        public override void Execute()
        {
            // Create a LowLevelSystem - this is the most basic system and has no support for FMOD studio projects
            using (var system = new LowLevelSystem())
            {
                // Set up the demo to call update on the system
                Pump(system);

                var startSound = LoadNextSound(system);

                // Begin playing the sound, this returns the "channel" which is playing this sound

                PlaySound(system, startSound);

                //temp: Force GC collection to test callback handling
                GC.Collect();

                // Wait until any key is pressed
                WaitForKeypress(() =>
                {

                    //temp: Force GC collection to test callback handling
                    GC.Collect();

                    var pos = _currentChannel?.GetPosition(TimeUnit.Milliseconds);
                    if (pos.HasValue)
                        Console.WriteLine("Position {0}ms", pos.Value);
                });

                // Stop the sound playing
                _currentChannel?.Stop();
            }

            Console.WriteLine("Done");
        }
    }
}
