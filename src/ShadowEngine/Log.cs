using ShadowBuild;
using System;
using System.Windows.Forms;

namespace ShadowEngine
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
        public static void Message(string text)
        {
            Message(text, LogType.Normal);
        }

        public static void Message(string text, LogType type)
        {
            Console.Write("[");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            DateTime now = DateTime.Now;
            Console.Write(now.Hour + ":" + now.Minute + ":" + now.Second);

            
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("] ");

            switch (type)
            {
                case LogType.Normal:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogType.Info:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }

            Console.Write(text + "\n");

            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Exception(Exception e)
        {
            Message(
                e.GetType().Name + "\n" +
                "Inner exception: " + e.InnerException + "\n" +
                "Message: " + e.Message + "\n" +
                "In: " + e.StackTrace,
                LogType.Error
                );
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
