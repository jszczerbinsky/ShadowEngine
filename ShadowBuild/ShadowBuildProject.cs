
using System.Windows.Forms;

namespace ShadowBuild
{
    public abstract class ShadowBuildProject
    {
        internal static ShadowBuildProject project;

        public abstract void OnStart();
        public abstract void OnTick();

        public ShadowBuildProject()
        {
            Log.say("Starting new ShadowBuild project");

            project = this;


            Render.gameWindow = new GameWindow();
            Time.onTick += OnTick;
            Application.Run(Render.gameWindow);
        }
    }
}
