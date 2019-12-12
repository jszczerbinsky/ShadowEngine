using System;

namespace ShadowBuild.Exceptions
{
    class EventNameIsAlreadyUsedException : Exception
    {
        public EventNameIsAlreadyUsedException(string mess) : base(mess)
        {

        }
    }
}
