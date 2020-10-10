using ShadowBuild.Objects;
using System.Collections.Generic;
using System.Drawing;

namespace ShadowBuild.Rendering
{
    /// <summary>
    /// Render class.
    /// Here you can specify game window resolution and some rendering options.
    /// </summary>
    public static class Render
    {

        /// <value>Game window resolution</value>
        public static System.Windows.Size Resolution = new System.Windows.Size(800, 600);

        /// <value>If true - FPS counter will be shown</value>
        public static bool ShowFPS = false;

        /// <value>If true - Colliders will be shown</value>
        public static bool ShowColliders = false;
        private static SortedSet<Layer> SortedLayers;

        private static readonly Brush FPSForeground = new SolidBrush(Color.White);
        private static readonly Brush FPSBackground = new SolidBrush(Color.Black);
        private static readonly Font FPSFont = new Font(FontFamily.GenericMonospace, 12);
        private static readonly Rectangle FPSRectangle = new Rectangle(20, 20, 80, 20);

        public static void SortLayers()
        {
            SortedLayers = new SortedSet<Layer>(Layer.All);
        }
        public static void FromCamera(Image frame, Graphics g, Camera cam)
        {
            System.Windows.Point startPos =
                new System.Windows.Point(
                    cam.Position.X - cam.BaseSize.Width / 2,
                    cam.Position.Y - cam.BaseSize.Height / 2);


            using (Brush backgroundBrush = new SolidBrush(cam.Background))
            {
                g.FillRectangle(backgroundBrush, 0, 0, (int)cam.BaseSize.Width, (int)cam.BaseSize.Height);
            }

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

                    obj.InheritGraphicsTransform(g, startPos);


                    obj.Render(g, startPos);

                    if (ShowColliders && obj is GameObject)
                    {
                        GameObject gobj = (GameObject)obj;
                        if (gobj.Collider != null)
                        {
                            foreach (System.Windows.Point p in gobj.Collider.GetGlobalPoints(gobj))
                            {
                                g.FillRectangle(new SolidBrush(Color.Blue), new Rectangle(new Point((int)p.X - 1 - (int)startPos.X, (int)p.Y - 1 - (int)startPos.Y), new Size(3, 3)));
                            }
                        }
                    }
                    g.ResetTransform();

                }
            }

        }

    }
}
