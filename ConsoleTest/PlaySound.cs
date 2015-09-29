using System;
using SupersonicSound.LowLevel;

namespace ConsoleTest
{
    public class PlaySound : TestBase
    {
        public PlaySound(SupersonicSound.Studio.System system, string contentPath)
            : base(system, contentPath)
        {
        }

        public override void Execute()
        {
            using (var sound = system.LowLevelSystem.CreateSound(GetContentPath("test.wav"), Mode.LoopNormal))
            {
                var channel = system.LowLevelSystem.PlaySound(sound, null, false);

                WaitForKeypress(() =>
                {
                    Console.WriteLine("Position {0} ms", channel.GetPosition(TimeUnit.Milliseconds));
                });

                channel.Stop();
            }
        }
    }
}
