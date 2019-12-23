using ShadowBuild.Objects.Dimensions;
using System.Drawing;

namespace ShadowBuild.Objects.Texturing
{
    public class RegularTexture : Texture
    {

        public RegularTexture(Bitmap image)
        {
            this.Image = image;
        }

        public override _2Dsize GetSize()
        {
            return new _2Dsize(this.Image.Width, this.Image.Height);
        }

        public override void Render(Graphics g, GameObject obj, _2Dsize cameraPos)
        {

            RegularTexture tex = (RegularTexture)obj.ActualTexture;

            g.DrawImage(
                tex.Image,
                new Rectangle(
                    new Point(
                        (int)(obj.GetStartPosition().X - cameraPos.X),
                        (int)(obj.GetStartPosition().Y - cameraPos.Y)
                    ), new Size(
                        (int)(tex.Image.Width * obj.Size.X),
                        (int)(tex.Image.Height * obj.Size.Y)
                        )
                    )
                );
        }
    }
}
