using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GA.Helpers
{
    class StopwatchProvider
    {
        private static Stopwatch _stopwatch = new Stopwatch();

        public static Stopwatch StopWatch { get { return _stopwatch; } }

        private static void ClearStopWatch()
        {
            StopWatch.Reset();
        }

        public static void StartStopWatch()
        {
            StopWatch.Start();
        }

        public static string StopWatchTime()
        {
            string time =  StopWatch.Elapsed.ToString();
            StopWatch.Reset();
            return time;
        }

    }
}
