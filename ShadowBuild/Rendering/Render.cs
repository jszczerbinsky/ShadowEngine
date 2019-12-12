using ShadowBuild.Objects;
using ShadowBuild.Objects.Dimensions;
using ShadowBuild.Objects.Texturing;
using System.Collections.Generic;
using System.Drawing;

namespace ShadowBuild.Rendering
{
    public static class Render
    {
        public static readonly _2Dsize Resolution = new _2Dsize(GameWindow.actualGameWindow.Width, GameWindow.actualGameWindow.Height);

        public static bool showObjectBorders = false;

        public static Bitmap FromCamera(Camera cam)
        {
            _2Dsize startPos = cam.Position;
            _2Dsize endPos = _2Dsize.Add(cam.Position, cam.Size);

            Bitmap frame = new Bitmap((int)cam.Size.X, (int)cam.Size.Y);


            using (Graphics g = Graphics.FromImage(frame))
            {
                g.FillRectangle(new SolidBrush(cam.Background), 0, 0, (int)cam.Size.X, (int)cam.Size.Y);
                SortedSet<Layer> sortedLayers = new SortedSet<Layer>(Layer.All);

                foreach (Layer l in sortedLayers)
                {
                    if (!l.Visible) continue;
                    foreach (GameObject obj in l.GameObjects)
                    {
                        if (!obj.Visible) continue;
                        obj.ActualTexture.Render(g, obj);

                        if (showObjectBorders) Texture.RenderObjectBorders(g, obj);

                    }
                }

                if (showObjectBorders)
                    foreach (Layer l in sortedLayers)
                        if (l.Visible)
                            foreach (GameObject obj in l.GameObjects)
                                if (obj.Visible)
                                    Texture.RenderObjectCenters(g, obj);

                return frame;
            }

        }

    }
}
