using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Exceptions
{
    class EventNameIsAlreadyUsedException : Exception
    {
        public EventNameIsAlreadyUsedException(string mess) : base(mess)
        {

        }
    }
}
