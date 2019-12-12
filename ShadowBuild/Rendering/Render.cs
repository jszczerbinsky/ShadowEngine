using ShadowBuild.Objects;
using ShadowBuild.Objects.Dimensions;
using ShadowBuild.Objects.Texturing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

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
                g.FillRectangle(new SolidBrush(cam.background), 0, 0, (int)cam.Size.X, (int)cam.Size.Y);
                SortedSet<GameObject> sortedObjects = new SortedSet<GameObject>(GameObject.AllGameObjects);

                foreach (GameObject obj in sortedObjects)
                {
                    if (obj.isRendered)
                    {
                        obj.ActualTexture.Render(g, obj);

                        if (showObjectBorders) Texture.RenderObjectBorders(g, obj);
                    }
                }

                if (showObjectBorders)
                    foreach (GameObject obj in sortedObjects)
                        Texture.RenderObjectCenters(g, obj);

                return frame;
            }

        }

    }
}
