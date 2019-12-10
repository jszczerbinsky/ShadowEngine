using ShadowBuild.Objects;
using ShadowBuild.Objects.Dimensions;
using ShadowBuild.Objects.Texturing;
using ShadowBuild.Window;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace ShadowBuild
{
    public static class Render
    {
        internal static GameWindow gameWindow;
        public static Resolution resolution { get; private set; }

        public static bool showObjectBorders = false;

        internal static void initialize()
        {
            Log.say("Initializing resolution settings");
            resolution = new Resolution();
            resolution.X = 800;
            resolution.Y = 600;
            resolution.windowType = WindowType.WINDOW;
        }

        internal static void renderNewFrame()
        {

            Bitmap frame = new Bitmap(resolution.X, resolution.Y);

            gameWindow.Invoke(new Action(() =>
            {
                gameWindow.Size = new Size(resolution.X, resolution.Y);
                gameWindow.display.Size = new Size(resolution.X, resolution.Y);
            }));

            using (Graphics g = Graphics.FromImage(frame))
            {

                foreach (GameObject obj in GameObject.allGameObjects)
                {
                    if (obj.isRendered)
                    {
                        obj.actualTexture.render(g, obj);

                        if (showObjectBorders) Texture.renderObjectBorders(g, obj);
                    }
                }

                if (showObjectBorders)
                    foreach (GameObject obj in GameObject.allGameObjects)
                        Texture.renderObjectCenters(g, obj);

                gameWindow.Invoke(new Action(() =>
                {
                    Image tmp = gameWindow.display.Image;
                    gameWindow.display.Image = frame;
                    if (tmp != null) tmp.Dispose();
                }));
            }
        }

        public static void SetResolution(Resolution newResolution)
        {
            resolution = newResolution;
            string fullscreen = "NO";
            if (newResolution.windowType == WindowType.FULLSCREEN) fullscreen = "YES";
            Log.say("Resolution changed to " + newResolution.X + "x" + newResolution.Y + " fullscreen: " + fullscreen);
        }

        public static void SetResolution(int x, int y, WindowType windowType)
        {
            resolution.X = x;
            resolution.Y = y;
            resolution.windowType = windowType;
            string fullscreen = "NO";
            if (windowType == WindowType.FULLSCREEN) fullscreen = "YES";
            Log.say("Resolution changed to " + x + "x" + y + " fullscreen: " + fullscreen);
        }
    }
}
