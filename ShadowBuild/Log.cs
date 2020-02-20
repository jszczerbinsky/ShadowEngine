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
            Console.Write("[");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("ShadowBuild");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("] " +text+"\n");
        }
        public static void Space()
        {
            Console.WriteLine();
        }
    }
}
