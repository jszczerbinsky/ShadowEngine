using System;

namespace ShadowEngine.Exceptions
{
    public class TextureException : Exception
    {
        public TextureException() : base() { }
        public TextureException(string mess) : base(mess) { }
        public TextureException(string mess, Exception inner) : base(mess, inner) { }
    }
}
