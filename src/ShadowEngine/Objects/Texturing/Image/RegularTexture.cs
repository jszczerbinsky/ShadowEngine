using ShadowEngine.Exceptions;
using System;
using System.Drawing;

namespace ShadowEngine.Objects.Texturing.Image
{
    /// <summary>
    /// Regular texture class.
    /// With this class you can create standard image textures.
    /// </summary>
    public class RegularTexture : ImageTexture
    {
        //Empty constructor for deserialization
        public RegularTexture() { }

        /// <summary>
        /// Regular texture constructor
        /// </summary>
        /// <param name="name">texture name</param>
        /// <param name="imgPath">path to image</param>
        public RegularTexture(string name, string imgPath)
        {
            this.Name = name;
            this.ImagePath = imgPath;
            InitializeImage();
        }

        public override void Render(Graphics g, TexturedObject obj, System.Windows.Point cameraPos)
        {
            RegularTexture tex = (RegularTexture)obj.GetActualTexture();
            g.InterpolationMode = tex.InterpolationMode;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            if (tex.Image == null) throw new RenderException("Image was not initialized in texture \"" + tex.Name);
            g.DrawImage(
                tex.Image,
                new Rectangle(
                    new Point(
                        (int)(obj.GetStartPosition().X - cameraPos.X),
                        (int)(obj.GetStartPosition().Y - cameraPos.Y)
                    ), new Size(
                        (int)Math.Round(obj.SizeMultipler.Width * obj.BaseSize.Width, MidpointRounding.AwayFromZero),
                        (int)Math.Round(obj.SizeMultipler.Height * obj.BaseSize.Height, MidpointRounding.AwayFromZero)
                        )
                    )
                );
        }
    }
}
