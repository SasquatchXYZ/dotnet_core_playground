using System;
using System.Threading;

namespace MultiThreadedPrinting
{
    public class Printer
    {
        private readonly object _threadLock = new object();

        public void PrintNumbers()
        {
            /*// Display Thread Info.
            Console.WriteLine("-> {0} is executing PrintNumbers()", Thread.CurrentThread.Name);
            
            Console.Write("Your Numbers: ");
            for (var i = 0; i < 10; i++)
            {
                // Put thread to sleep for a random amount of time.
                var r = new Random();
                Thread.Sleep(1000 * r.Next(5));
                Console.Write("{0}, ", i);
            }

            Console.WriteLine();*/

            // Use the private object lock token.
            lock (_threadLock)
            {
                // Display Thread Info.
                Console.WriteLine("-> {0} is executing PrintNumbers()", Thread.CurrentThread.Name);

                Console.Write("Your Numbers: ");
                for (var i = 0; i < 10; i++)
                {
                    // Put thread to sleep for a random amount of time.
                    var r = new Random();
                    Thread.Sleep(1000 * r.Next(5));
                    Console.Write("{0}, ", i);
                }

                Console.WriteLine();
            }
        }
    }
}