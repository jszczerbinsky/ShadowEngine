using System;

namespace ShadowBuild
{
    /// <summary>
    /// Game log class.
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Write a log in console.
        /// </summary>
        /// <param name="text">log text</param>
        public static void Say(string text)
        {
            Console.Write("[");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("ShadowBuild");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("] " + text + "\n");
        }

        /// <summary>
        /// Write a blank line in console.
        /// </summary>
        public static void Space()
        {
            Console.WriteLine();
        }
    }
}
