using ShadowBuild.Config;
using ShadowBuild.Exceptions;
using ShadowBuild.Objects.Dimensions;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ShadowBuild.Objects.Texturing
{
    public abstract class Texture : ConfigSavable
    {
        public static List<Texture> All = new List<Texture>();

        public string Name;

        public abstract _2Dsize GetSize();
        public abstract void Render(Graphics g, GameObject obj, _2Dsize cameraPos);

        public static void Setup(Texture t)
        {
            foreach (Texture tex in All)
                if (tex.Name == t.Name)
                    throw new TextureException("Texture name \"" + t.Name + "\" is already in use");
            All.Add(t);
        }
        public static Texture Get(string name)
        {
            foreach (Texture t in All)
                if (t.Name == name)
                    return t;
            throw new TextureException("Cannot find texture \"" + name);
        }

        public static void RenderObjectCenters(Graphics g, GameObject obj, _2Dsize cameraPos)
        {
            if (obj.Visible)
            {
                g.FillEllipse(
                    new SolidBrush(
                            Color.Red
                    ),
                    new Rectangle(
                        new Point(
                            (int)(obj.GetGlobalPosition().X - 2 - cameraPos.X),
                            (int)(obj.GetGlobalPosition().Y - 2 - cameraPos.Y)
                        ),
                        new Size(
                            5, 5)
                        ));
            }
        }
        public static void RenderObjectBorders(Graphics g, GameObject obj, _2Dsize cameraPos)
        {
            Random rand = new Random();
            Color fillColor = Color.FromArgb(100, rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));

            g.FillRectangle(
                    new SolidBrush(
                            fillColor
                    ),
                new Rectangle(
                    new Point(
                        (int)(obj.GetStartPosition().X - cameraPos.X),
                        (int)(obj.GetStartPosition().Y - cameraPos.Y)
                    ),
                    new Size(
                        (int)(obj.ActualTexture.GetSize().X * obj.Size.X), (int)(obj.ActualTexture.GetSize().Y * obj.Size.Y))
                    ));

            Color drawColor = Color.FromArgb(100, rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));

            g.DrawRectangle(
                new Pen(
                    new SolidBrush(
                            drawColor
                    ),
                    3),
                new Rectangle(
                    new Point(
                        (int)(obj.GetStartPosition().X - cameraPos.X),
                        (int)(obj.GetStartPosition().Y - cameraPos.Y)
                    ),
                    new Size(
                        (int)(obj.ActualTexture.GetSize().X * obj.Size.X), (int)(obj.ActualTexture.GetSize().Y * obj.Size.Y))
                    ));
        }
    }
}
