using System;

namespace ShadowBuild.Exceptions
{
    public class AxisException : Exception
    {
        public AxisException() : base() { }
        public AxisException(string mess) : base(mess) { }
        public AxisException(string mess, Exception inner) : base(mess, inner) { }
    }
}
