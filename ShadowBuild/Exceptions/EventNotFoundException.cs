using System;

namespace ShadowBuild.Exceptions
{
    public class EventNotFoundException : Exception
    {
        public EventNotFoundException(string mess) : base(mess)
        {

        }
    }
}
