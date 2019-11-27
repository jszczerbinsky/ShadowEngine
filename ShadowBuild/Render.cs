using ShadowBuild.Objects;
using ShadowBuild.Window;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        internal static void initialize()
        {
            resolution = new Resolution();
            resolution.X = 600;
            resolution.Y = 800;
            resolution.windowType = WindowType.WINDOW;

            maxFPS = 60;
        }
        internal static void renderNewFrame()
        {
            Bitmap frame = new Bitmap(resolution.X, resolution.Y);
            using (Graphics g = Graphics.FromImage(frame))
            {
                g.FillRectangle(new SolidBrush(Color.Red), 0, 0, resolution.X, resolution.Y);
                foreach(GameObject gobj in GameObject.allGameObjects)
                {
                    if (!gobj.isRendered)
                    {

                    }
                }
            }
            gameWindow.display.Image = frame;
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
