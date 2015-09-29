using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ConsoleTest
{
    public abstract class TestBase
    {
        protected SupersonicSound.Studio.System system;
        protected string contentPath;

        public TestBase(SupersonicSound.Studio.System system, string contentPath)
        {
            this.system = system;
            this.contentPath = contentPath;
        }

        protected string GetContentPath(string fileName)
        {
            return Path.Combine(this.contentPath, fileName);
        }

        public abstract void Execute();

        protected void WaitForKeypress(Action reportAction = null)
        {
            var watch = Stopwatch.StartNew();

            if (reportAction != null)
                reportAction();

            while (!Console.KeyAvailable)
            {
                Thread.Sleep(50);
                system.Update();

                if (watch.ElapsedMilliseconds >= 500)
                {
                    watch.Restart();

                    if (reportAction != null)
                        reportAction();
                }
            }

            // Flush
            while (Console.KeyAvailable)
                Console.ReadKey();

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
                system.Update();

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
