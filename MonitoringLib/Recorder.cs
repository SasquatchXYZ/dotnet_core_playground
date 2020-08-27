using System;
using System.Diagnostics;
using static System.Console;
using static System.Diagnostics.Process;

namespace MonitoringLib
{
    public static class Recorder
    {
        private static readonly Stopwatch _timer = new Stopwatch();
        private static long _bytesPhysicalBefore = 0;
        private static long _bytesVirtualBefore = 0;

        public static void Start()
        {
            // force two garbage collections to release memory that is no longer references but has not been released yet
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // store the current physical and virtual memory use
            _bytesPhysicalBefore = GetCurrentProcess().WorkingSet64;
            _bytesVirtualBefore = GetCurrentProcess().VirtualMemorySize64;
            _timer.Restart();
        }

        public static void Stop()
        {
            _timer.Stop();
            var bytesPhysicalAfter = GetCurrentProcess().WorkingSet64;
            var bytesVirtualAfter = GetCurrentProcess().VirtualMemorySize64;

            WriteLine("{0:N0} physical bytes used.", bytesPhysicalAfter - _bytesPhysicalBefore);
            WriteLine("{0:N0} virtual bytes used.", bytesVirtualAfter - _bytesVirtualBefore);

            WriteLine("{0} time span elapsed.", _timer.Elapsed);

            WriteLine("{0:N0} total milliseconds elapsed.", _timer.ElapsedMilliseconds);
        }
    }
}