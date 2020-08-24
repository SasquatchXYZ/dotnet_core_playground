using System;
using System.Threading;

namespace AddWithThreads
{
    class Program
    {
        private static AutoResetEvent _waitHandle = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine("***** Adding with Thread Objects *****");
            Console.WriteLine("ID of thread in Main(): {0}", Thread.CurrentThread.ManagedThreadId);

            // Make an AddParams object to pass to the secondary thread.
            var ap = new AddParams(10, 10);
            var t = new Thread(Add);
            t.Start(ap);

            /*// Force a wait to let the other thread finish.
            Thread.Sleep(5);*/

            // Wait here until you are notified.
            _waitHandle.WaitOne();
            Console.WriteLine("Other Thread is done!");

            Console.ReadLine();
        }

        static void Add(object data)
        {
            if (data is AddParams)
            {
                Console.WriteLine("ID of thread in Add(): {0}", Thread.CurrentThread.ManagedThreadId);

                var ap = (AddParams) data;
                Console.WriteLine("{0} + {1} is {2}", ap.A, ap.B, ap.A + ap.B);

                // Tell other thread we are done.
                _waitHandle.Set();
            }
        }
    }
}