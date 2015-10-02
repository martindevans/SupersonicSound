using System;
using ConsoleTest.Examples;

namespace ConsoleTest
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string contentPath = "Content";

            while (true)
            {
                // Flush
                while (Console.KeyAvailable)
                    Console.ReadKey();

                Console.WriteLine("--== Test Menu ==--");
                Console.WriteLine("1. Play Sound");
                Console.WriteLine("2. Play Bank");
                Console.WriteLine("3. Play Sound with channel event/callback");

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

                    case "3":
                        new ChannelCallback(contentPath).Execute();
                        break;

                    default:
                        Console.WriteLine("Unknown choice");
                        break;
                }

                Console.WriteLine();
            }

            Console.WriteLine("Shutting down");
        }
    }
}
