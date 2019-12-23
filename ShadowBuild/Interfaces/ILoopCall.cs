using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Interfaces
{
    public interface ILoopCall
    {
        void OnStart();
        void OnTick();
    }
}
