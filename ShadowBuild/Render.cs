using ShadowBuild.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild
{
    public static class Render
    {
        internal static GameWindow gameWindow;
        public static Resolution resolution { get; private set; }
        public static int maxFPS { get; private set; }

        internal static void renderNewFrame()
        {

        }

        public static void setFPSlimit(int fpsLimit)
        {
            maxFPS = fpsLimit;
        }

        public static void SetResolution(Resolution newResolution)
        {
            resolution = newResolution;
        }

        public static void SetResolution(int x, int y, WindowType windowType)
        {
            resolution.X = x;
            resolution.Y = y;
            resolution.windowType = windowType;
        }
    }
}
