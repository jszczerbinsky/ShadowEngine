using ShadowBuild.Config;
using System.Windows.Forms;

namespace ShadowBuild
{
    public abstract class ShadowBuildProject
    {
        internal static ShadowBuildProject project;
        public ShadowBuildProjectConfig Config { get; private set; }

        public static class ConfigFiles
        {
            public const string AxisConfigPath = "Axes.json";
            public const string LayerConfigPath = "Layers.json";
            public const string TextureConfigPath = "textures.json";
        }

        public abstract void OnStart();
        public abstract void OnTick();

        public ShadowBuildProject()
        {
            Log.Say("Starting new ShadowBuild project");

            project = this;


            new GameWindow();
            Loop.OnTick += OnTick;
            Application.Run(GameWindow.actualGameWindow);
        }

        
    }
}
