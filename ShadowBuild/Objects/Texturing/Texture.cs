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

        public abstract System.Windows.Point GetSize();
        public abstract void Render(Graphics g, RenderableObject obj, System.Windows.Point cameraPos);

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

        public static void RenderObjectCenters(Graphics g, RenderableObject obj, System.Windows.Point cameraPos)
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
        public static void RenderObjectBorders(Graphics g, RenderableObject obj, System.Windows.Point cameraPos)
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
                image = new
                {
                    regular = r,
                    grid = g
                },
                other = new
                {
                    color = c
                }
            };
            WriteConfigFile(path, serialized);
        }
        public static void LoadConfig(string path)
        {
            dynamic val = ReadConfigFile(path);

            foreach (Dictionary<string, object> dict in val["image"]["regular"])
                All.Add(new RegularTexture((string)dict["Name"], (string)dict["ImagePath"]));
            foreach (Dictionary<string, object> dict in val["image"]["grid"])
                All.Add(new GridTexture((string)dict["Name"], (string)dict["ImagePath"], (int)dict["xCount"], (int)dict["yCount"]));
            foreach (Dictionary<string, object> dict in val["other"]["color"])
            {
                Dictionary<string, object> cd = (Dictionary<string, object>)dict["Color"];
                Color c = Color.FromArgb((int)cd["A"], (int)cd["R"], (int)cd["G"], (int)cd["B"]);
                Dictionary<string, object>sd = (Dictionary<string, object>) dict["Size"];
                System.Windows.Point p = new System.Windows.Point((int)sd["X"], (int)sd["Y"]);
                All.Add(new ColorTexture((string)dict["Name"], c, (Shape)Enum.Parse(typeof(Shape), (string)dict["ShapeString"]), p));
            }

            foreach (Texture t in All)
                if (t is ImageTexture)
                    ((ImageTexture)t).InitializeImage();
        }
    }
}
