using ShadowBuild.Utils;
using System;
using System.Windows.Forms;

namespace ShadowBuild
{
    internal class GameWindow : Form
    {
        private Ticker renderTicker;
        internal PictureBox display;

        public GameWindow()
        {
            Log.say("Creating new game window");

            this.FormClosing += onClose;

            Render.gameWindow = this;
            Render.initialize();
            InitializeComponent();

            Log.say("Initializing ticker");
            renderTicker = new Ticker(Render.maxFPS);
            renderTicker.onTick += Render.renderNewFrame;

            this.Show();
            Log.say("Calling OnStart");
            ShadowBuildProject.project.OnStart();

        }

        private void onClose(object sender, EventArgs a)
        {
            Log.say("Closing...");
            renderTicker.abort();
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GameWindow";
            ((System.ComponentModel.ISupportInitialize)(this.display)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
