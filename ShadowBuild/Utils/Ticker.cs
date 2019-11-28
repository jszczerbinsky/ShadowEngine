using ShadowBuild.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShadowBuild.Utils
{
    public class Ticker
    {
        public long currentFPS { get; private set; } = 0;
        private Thread thread;
        public delegate void OnTickDelegateVoid();
        public OnTickDelegateVoid onTick;

        public Ticker()
        {
            thread = new Thread(async () => {
                while (true)
                {
                    DateTime timeOnStart = DateTime.Now;
                    await Task.Run(new Action(()=> { onTick(); }));
                    DateTime timeOnEnd = DateTime.Now;

                    TimeSpan delay = timeOnEnd - timeOnStart;
                    currentFPS = (int)(1000 / delay.TotalMilliseconds);
                }
            });
            thread.Start();

        }
        public void abort()
        {
            thread.Abort();
        }
    }
}
