using System;
using System.Drawing;
using ShadowBuild.Rendering;

namespace ShadowBuild.Objects.Texturing
{
    public class CameraViewTexture : Texture
    {
        public Camera Cam;
        private Bitmap view;
        private Graphics viewGraphics;

        public CameraViewTexture(string name, Camera camera)
        {
            this.Name = name;
            this.Cam = camera;
            view = new Bitmap((int)Cam.GetRealSize().Width, (int)Cam.GetRealSize().Height);
            viewGraphics = Graphics.FromImage(view);
        }

        public override void Render(Graphics g, TexturedObject obj, System.Windows.Point cameraPos)
        {
            Rendering.Render.FromCamera(view, viewGraphics, Cam);

            g.DrawImage(
                view,
                new Rectangle(
                    new Point(
                        (int)(obj.GetStartPosition().X - cameraPos.X),
                        (int)(obj.GetStartPosition().Y - cameraPos.Y)
                    ), new Size(
                        (int)(obj.SizeMultipler.Width * obj.BaseSize.Width),
                        (int)(obj.SizeMultipler.Height * obj.BaseSize.Height)
                        )
                    )
                );
        }
    }
}
