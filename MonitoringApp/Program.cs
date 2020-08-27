using System;
using System.Linq;
using System.Text;
using System.Threading;
using MonitoringLib;

namespace MonitoringApp
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Console.WriteLine("Processing. Please wait...");
            Recorder.Start();

            // simulate a process that requires some memory resources...
            var largeArrayOfInts = Enumerable.Range(1, 10_000).ToArray();
            
            // ...and takes some time to complete
            Thread.Sleep(new Random().Next(5, 10) * 1000);
            
            Recorder.Stop();*/

            var numbers = Enumerable.Range(1, 50_000).ToArray();

            Recorder.Start();
            Console.WriteLine("Using string with +");
            var s = "";
            for (var i = 0; i < numbers.Length; i++)
            {
                s += numbers[i] + ", ";
            }

            Recorder.Stop();

            Recorder.Start();
            Console.WriteLine("Using string with + (foreach)");
            var a = "";
            foreach (var t in numbers)
            {
                a += t + ", ";
            }

            Recorder.Stop();

            Recorder.Start();
            Console.WriteLine("Using StringBuilder");
            var builder = new StringBuilder();
            for (var i = 0; i < numbers.Length; i++)
            {
                builder.Append(numbers[i]);
                builder.Append(", ");
            }

            Recorder.Stop();

            Recorder.Start();
            Console.WriteLine("Using StringBuilder (foreach)");
            var builder2 = new StringBuilder();
            foreach (var number in numbers)
            {
                builder2.Append(number);
                builder2.Append(", ");
            }

            Recorder.Stop();
        }
    }
}