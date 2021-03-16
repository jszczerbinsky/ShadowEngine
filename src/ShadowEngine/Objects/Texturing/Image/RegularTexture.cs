using ShadowEngine.Objects.Parameters;
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

        public override void Render(Graphics g, TexturedObject obj, Vector2D cameraPos)
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
                    ), new System.Drawing.Size(
                        (int)Math.Round(obj.Scale.Width * obj.Size.Width, MidpointRounding.AwayFromZero),
                        (int)Math.Round(obj.Scale.Height * obj.Size.Height, MidpointRounding.AwayFromZero)
                        )
                    )
                );
        }
    }
}
