using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Exceptions
{
    public class ConfigException : Exception
    {
        public ConfigException() : base() { }
        public ConfigException(string mess) : base(mess) { }
        public ConfigException(string mess, Exception inner) : base(mess, inner) { }
    }
}
