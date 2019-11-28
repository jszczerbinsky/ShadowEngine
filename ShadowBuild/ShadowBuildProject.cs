using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Render.gameWindow.renderTicker.onTick += OnTick;
            Application.Run(Render.gameWindow);
        }
    }
}
