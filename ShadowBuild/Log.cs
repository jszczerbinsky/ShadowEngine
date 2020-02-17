using ShadowBuild.Objects;
using ShadowBuild.Rendering;
using System;
using System.Collections.Generic;

namespace ShadowBuild
{
    public static class Log
    {
        public static void Say(string text)
        {
            Console.WriteLine(text);
        }
        public static void Space()
        {
            Console.WriteLine();
        }
    }
}
