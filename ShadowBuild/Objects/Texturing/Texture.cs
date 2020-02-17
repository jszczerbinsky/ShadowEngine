using ShadowBuild.Config;
using ShadowBuild.Exceptions;
using ShadowBuild.Objects.Texturing.Image;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ShadowBuild.Objects.Texturing
{
    public abstract class Texture : ConfigSavable
    {
        public static List<Texture> All = new List<Texture>();

        public string Name;

        public abstract System.Windows.Size GetSize();
        public abstract void Render(Graphics g, TexturedObject obj, System.Windows.Point cameraPos);

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

        public static void RenderObjectCenters(Graphics g, TexturedObject obj, System.Windows.Point cameraPos)
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
        public static void RenderObjectBorders(Graphics g, TexturedObject obj, System.Windows.Point cameraPos)
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
                        (int)(obj.ActualTexture.GetSize().Width * obj.Size.Width),
                        (int)(obj.ActualTexture.GetSize().Height * obj.Size.Height))
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
                        (int)(obj.ActualTexture.GetSize().Width * obj.Size.Width),
                        (int)(obj.ActualTexture.GetSize().Height * obj.Size.Height))
                    ));
        }


        public static void SaveConfig(string path)
        {
            List<ColorTexture> c = new List<ColorTexture>();
            List<RegularTexture> r = new List<RegularTexture>();
            List<GridTexture> g = new List<GridTexture>();
            foreach (Texture t in All)
                if (t is ColorTexture) c.Add((ColorTexture)t);
                else if (t is RegularTexture) r.Add((RegularTexture)t);
                else g.Add((GridTexture)t);

            var serialized = new
            {
                Image = new
                {
                    Regular = r,
                    Grid = g
                },
                Other = new
                {
                    Color = c
                }
            };
            WriteConfigFile(path, serialized);
        }
        public static void LoadConfig(string path)
        {
            dynamic val = ReadConfigFile(path);

            try
            {
                var a = val["Image"]["Regular"];
                var b = val["Image"]["Grid"];
                var c = val["Other"]["Color"];
            }
            catch (Exception e)
            {
                throw new ConfigException(path + " config file is incorrect", e);
            }

            foreach (Dictionary<string, object> dict in val["Image"]["Regular"])
            {
                string name;
                string p;
                try
                {
                    name = (string)dict["Name"];
                    p = (string)dict["ImagePath"];
                }
                catch (Exception e)
                {
                    throw new ConfigException(path + " config file is incorrect", e);
                }
                All.Add(new RegularTexture(name, p));
            }
            foreach (Dictionary<string, object> dict in val["Image"]["Grid"])
            {
                string name;
                string p;
                int xc;
                int yc;
                try
                {
                    name = (string)dict["Name"];
                    p = (string)dict["ImagePath"];
                    xc = (int)dict["xCount"];
                    yc = (int)dict["yCount"];
                }
                catch (Exception e)
                {
                    throw new ConfigException(path + " config file is incorrect", e);
                }
                All.Add(new GridTexture(name, p, xc, yc));
            }
            foreach (Dictionary<string, object> dict in val["Other"]["Color"])
            {
                string name;
                string hex;
                System.Windows.Size p;
                ColorTextureShape shape;
                try
                {
                    name = (string)dict["Name"];
                    hex = (string)dict["HexColor"];
                    Dictionary<string, object> sd = (Dictionary<string, object>)dict["Size"];
                    p = new System.Windows.Size((int)sd["Width"], (int)sd["Height"]);
                    shape = (ColorTextureShape)Enum.Parse(typeof(ColorTextureShape), (string)dict["ShapeString"]);
                }
                catch (Exception e)
                {
                    throw new ConfigException(path + " config file is incorrect", e);
                }
                ColorConverter cc = new ColorConverter();
                Color c = (Color)cc.ConvertFromString(hex);
                All.Add(new ColorTexture(name, c, shape, p));
            }

            foreach (Texture t in All)
                if (t is ImageTexture)
                    ((ImageTexture)t).InitializeImage();
        }
    }
}
