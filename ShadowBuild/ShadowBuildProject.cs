using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild
{
    public abstract class ShadowBuildProject
    {
        public ShadowBuildProject()
        {
            Render.gameWindow = new GameWindow();
        }
    }
}
