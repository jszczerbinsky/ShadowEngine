using ShadowBuild.Config;
using ShadowBuild.Exceptions;
using ShadowBuild.Input.Keyboard;
using ShadowBuild.Objects.Texturing;
using ShadowBuild.Rendering;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ShadowBuild
{
    public abstract class ShadowBuildProject : ConfigSavable
    {
        public static ShadowBuildProject Project { get; private set; }
        public ProjectConfig Config { get; private set; }


        public abstract void OnStart();
        public abstract void OnTick();

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        protected ShadowBuildProject(string[] args)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                ShowWindow(GetConsoleWindow(), SW_HIDE);
            Project = this;

            Log.Say("Loading project config");
            LoadConfig();
            Log.Say("Config file loaded");

            CheckArgs(args);

            new GameWindow();
            Loop.OnTick += OnTick;
            Application.Run(GameWindow.actualGameWindow);
        }
        private bool CheckArg(string arg, string content)
        {
            if (arg.Equals(content)) return true;
            return false;
        }
        private void CheckArgs(string[] args)
        {
            foreach (string arg in args)
            {
                if (CheckArg(arg, "-console") && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    ShowWindow(GetConsoleWindow(), SW_SHOW);
                else if (CheckArg(arg, "-showfps"))
                    Render.ShowFPS = true;
                else if (CheckArg(arg, "-showcolliders"))
                    Render.ShowColliders = true;
            }
        }
        private void LoadConfig()
        {
            dynamic val = ReadConfigFile("ProjectConfig.json");

            ProjectConfig pc = new ProjectConfig();

            try
            {
                pc.Name = val["Name"];
                pc.Author = val["Author"];
                pc.ProjectVersion = val["ProjectVersion"];
                pc.ShadowBuildVersion = val["ShadowBuildVersion"];
                dynamic sr = val["StartResolution"];
                pc.StartResolution = new System.Drawing.Size(sr["Width"], sr["Height"]);
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
