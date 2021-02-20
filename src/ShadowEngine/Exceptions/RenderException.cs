using System;

namespace ShadowEngine.Exceptions
{
    public class RenderException : Exception
    {
        public RenderException() : base() { }
        public RenderException(string mess) : base(mess) { }
        public RenderException(string mess, Exception inner) : base(mess, inner) { }
    }
}
