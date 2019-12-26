using Newtonsoft.Json;
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

        public static string GetActualConfig()
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
            return JsonConvert.SerializeObject(serialized);
        }
        public static void SaveConfig(string path, ConfigType cfgType)
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
            WriteConfigFile(path, serialized, cfgType);
        }
        public static void LoadConfig(string path, ConfigType cfgType)
        {
            var deserialized = new
            {
                image = new
                {
                    regular = new List<RegularTexture>(),
                    grid = new List<GridTexture>()
                },
                other = new
                {
                    color = new List<ColorTexture>()
                }
            };

            deserialized = ReadConfigFile(path, deserialized, cfgType);

            All = new List<Texture>();
            foreach (RegularTexture t in deserialized.image.regular)
                All.Add(t);
            foreach (GridTexture t in deserialized.image.grid)
                All.Add(t);
            foreach (ColorTexture t in deserialized.other.color)
                All.Add(t);

            foreach (Texture t in All)
                if (t is ImageTexture)
                    ((ImageTexture)t).InitializeImage();
        }
    }
}
