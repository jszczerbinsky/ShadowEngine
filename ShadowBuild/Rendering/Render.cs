using ShadowBuild.Objects;
using ShadowBuild.Objects.Dimensions;
using ShadowBuild.Objects.Texturing;
using ShadowBuild.Window;
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
        public static _2Dsize resolution
        {
            get
            {
                return new _2Dsize(GameWindow.actualGameWindow.Width, GameWindow.actualGameWindow.Height);
            }
            private set { }
        }

        public static bool showObjectBorders = false;

        public static Bitmap fromCamera(Camera cam)
        {
            _2Dsize startPos = cam.position;
            _2Dsize endPos = _2Dsize.add(cam.position, cam.size);

            Bitmap frame = new Bitmap((int)cam.size.X, (int)cam.size.Y);


            using (Graphics g = Graphics.FromImage(frame))
            {
                g.FillRectangle(new SolidBrush(cam.background), 0, 0, (int)cam.size.X, (int)cam.size.Y);
                SortedSet<GameObject> sortedObjects = new SortedSet<GameObject>(GameObject.allGameObjects);

                foreach (GameObject obj in sortedObjects)
                {
                    if (obj.isRendered)
                    {
                        obj.actualTexture.render(g, obj);

                        if (showObjectBorders) Texture.renderObjectBorders(g, obj);
                    }
                }

                if (showObjectBorders)
                    foreach (GameObject obj in sortedObjects)
                        Texture.renderObjectCenters(g, obj);

                return frame;
            }

        }

    }
}
