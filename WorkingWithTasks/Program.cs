using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace WorkingWithTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = Stopwatch.StartNew();

            // Console.WriteLine("Running methods synchronously on one thread.");
            //
            // MethodA();
            // MethodB();
            // MethodC();

            /*Console.WriteLine("Running methods asynchronously on multiple threads.");
            var taskA = new Task(MethodA);
            taskA.Start();
            var taskB = Task.Factory.StartNew(MethodB);
            var taskC = Task.Run(new Action(MethodC));

            Task[] tasks = {taskA, taskB, taskC};
            Task.WaitAll(tasks);*/

            Console.WriteLine("Passing the result of one task as an input into another.");
            var taskCallWebServiceAndThenStoredProcedure =
                Task.Factory.StartNew(CallWebService).ContinueWith(previousTask => CallStoredProcedure(previousTask.Result));

            Console.WriteLine($"Result: {taskCallWebServiceAndThenStoredProcedure.Result}");

            Console.WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");
        }

        static void MethodA()
        {
            Console.WriteLine("Starting Method A...");
            Thread.Sleep(3_000); // simulate 3 seconds of work
            Console.WriteLine("Finished Method A.");
        }

        static void MethodB()
        {
            Console.WriteLine("Starting Method B...");
            Thread.Sleep(2_000); // simulate 2 seconds of work
            Console.WriteLine("Finished Method B.");
        }

        static void MethodC()
        {
            Console.WriteLine("Starting Method C...");
            Thread.Sleep(1_000); // simulate 3 seconds of work
            Console.WriteLine("Finished Method C.");
        }

        static decimal CallWebService()
        {
            Console.WriteLine("Starting call to web service...");
            Thread.Sleep(new Random().Next(2_000, 4_000));
            Console.WriteLine("Finished call to web service.");
            return 89.99M;
        }

        static string CallStoredProcedure(decimal amount)
        {
            Console.WriteLine("Starting call to stored procedure...");
            Thread.Sleep(new Random().Next(2_000, 4_000));
            Console.WriteLine("Finished call to stored procedure.");
            return $"12 products cost more than {amount:C}";
        }
    }
}