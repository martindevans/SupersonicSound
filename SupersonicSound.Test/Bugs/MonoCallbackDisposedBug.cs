using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupersonicSound.LowLevel;

namespace SupersonicSound.Test.Bugs
{
    /// <summary>
    /// Intended to reproduce https://github.com/martindevans/SupersonicSound/issues/23
    /// </summary>
    [TestClass]
    public class MonoCallbackDisposedBug
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var system = new LowLevelSystem(preInit: a => { a.Output = OutputMode.NoSound; }))
            {
                //Load a sound to play
                var sound = system.CreateStream(name: "Content/Front_Center.wav", mode: Mode.Default);

                // Begin playing the sound
                var channel = system.PlaySound(sound, null, false);

                bool flag = true;

                channel.SetCallback((type, data1, data2) =>
                {
                    if (type == ChannelControlCallbackType.End)
                    {
                        //Now that the sound is done, dispose it
                        //With issue #23 this should crash on mono
                        sound.Dispose();

                        flag = false;
                    }
                });

                //Loop until the flag is set to false
                while (flag)
                    system.Update();
            }
        }
    }
}
