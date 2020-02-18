using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShadowBuild
{
    public static class Loop
    {
        private static Thread thread;
        public delegate void OnTickDelegateVoid();
        public static OnTickDelegateVoid OnTick;
        public static long currentFPS { get; private set; } = 0;
        public static double delay = 0;


        internal static void StartTicker()
        {
            thread = new Thread(() =>
            {
                double waitForCount = 0;
                while (true)
                {
                    
                    DateTime timeOnStart = DateTime.Now;
                    OnTick();
                    DateTime timeOnEnd = DateTime.Now;

                    TimeSpan tsdelay = timeOnEnd - timeOnStart;
                    delay = tsdelay.TotalSeconds;
                    waitForCount += delay;
                    if (waitForCount >= 0.5)
                    {
                        currentFPS = (int)(1000 / tsdelay.TotalMilliseconds);
                        waitForCount = 0;
                    }
                }
            });
            thread.Start();

        }
        internal static void AbortThread()
        {
            thread.Abort();
        }
    }
}
