using System;

namespace ShadowBuild
{
    /// <summary>
    /// With this class you can write logs.
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Write a log in a console.
        /// </summary>
        /// <param name="text">log content</param>
        public static void Say(string text)
        {
            Console.Write("[");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("ShadowEngine");
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
