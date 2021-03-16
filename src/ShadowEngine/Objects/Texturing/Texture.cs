using ShadowEngine.Objects.Parameters;
using ShadowEngine.Config;
using ShadowEngine.Exceptions;
using ShadowEngine.Objects.Texturing.Image;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ShadowEngine.Objects.Texturing
{
    /// <summary>
    /// Texture class
    /// </summary>
    public abstract class Texture : ConfigSavable
    {
        /// <value>All set up textures</value>
        public static List<Texture> All = new List<Texture>();

        /// <value>Texture name</value>
        public string Name;

        public abstract void Render(Graphics g, TexturedObject obj, Vector2D cameraPos);

        /// <summary>
        /// Sets up a texture
        /// </summary>
        public static void Setup(Texture t)
        {
            foreach (Texture tex in All)
                if (tex.Name == t.Name)
                    throw new TextureException("Texture name \"" + t.Name + "\" is already in use");
            All.Add(t);
        }

        /// <summary>
        /// Finds texture by name
        /// </summary>
        public static Texture Get(string name)
        {
            foreach (Texture t in All)
                if (t.Name == name)
                    return t;
            throw new TextureException("Cannot find texture \"" + name);
        }

        public static List<Texture> StartsWidth(string name)
        {
            List<Texture> tex = new List<Texture>();

            foreach (Texture t in All)
                if (t.Name.StartsWith(name))
                    tex.Add(t);

            return tex;
        }

        public static void RenderObjectCenters(Graphics g, TexturedObject obj, Vector2D cameraPos)
        {
            if (obj.Visible)
            {
                g.FillEllipse(
                    new SolidBrush(
                            Color.Red
                    ),
                    new Rectangle(
                        new Point(
                            (int)(obj.GetNonRotatedGlobalPosition().X - 2 - cameraPos.X),
                            (int)(obj.GetNonRotatedGlobalPosition().Y - 2 - cameraPos.Y)
                        ),
                        new System.Drawing.Size(
                            5, 5)
                        ));
            }
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
                Exception ex = new ConfigException(path + " config file is incorrect", e);
                Log.Exception(ex);
                throw ex;
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
                    Exception ex = new ConfigException(path + " config file is incorrect", e);
                    Log.Exception(ex);
                    throw ex;
                }
                RegularTexture t = new RegularTexture(name, p);
                if (dict.ContainsKey("InterpolationMode"))
                    t.InterpolationMode = (System.Drawing.Drawing2D.InterpolationMode)
                        Enum.Parse(typeof(System.Drawing.Drawing2D.InterpolationMode),
                        (string)dict["InterpolationMode"]);
                All.Add(t);
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
                    Exception ex = new ConfigException(path + " config file is incorrect", e);
                    Log.Exception(ex);
                    throw ex;
                }
                GridTexture t = new GridTexture(name, p, xc, yc);
                if (dict.ContainsKey("InterpolationMode"))
                    t.InterpolationMode = (System.Drawing.Drawing2D.InterpolationMode)
                        Enum.Parse(typeof(System.Drawing.Drawing2D.InterpolationMode),
                        (string)dict["InterpolationMode"]);
                All.Add(t);
            }
            foreach (Dictionary<string, object> dict in val["Other"]["Color"])
            {
                string name;
                string hex;
                ColorTextureShape shape;
                try
                {
                    name = (string)dict["Name"];
                    hex = (string)dict["HexColor"];
                    Dictionary<string, object> sd = (Dictionary<string, object>)dict["Size"];
                    shape = (ColorTextureShape)Enum.Parse(typeof(ColorTextureShape), (string)dict["ShapeString"]);
                }
                catch (Exception e)
                {
                    Exception ex = new ConfigException(path + " config file is incorrect", e);
                    Log.Exception(ex);
                    throw ex;
                }
                ColorConverter cc = new ColorConverter();
                Color c = (Color)cc.ConvertFromString(hex);
                All.Add(new ColorTexture(name, c, shape));
            }

            foreach (Texture t in All)
                if (t is ImageTexture)
                    ((ImageTexture)t).InitializeImage();
        }
    }
}
