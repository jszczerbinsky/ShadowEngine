using System;

namespace ShadowEngine.Exceptions
{
    public class LayerException : Exception
    {
        public LayerException() : base() { }
        public LayerException(string mess) : base(mess) { }
        public LayerException(string mess, Exception inner) : base(mess, inner) { }
    }
}
