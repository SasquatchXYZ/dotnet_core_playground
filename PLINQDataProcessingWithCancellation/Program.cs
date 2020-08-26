using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PLINQDataProcessingWithCancellation
{
    class Program
    {
        private static readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Select 1 to Process, 2 to Process in Parallel.");
            var selection = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Green;


            switch (selection)
            {
                case "1":
                    Console.WriteLine($"Entered {selection} Processing...");
                    Task.Factory.StartNew(() => ProcessIntData());
                    break;
                case "2":
                    Console.WriteLine($"Entered {selection} Processing in Parallel...");
                    Task.Factory.StartNew(() => ProcessIntDataInParallel());
                    Console.Write("Enter Q to quit: ");
                    var answer = Console.ReadLine();
                    if (answer.Equals("Q", StringComparison.OrdinalIgnoreCase))
                    {
                        _cancellationTokenSource.Cancel();
                    }

                    break;
                default:
                    Console.WriteLine($"Entered {selection} Processing...");
                    Task.Factory.StartNew(() => ProcessIntData());
                    break;
            }

            Console.ReadLine();
        }

        static void ProcessIntData()
        {
            // Get a very large array of integers.
            var source = Enumerable.Range(1, 10_000_000).ToArray();
            // Find the numbers where num % 3 == 0 is true, returned in descending order.
            var modThreeIsZero = (
                from num in source
                where num % 3 == 0
                orderby num descending
                select num).ToArray();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Found {modThreeIsZero.Length} numbers that match query!");
        }

        static void ProcessIntDataInParallel()
        {
            // Get a very large array of integers.
            var source = Enumerable.Range(1, 10_000_000).ToArray();
            // Find the numbers where num % 3 == 0 is true, returned in descending order.
            var modThreeIsZero = (
                from num in source.AsParallel().WithCancellation(_cancellationTokenSource.Token)
                where num % 3 == 0
                orderby num descending
                select num).ToArray();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Found {modThreeIsZero.Length} numbers that match query!");
        }
    }
}