using System;

namespace ShadowEngine.Exceptions
{
    public class ColliderException : Exception
    {
        public ColliderException() : base() { }
        public ColliderException(string mess) : base(mess) { }
        public ColliderException(string mess, Exception inner) : base(mess, inner) { }
    }
}
