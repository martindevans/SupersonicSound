using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using SupersonicSound.Exceptions;
using SupersonicSound.LowLevel;

namespace ConsoleTest
{
    public abstract class TestBase
    {
        protected string ContentPath { get; private set; }

        private LowLevelSystem _lowLevelSystem;
        private SupersonicSound.Studio.System _system;

        protected TestBase(string contentPath)
        {
            ContentPath = contentPath;
        }

        protected string GetContentPath(string fileName)
        {
            return Path.Combine(ContentPath, fileName);
        }

        public abstract void Execute();

        protected void Pump(LowLevelSystem system)
        {
            _lowLevelSystem = system;
        }

        protected void Pump(SupersonicSound.Studio.System system)
        {
            _system = system;
        }

        protected void WaitForKeypress(Action reportAction = null)
        {
            var watch = Stopwatch.StartNew();

            if (reportAction != null)
                reportAction();

            while (!Console.KeyAvailable)
            {
                Thread.Sleep(50);

                if (_lowLevelSystem != null)
                    _lowLevelSystem.Update();
                if (_system != null)
                    _system.Update();

                if (watch.ElapsedMilliseconds >= 500)
                {
                    watch.Restart();

                    if (reportAction != null)
                        reportAction();
                }
            }

            // Flush
            while (Console.KeyAvailable)
                Console.ReadKey(true);

            if (reportAction != null)
                reportAction();
        }

        protected void WaitUntil(Func<bool> predicate, Action reportAction = null)
        {
            var watch = Stopwatch.StartNew();

            if (reportAction != null)
                reportAction();

            while (!predicate())
            {
                Thread.Sleep(50);

                if (_lowLevelSystem != null)
                    _lowLevelSystem.Update();
                if (_system != null)
                    _system.Update();

                if (watch.ElapsedMilliseconds >= 500)
                {
                    watch.Restart();

                    if (reportAction != null)
                        reportAction();
                }
            }

            if (reportAction != null)
                reportAction();
        }
    }
}
