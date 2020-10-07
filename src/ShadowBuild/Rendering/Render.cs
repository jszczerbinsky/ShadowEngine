using ShadowBuild.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ShadowBuild.Rendering
{
    public static class Render
    {
        public static System.Windows.Size Resolution = new System.Windows.Size(800, 600);
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

                    obj.InheritRotation(g, startPos);


                    obj.Render(g, startPos);

                }
            }

        }

    }
}
