using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;

namespace AsyncEnumerable
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ForegroundColor = ConsoleColor.DarkCyan;

            await foreach (int number in GetNumbers())
            {
                WriteLine($"Number: {number}");
            }
        }

        static async IAsyncEnumerable<int> GetNumbers()
        {
            var r = new Random();

            // simulate work
            await Task.Run(() => Task.Delay(r.Next(1_500, 3_000)));
            yield return r.Next(1, 101);

            await Task.Run(() => Task.Delay(r.Next(1_500, 3_000)));
            yield return r.Next(1, 101);

            await Task.Run(() => Task.Delay(r.Next(1_500, 3_000)));
            yield return r.Next(1, 101);
        }
    }
}