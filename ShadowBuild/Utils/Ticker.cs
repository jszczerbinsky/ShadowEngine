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
            thread = new Thread(() => {
                while (true)
                {
                    onTick();
                    Thread.Sleep(1000 / frequency);
                }
            });

        }
        public void stop()
        {
            thread.Abort();
        }
    }
}
