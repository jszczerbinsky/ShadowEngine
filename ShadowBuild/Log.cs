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
        public static void ListLayers()
        {
            SortedSet<Layer> layers = new SortedSet<Layer>(Layer.All);
            foreach (Layer l in layers)
            {
                Console.WriteLine(" * " + l.Name);
                foreach (GameObject obj in l.GameObjects)
                {
                    Console.WriteLine("    - " + obj.Name);
                }
            }
        }
    }
}
