using ShadowBuild.Utils;
using System.Windows.Forms;

namespace ShadowBuild
{
    internal class GameWindow : Form
    {
        private Ticker renderTicker;
        internal PictureBox display;

        public GameWindow()
        {
            Render.gameWindow = this;
            Render.initialize();
            InitializeComponent();

            renderTicker = new Ticker(Render.maxFPS);
            renderTicker.onTick += Render.renderNewFrame;
            this.Show();
        }

        private void InitializeComponent()
        {
            this.display = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.display)).BeginInit();
            this.SuspendLayout();
            // 
            // display
            // 
            this.display.Location = new System.Drawing.Point(0, 0);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(100, 50);
            this.display.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.display.TabIndex = 0;
            this.display.TabStop = false;
            // 
            // GameWindow
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.display);
            this.Name = "GameWindow";
            ((System.ComponentModel.ISupportInitialize)(this.display)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
