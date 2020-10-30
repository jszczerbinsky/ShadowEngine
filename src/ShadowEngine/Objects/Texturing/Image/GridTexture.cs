using ShadowBuild.Exceptions;
using System.Drawing;

namespace ShadowBuild.Objects.Texturing.Image
{
    /// <summary>
    /// Grid texture class.
    /// With this class you can create textures from with grid pattern of images
    /// </summary>
    public class GridTexture : ImageTexture
    {
        public int xCount;
        public int yCount;

        //Empty constructor for deserialization
        public GridTexture() { }

        /// <summary>
        /// Grid texture constructor
        /// </summary>
        /// <param name="name">texture name</param>
        /// <param name="imgPath">path to image</param>
        /// <param name="xCount">count of images in x axis</param>
        /// <param name="yCount">count of images in y axis</param>
        public GridTexture(string name, string imgPath, int xCount, int yCount)
        {
            this.Name = name;
            this.ImagePath = imgPath;
            this.xCount = xCount;
            this.yCount = yCount;
            InitializeImage();
        }

        public override void Render(Graphics g, TexturedObject obj, System.Windows.Point cameraPos)
        {
            GridTexture tex = (GridTexture)obj.ActualTexture;
            g.InterpolationMode = tex.InterpolationMode;

            if (tex.Image == null) throw new RenderException("Image was not initialized in texture \"" + tex.Name);


            for (int x = 0; x < tex.xCount; x++)
            {
                for (int y = 0; y < tex.yCount; y++)
                {

                    g.DrawImage(
                        tex.Image,
                        new Rectangle(
                            new Point(
                                (int)(obj.GetStartPosition().X - cameraPos.X + x * obj.SizeMultipler.Width * obj.BaseSize.Width),
                                (int)(obj.GetStartPosition().Y - cameraPos.Y + y * obj.SizeMultipler.Height * obj.BaseSize.Height)
                            ), new Size(
                                (int)(obj.SizeMultipler.Width * obj.BaseSize.Width),
                                (int)(obj.SizeMultipler.Height * obj.BaseSize.Height)
                                )
                            )
                        );
                }
            }
        }
    }
}
