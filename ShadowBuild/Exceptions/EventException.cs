using System;

namespace ShadowBuild.Exceptions
{
    public class EventException : Exception
    {
        public EventException() : base() { }
        public EventException(string mess) : base(mess) { }
        public EventException(string mess, Exception inner) { }
    }
}
