using System;
using ConsoleTest.Examples;
using SupersonicSound.LowLevel;

namespace ConsoleTest
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string contentPath = "Content";

            Console.WriteLine("Initializing FMOD");

            
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
                            new PlaySound(contentPath).Execute();
                            break;

                        case "2":
                            new PlayBank(contentPath).Execute();
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
    }
}
