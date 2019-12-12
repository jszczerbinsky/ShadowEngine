using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShadowBuild
{
    public static class Loop
    {
        private static Thread thread;
        public delegate void OnTickDelegateVoid();
        public static OnTickDelegateVoid onTick;
        public static long currentFPS { get; private set; } = 0;
        public static double delay = 0;


        internal static void StartTicker()
        {
            thread = new Thread(async () =>
            {
                while (true)
                {
                    DateTime timeOnStart = DateTime.Now;
                    await Task.Run(new Action(() => { onTick(); }));
                    DateTime timeOnEnd = DateTime.Now;

                    TimeSpan tsdelay = timeOnEnd - timeOnStart;
                    delay = tsdelay.TotalSeconds;
                    currentFPS = (int)(1000 / tsdelay.TotalMilliseconds);
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
