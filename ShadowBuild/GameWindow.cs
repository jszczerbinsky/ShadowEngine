using ShadowBuild.Utils;
using System.Windows.Forms;

namespace ShadowBuild
{
    internal class GameWindow : Form
    {
        private Ticker renderTicker;

        public GameWindow()
        {
            renderTicker = new Ticker(Render.maxFPS);
            renderTicker.onTick += Render.renderNewFrame;
            this.Show();
        }
    }
}
