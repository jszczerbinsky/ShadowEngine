using ShadowEngine.Config;
using ShadowEngine.Exceptions;
using ShadowEngine.Input.Keyboard;
using ShadowEngine.Objects.Texturing;
using ShadowEngine.Rendering;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ShadowEngine
{
    /// <summary>
    /// Class of ShadowEngine project.
    /// You have to extend this class to create a ShadowEngine project.
    /// You should extend this class only once in your project.
    /// </summary>
    public abstract class ShadowEngineProject : ConfigSavable
    {
        /// <value>Gets actual project.</value>
        public static ShadowEngineProject Project { get; private set; }

        /// <value>Options loaded from ProjectConfig.json file.</value>
        public ProjectConfig Config { get; private set; }

        /// <summary>
        /// This method is called only once, after engine initialization.
        /// </summary>
        public abstract void OnStart();

        /// <summary>
        /// This method is called ater every tick of game timer.
        /// </summary>
        public abstract void OnTick();

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="args">Apllication startup arguments.</param>
        protected ShadowEngineProject(string[] args)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                ShowWindow(GetConsoleWindow(), SW_HIDE);
            Project = this;

            LoadConfig();

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
                pc.ShadowEngineVersion = val["ShadowEngineVersion"];
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
