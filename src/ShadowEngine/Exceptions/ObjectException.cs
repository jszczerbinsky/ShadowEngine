using System;

namespace ShadowEngine.Exceptions
{
    public class ObjectException : Exception
    {
        public ObjectException() : base() { }
        public ObjectException(string mess) : base(mess) { }
        public ObjectException(string mess, Exception inner) : base(mess, inner) { }
    }
}
