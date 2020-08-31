using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;

namespace SynchronizingResourceAccess
{
    class Program
    {
        private static readonly Random _r = new Random();
        private static string _message; // a shared resource
        private static int _counter; // another shared resource
        private static readonly object _conch = new object();

        static void Main(string[] args)
        {
            WriteLine("Please wait for the tasks to complete.");
            Stopwatch watch = Stopwatch.StartNew();

            Task a = Task.Factory.StartNew(MethodA);
            Task b = Task.Factory.StartNew(MethodB);

            Task.WaitAll(new Task[] {a, b});

            WriteLine();
            WriteLine($"Results: {_message}.");
            WriteLine($"{watch.ElapsedMilliseconds:#,##0} elapsed milliseconds.");
            WriteLine($"{_counter} string modifications.");
        }

        static void MethodA()
        {
            try
            {
                Monitor.TryEnter(_conch, TimeSpan.FromSeconds(15));

                for (var i = 0; i < 5; i++)
                {
                    Thread.Sleep(_r.Next(2_000));
                    _message += "A";
                    Interlocked.Increment(ref _counter);
                    Write(".");
                }
            }
            finally
            {
                Monitor.Exit(_conch);
            }
        }

        static void MethodB()
        {
            try
            {
                Monitor.TryEnter(_conch, TimeSpan.FromSeconds(15));

                for (var i = 0; i < 5; i++)
                {
                    Thread.Sleep(_r.Next(2_000));
                    _message += "B";
                    Interlocked.Increment(ref _counter);
                    Write(".");
                }
            }
            finally
            {
                Monitor.Exit(_conch);
            }
        }
    }
}