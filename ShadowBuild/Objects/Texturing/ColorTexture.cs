﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Drawing;

namespace ShadowBuild.Objects.Texturing
{
    public class ColorTexture : Texture
    {
        public Color Color;
        public System.Windows.Point Size;
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("Shape")]
        public Shape Shape;

        public ColorTexture(string name, Color color, Shape shape, System.Windows.Point size)
        {
            this.Name = name;
            this.Shape = shape;
            this.Color = color;
            this.Size = size;
        }

        public override System.Windows.Point GetSize()
        {
            return Size;
        }

        public override void Render(Graphics g, GameObject obj, System.Windows.Point cameraPos)
        {
            ColorTexture tex = (ColorTexture)obj.ActualTexture;
            Brush brush = new SolidBrush(tex.Color);
            Rectangle size = new Rectangle(
                new System.Drawing.Point(
                     (int)(obj.GetStartPosition().X - cameraPos.X),
                     (int)(obj.GetStartPosition().Y - cameraPos.Y)
                ),
                new Size(
                    (int)(tex.Size.X * obj.Size.X),
                    (int)(tex.Size.Y * obj.Size.Y)
                )
            );

            if (tex.Shape == Shape.ELLIPSE)
                g.FillEllipse(brush, size);
            else g.FillRectangle(brush, size);
        }
    }
}
