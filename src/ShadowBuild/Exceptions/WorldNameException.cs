using System;
namespace ShadowBuild.Exceptions
{
    public class WorldNameException : Exception
    {
        public WorldNameException() : base() { }
        public WorldNameException(string mess) : base(mess) { }
        public WorldNameException(string mess, Exception inner) : base(mess, inner) { }
    }
}
