using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace NestedAndChildTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var outer = Task.Factory.StartNew(OuterMethod);
            outer.Wait();
            WriteLine("Console app is stopping.");
        }

        static void OuterMethod()
        {
            WriteLine("Outer Method starting...");
            var inner = Task.Factory.StartNew(InnerMethod, TaskCreationOptions.AttachedToParent);
            // inner.Wait();
            WriteLine("Outer Method finished.");
        }

        static void InnerMethod()
        {
            WriteLine("Inner Method starting...");
            Thread.Sleep(2_000);
            WriteLine("Inner Method finished.");
        }
    }
}