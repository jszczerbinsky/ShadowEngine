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
        public static int maxFPS { get; private set; }

        public static bool lastFrameRendered = true;

        internal static void initialize()
        {
            Log.say("Initializing resolution settings");
            resolution = new Resolution();
            resolution.X = 800;
            resolution.Y = 600;
            resolution.windowType = WindowType.WINDOW;

            maxFPS = 60;
        }

        internal static void renderNewFrame()
        {
            lastFrameRendered = false;

            Bitmap frame = new Bitmap(resolution.X, resolution.Y);

            gameWindow.Invoke(new Action(() =>
            {
                gameWindow.Size = new Size(resolution.X, resolution.Y);
                gameWindow.display.Size = new Size(resolution.X, resolution.Y);
            }));

            using (Graphics g = Graphics.FromImage(frame))
            {
                //Render objects
                foreach (GameObject obj in GameObject.allGameObjects)
                {
                    if (obj.isRendered)
                    {
                        if (obj.actualTexture is RegularTexture)
                        {
                            RegularTexture tex = (RegularTexture)obj.actualTexture;

                            g.DrawImage(
                                tex.image,
                                new Rectangle(
                                    new Point(
                                        obj.globalPosition.X - tex.image.Width * obj.size.X / 2,
                                        obj.globalPosition.Y - tex.image.Height * obj.size.Y / 2
                                    ), new Size(
                                        tex.image.Width * obj.size.X,
                                        tex.image.Height * obj.size.Y
                                        )
                                    )
                                );

                        }

                    }
                }
                //Render object centers
                foreach (GameObject obj in GameObject.allGameObjects)
                {
                    if (obj.isRendered)
                    {
                        g.FillEllipse(
                            new SolidBrush(
                                    Color.Red
                            ),
                            new Rectangle(
                                new Point(
                                    obj.globalPosition.X - 1,
                                    obj.globalPosition.Y - 1),
                                new Size(
                                    5, 5)
                                ));

                        RegularTexture tex = (RegularTexture)obj.actualTexture;

                        g.DrawRectangle(
                            new Pen(
                                new SolidBrush(
                                        Color.Green
                                ),
                            2),
                            new Rectangle(
                                new Point(
                                    obj.globalPosition.X - tex.image.Width * obj.size.X / 2,
                                    obj.globalPosition.Y - tex.image.Height * obj.size.Y / 2),
                                new Size(
                                    tex.image.Width, tex.image.Height)
                                ));
                    }
                }
                gameWindow.Invoke(new Action(() =>
                {
                    Image tmp = gameWindow.display.Image;
                    gameWindow.display.Image = frame;
                    if (tmp != null) tmp.Dispose();
                }));
                lastFrameRendered = true;
            }
        }

        public static void setFPSlimit(int fpsLimit)
        {
            maxFPS = fpsLimit;
            Log.say("FPS limit changed to " + fpsLimit + " FPS");
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
