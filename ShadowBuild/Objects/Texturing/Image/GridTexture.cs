using ShadowBuild.Exceptions;
using ShadowBuild.Objects.Dimensions;
using System.Drawing;

namespace ShadowBuild.Objects.Texturing.Image
{
    public class GridTexture : ImageTexture
    {
        public int xCount;
        public int yCount;

        //Empty constructor for deserialization
        public GridTexture() { }
        public GridTexture(string name, string imgPath, int xCount, int yCount)
        {
            this.Name = name;
            this.ImagePath = imgPath;
            this.xCount = xCount;
            this.yCount = yCount;
        }

        public override _2Dsize GetSize()
        {
            return new _2Dsize(Image.Size.Width * xCount, Image.Size.Height * yCount);
        }

        public override void Render(Graphics g, GameObject obj, _2Dsize cameraPos)
        {
            GridTexture tex = (GridTexture)obj.ActualTexture;

            if (tex.Image == null) throw new RenderException("Image was not initialized in texture \"" + tex.Name);


            for (int x = 0; x < tex.xCount; x++)
            {
                for (int y = 0; y < tex.yCount; y++)
                {

                    g.DrawImage(
                        tex.Image,
                        new Rectangle(
                            new Point(
                                (int)(obj.GetStartPosition().X - cameraPos.X + x * tex.Image.Width * obj.Size.X),
                                (int)(obj.GetStartPosition().Y - cameraPos.Y + y * tex.Image.Height * obj.Size.Y)
                            ), new Size(
                                (int)(tex.Image.Width * obj.Size.X),
                                (int)(tex.Image.Height * obj.Size.Y)
                                )
                            )
                        );
                }
            }
        }
    }
}
