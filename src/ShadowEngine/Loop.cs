using ShadowEngine.Objects;
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

        public delegate void OnObjectIterationDelegateVoid(RenderableObject obj);

        public static OnTickDelegateVoid OnObjectIterationBegin;
        public static OnObjectIterationDelegateVoid OnObjectIteration;

        /// <value>Gets current frames per second value.</value>
        public static long currentFPS { get; private set; } = 0;

        public static float MaxFPS = 60;

        /// <value>Gets time between last 2 timer ticks.</value>
        public static float Delay = 0;

        private static TimeSpan tsdelay = TimeSpan.Zero;

        internal static void StartTicker()
        {
            thread = new Thread(() =>
            {
                thread.Name = "Game loop";

                double waitForCount = 0;
                while (true)
                {
                    DateTime timeOnStart = DateTime.Now;
                    Thread.Sleep(10);
                    Delay = (float)tsdelay.TotalSeconds;

                    if (Delay > 1.0 / MaxFPS || Delay == 0)
                    {
                        OnTick.Invoke();
                        RenderableObject.UpdateAllObjects();
                    }

                    waitForCount += Delay;
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
