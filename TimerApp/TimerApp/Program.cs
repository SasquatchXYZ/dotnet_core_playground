using System;
using System.Threading;

namespace TimerApp
{
    class Program
    {
        static void PrintTime(object state)
        {
            // Console.WriteLine("Time is: {0}", DateTime.Now.ToLongTimeString());
            Console.WriteLine("Time is: {0}, Today is {1}", DateTime.Now.ToLongTimeString(), state.ToString());
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("***** Working with Timer type *****\n");
            Console.ForegroundColor = ConsoleColor.Magenta;

            // Create the delegate for the timer type.
            var timerCB = new TimerCallback(PrintTime);

            // Establish Timer Settings.
            /*var t = new Timer(
                timerCB, // The TimerCallback delegate object
                null, // Any info to pass into the called method (null for no info)
                0, // Amount of time to wait before starting (in milliseconds)
                1000); // Interval of time in between calls (in milliseconds)*/
            var _ = new Timer(
                timerCB,
                "Bullet Journal Day 1",
                0,
                1000);

            Console.WriteLine("Hit Enter key to terminate...");
            Console.ReadLine();
        }
    }
}