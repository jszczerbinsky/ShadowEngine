using ShadowBuild.Config;
using ShadowBuild.Exceptions;
using ShadowBuild.Input.Keyboard;
using ShadowBuild.Objects.Texturing;
using ShadowBuild.Rendering;
using System;
using System.Windows;
using System.Windows.Forms;

namespace ShadowBuild
{
    public abstract class ShadowBuildProject : ConfigSavable
    {
        public static ShadowBuildProject Project { get; private set; }
        public ProjectConfig Config { get; private set; }


        public abstract void OnStart();
        public abstract void OnTick();

        public ShadowBuildProject()
        {
            Project = this;

            Log.Say("Loading project config");
            LoadConfig();
            Log.Say("Config file loaded");

            new GameWindow();
            Loop.OnTick += OnTick;
            Application.Run(GameWindow.actualGameWindow);
        }
        private void LoadConfig()
        {
            dynamic val = ReadConfigFile("ProjectConfig.json");

            ProjectConfig pc = new ProjectConfig();

            try
            {
                pc.Name = val["Name"];
                pc.Author = val["Author"];
                pc.LicenseFilePath = val["LicenseFilePath"];
                pc.ProjectVersion = val["ProjectVersion"];
                pc.ShadowBuildVersion = val["ShadowBuildVersion"];
                dynamic sr = val["StartResolution"];
                pc.StartResolution = new Point(sr["X"], sr["Y"]);
                pc.StartFullscreen = val["StartFullscreen"];
                pc.AxisConfigPath = val["AxisConfigPath"];
                pc.AxisConfigAutoLoad = val["AxisConfigAutoLoad"];
                pc.LayerConfigPath = val["LayerConfigPath"];
                pc.LayerConfigAutoLoad = val["LayerConfigAutoLoad"];
                pc.TextureConfigPath = val["TextureConfigPath"];
                pc.TextureConfigAutoLoad = val["TextureConfigAutoLoad"];
            }
            catch (Exception e)
            {
                throw new ConfigException("ProjectConfig.json file is incorrect", e);
            }
            this.Config = pc;
            if (this.Config.AxisConfigAutoLoad)
                Axis.LoadConfig(this.Config.AxisConfigPath);
            if (this.Config.LayerConfigAutoLoad)
                Layer.LoadConfig(this.Config.LayerConfigPath);
            if (this.Config.TextureConfigAutoLoad)
                Texture.LoadConfig(this.Config.TextureConfigPath);
        }
    }
}
