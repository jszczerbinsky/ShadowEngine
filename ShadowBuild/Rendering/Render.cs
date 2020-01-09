using ShadowBuild.Objects;
using ShadowBuild.Objects.Texturing;
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

        public static bool showObjectBorders = false;

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
                SortedSet<Layer> sortedLayers = new SortedSet<Layer>(Layer.All);

                foreach (Layer l in sortedLayers)
                {
                    if (!cam.IsRendering(l)) continue;
                    foreach (RenderableObject obj in l.Objects)
                    {
                        if (!obj.Visible) continue;
                        obj.ActualTexture.Render(g, obj, startPos);

                        if (showObjectBorders) Texture.RenderObjectBorders(g, obj, startPos);

                    }
                }

                if (showObjectBorders)
                    foreach (Layer l in sortedLayers)
                        if (cam.IsRendering(l))
                            foreach (RenderableObject obj in l.Objects)
                                if (obj.Visible)
                                    Texture.RenderObjectCenters(g, obj, startPos);

                return frame;
            }

        }

    }
}
