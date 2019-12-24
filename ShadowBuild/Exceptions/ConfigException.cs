using System;

namespace ShadowBuild.Exceptions
{
    public class ConfigException : Exception
    {
        public ConfigException() : base() { }
        public ConfigException(string mess) : base(mess) { }
        public ConfigException(string mess, Exception inner) : base(mess, inner) { }
    }
}
