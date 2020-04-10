using System;
namespace ShadowBuild.Exceptions
{
    public class SoundException : Exception
    {
            public SoundException() : base() { }
            public SoundException(string mess) : base(mess) { }
            public SoundException(string mess, Exception inner) : base(mess, inner) { }
    }
}
