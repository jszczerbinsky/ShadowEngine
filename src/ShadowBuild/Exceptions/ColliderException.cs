using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Exceptions
{
    public class ColliderException : Exception
    {
        public ColliderException() : base() { }
        public ColliderException(string mess) : base(mess) { }
        public ColliderException(string mess, Exception inner) : base(mess, inner) { }
    }
}
