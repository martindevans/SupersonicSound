using System;
using SupersonicSound.LowLevel;

namespace ConsoleTest
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string contentPath = "Content";

            Console.WriteLine("Initializing FMOD");

            using (SupersonicSound.Studio.System system = new SupersonicSound.Studio.System(preInit: PreInit))
            {
                Console.WriteLine("FMOD initialized");

                while (true)
                {
                    // Flush
                    while (Console.KeyAvailable)
                        Console.ReadKey();

                    Console.WriteLine("--== Test Menu ==--");
                    Console.WriteLine("1. Play Sound");
                    Console.WriteLine("2. Play Bank");

                    Console.WriteLine("Q. Quit");

                    string choice = Console.ReadLine().Trim().ToLower();
                    if (choice == "q")
                        break;

                    switch (choice)
                    {
                        case "1":
                            new PlaySound(system, contentPath).Execute();
                            break;

                        case "2":
                            new PlayBank(system, contentPath).Execute();
                            break;

                        default:
                            Console.WriteLine("Unknown choice");
                            break;
                    }

                    Console.WriteLine();
                }

                Console.WriteLine("Shutting down");
            }

            Console.WriteLine("Closed");
        }

        private static void PreInit(IPreInitilizeLowLevelSystem ll)
        {
            if (!SupersonicSound.Wrapper.Util.IsUnix)
                ll.Output = OutputMode.DirectSound;
        }
    }
}
