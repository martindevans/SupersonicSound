using System;
using System.Collections.Generic;
using SupersonicSound.Exceptions;
using SupersonicSound.LowLevel;

namespace ConsoleTest.Examples
{
    public class ChannelCallback : TestBase
    {
        //We're going to play these sounds, one by one, in order
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
        private List<IDisposable> _disposeList = new List<IDisposable>();

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

                //Load up the first sound
                var startSound = LoadNextSound(system);

                // Begin playing the sound, this returns the "channel" which is playing this sound
                // See inside the PlaySound method for how the callback is setup and used
                PlaySound(system, startSound);

                //temp: Force GC collection to test callback handling
                GC.Collect();

                // Wait until any key is pressed
                WaitForKeypress(() =>
                {
                    // Dispose sounds that aren't used anymore
                    _disposeList.ForEach(x => x.Dispose());
                    _disposeList.Clear();

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

        private Sound LoadNextSound(LowLevelSystem system)
        {
            if (++_soundIndex >= _sounds.Length)
                _soundIndex = 0;

            string fileName = GetContentPath(_sounds[_soundIndex]);

            Console.WriteLine("Loading {0}", _sounds[_soundIndex]);

            var sound = system.CreateStream(name: fileName, mode: Mode.Default);

            if (_currentSound.HasValue)
                // Because of some difference between Mono and CLR we can't dispose the sound here
                // while being called from the callback. The sound has finished playing, but for some
                // reason Mono will crash in the callback handler from the native side if we dispose here.
                // Putting it in a list to be disposed later worked good on Mono. CLR didn't have this issue.
                _disposeList.Add(_currentSound.Value);

            _currentSound = sound;

            return sound;
        }

        private Channel PlaySound(LowLevelSystem system, Sound sound)
        {
            Console.WriteLine("Playing");

            //Play the sound, but start it paused
            var channel = system.PlaySound(sound, null, true);

            //Set a callback on the channel, this is called for all events on the channel
            channel.SetCallback((type, data1, data2) =>
            {
                //When we get the "end" event start playing the next sound
                if (type == ChannelControlCallbackType.End)
                {
                    Console.WriteLine("Callback: Finished, playing next sound");
                    PlaySound(system, LoadNextSound(system));
                }
            });

            //Unpause the channel
            channel.Pause = false;

            //Save this channel
            _currentChannel = channel;

            return channel;
        }
    }
}
