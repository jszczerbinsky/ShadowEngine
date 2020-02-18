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
                    Rectangle r = new Rectangle((int)(Resolution.Width - 100), 20, 80, 20);
                    g.FillRectangle(new SolidBrush(Color.Black), r);
                    g.DrawString(
                        Loop.currentFPS.ToString("D3") + " FPS",
                        new Font(FontFamily.GenericMonospace, 12),
                        new SolidBrush(Color.White),
                        r
                    );
                }

                SortedSet<Layer> sortedLayers = new SortedSet<Layer>(Layer.All);

                foreach (Layer l in sortedLayers)
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
