using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadpoolPrinting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("***** Fun with the CLR Thread Pool *****\n");
            Console.WriteLine("Main thread started. ThreadID = {0}", Thread.CurrentThread.ManagedThreadId);

            var p = new Printer();

            var workItem = new WaitCallback(PrintTheNumbers);

            // Queue the method 10 times.
            for (var i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(workItem, p);
            }

            Console.WriteLine("All Tasks Queued");
            Console.ReadLine();
        }

        static void PrintTheNumbers(object state)
        {
            var task = (Printer) state;
            task.PrintNumbers();
        }
    }
}