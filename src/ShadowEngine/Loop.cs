using System;
using System.Threading;

namespace ShadowEngine
{
    /// <summary>
    /// Game loop class.
    /// </summary>
    public static class Loop
    {
        private static Thread thread;

        /// <value>OnTick delegate.</value>
        public delegate void OnTickDelegateVoid();

        /// <value>Methods called on every timer tick.</value>
        public static OnTickDelegateVoid OnTick;

        /// <value>Gets current frames per second value.</value>
        public static long currentFPS { get; private set; } = 0;

        /// <value>Gets time between last 2 timer ticks.</value>
        public static double delay = 0;

        private static TimeSpan tsdelay = TimeSpan.Zero;

        internal static void StartTicker()
        {
            thread = new Thread(() =>
            {
                double waitForCount = 0;
                while (true)
                {
                    DateTime timeOnStart = DateTime.Now;
                    Thread.Sleep(10);
                    OnTick();
                    
                    delay = tsdelay.TotalSeconds;
                    waitForCount += delay;
                    if (waitForCount >= 0.5)
                    {
                        currentFPS = (int)(1000 / tsdelay.TotalMilliseconds);
                        waitForCount = 0;
                    }
                    DateTime timeOnEnd = DateTime.Now;

                    tsdelay = timeOnEnd - timeOnStart;
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
