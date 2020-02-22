using ShadowBuild.Objects;
using System.Collections.Generic;
using System.Drawing;

namespace ShadowBuild.Rendering
{
    public static class Render
    {
        public static System.Windows.Size Resolution
        {
            get
            {
                return new System.Windows.Size(
                    GameWindow.actualGameWindow.Size.Width,
                    GameWindow.actualGameWindow.Size.Height
                    );
            }
        }
        public static bool ShowFPS = false;
        private static SortedSet<Layer> SortedLayers;

        private static readonly Brush FPSForeground = new SolidBrush(Color.White);
        private static readonly Brush FPSBackground = new SolidBrush(Color.Black);
        private static readonly Font FPSFont = new Font(FontFamily.GenericMonospace, 12);
        private static readonly Rectangle FPSRectangle = new Rectangle(20, 20, 80, 20);

        public static void SortLayers()
        {
            SortedLayers = new SortedSet<Layer>(Layer.All);
        }
        public static Bitmap FromCamera(Camera cam)
        {
            System.Windows.Point startPos =
                new System.Windows.Point(
                    cam.Position.X - cam.Size.Width / 2,
                    cam.Position.Y - cam.Size.Height / 2);

            Bitmap frame = new Bitmap((int)cam.Size.Width, (int)cam.Size.Height);


            using (Graphics g = Graphics.FromImage(frame))
            {
                g.FillRectangle(new SolidBrush(cam.Background), 0, 0, (int)cam.Size.Width, (int)cam.Size.Height);

                if (ShowFPS)
                {
                    g.FillRectangle(FPSBackground, FPSRectangle);
                    g.DrawString(
                        Loop.currentFPS.ToString("D3") + " FPS",
                        FPSFont,
                        FPSForeground, 
                        FPSRectangle
                    );
                }

                foreach (Layer l in SortedLayers)
                {
                    if (!cam.IsRendering(l)) continue;
                    foreach (RenderableObject obj in l.Objects)
                    {
                        if (!obj.Visible) continue;
                        obj.Render(g, startPos);

                    }
                }


                return frame;
            }

        }

    }
}
