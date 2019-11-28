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
        private Thread thread;
        public delegate void OnTickDelegateVoid();
        public OnTickDelegateVoid onTick;

        public Ticker(int frequency)
        {
            if (frequency == 0) throw new TickerFrequencyEqualsZeroException();
            thread = new Thread(() => {
                while (true)
                {
                    if(Render.lastFrameRendered) onTick();
                    Thread.Sleep(1000 / frequency);
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
