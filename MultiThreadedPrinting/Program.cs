using System;
using System.Threading;

namespace MultiThreadedPrinting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("***** Synchronizing Threads *****\n");

            var p = new Printer();

            // Make 10 threads that are all pointing to the same method on the same object.
            var threads = new Thread[10];
            for (var i = 0; i < 10; i++)
            {
                threads[i] = new Thread(new ThreadStart(p.PrintNumbers))
                {
                    Name = $"Worker Thread #{i}"
                };
            }

            // Now start each one
            foreach (var t in threads)
            {
                t.Start();
            }

            Console.ReadLine();
        }
    }
}