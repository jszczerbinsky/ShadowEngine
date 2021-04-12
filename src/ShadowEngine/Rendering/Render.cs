using ShadowEngine.Objects.Parameters;
using ShadowEngine.Objects;
using ShadowEngine.Objects.UI;
using System.Collections.Generic;
using System.Drawing;

namespace ShadowEngine.Rendering
{
    /// <summary>
    /// Render class.
    /// Here you can specify game window resolution and some other rendering options.
    /// </summary>
    public static class Render
    {

        /// <value>Game window resolution</value>
        public static Objects.Parameters.Size Resolution = new Objects.Parameters.Size(800, 600);

        public delegate void OnResolutionUpdateDelegateVoid();
        public static OnResolutionUpdateDelegateVoid OnResolutionUpdate;

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

        /// <summary>
        /// Renders image from camera rendering area.
        /// </summary>
        /// <param name="g">Graphics object to paint</param>
        /// <param name="cam">Camera</param>
        public static void FromCamera(Graphics g, Camera cam)
        {
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
            
            Vector2D startPos =
                new Vector2D(
                    cam.Position.X - cam.Size.Width / 2,
                    cam.Position.Y - cam.Size.Height / 2);


            using (Brush backgroundBrush = new SolidBrush(cam.Background))
            {
                g.FillRectangle(backgroundBrush, 0, 0, (int)cam.Size.Width, (int)cam.Size.Height);
            }


            Loop.OnObjectIterationBegin?.Invoke();
            foreach (Layer l in SortedLayers)
            {
                if (!cam.IsRendering(l)) continue;
                foreach (RenderableObject obj in l.Objects)
                {
                    Loop.OnObjectIteration?.Invoke(obj);

                    if (!obj.Visible) continue;
                    if ((obj is UIObject && ((UIObject)obj).PositionType == UIPositionType.Global) || !(obj is UIObject))
                        if (
                            obj.GetGlobalPosition().X - obj.GetRealSize().Width / 2 > Camera.Default.Position.X + Camera.Default.GetRealSize().Width / 2 ||
                            obj.GetGlobalPosition().X + obj.GetRealSize().Width / 2 < Camera.Default.Position.X - Camera.Default.GetRealSize().Width / 2 ||
                            obj.GetGlobalPosition().Y - obj.GetRealSize().Height / 2 > Camera.Default.Position.Y + Camera.Default.GetRealSize().Height / 2 ||
                            obj.GetGlobalPosition().Y + obj.GetRealSize().Height / 2 < Camera.Default.Position.Y - Camera.Default.GetRealSize().Height / 2
                            ) continue;

                    obj.InheritGraphicsTransform(g, startPos);


                    obj.Render(g, startPos);

                    if (ShowColliders && obj is GameObject)
                    {
                        GameObject gobj = (GameObject)obj;
                        if (gobj.Collider != null)
                        {
                            foreach (Vector2D p in gobj.Collider.GetGlobalPoints(gobj))
                            {
                                g.FillRectangle(new SolidBrush(Color.Blue), new Rectangle(new Point((int)p.X - 1 - (int)startPos.X, (int)p.Y - 1 - (int)startPos.Y), new System.Drawing.Size(3, 3)));
                            }
                        }
                    }
                    g.ResetTransform();

                }
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

        }

    }
}
