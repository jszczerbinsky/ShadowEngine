using ShadowBuild.Input;
using ShadowBuild.Rendering;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShadowBuild
{
    internal class GameWindow : Form
    {
        private PictureBox display;
        internal static GameWindow actualGameWindow;

        public GameWindow()
        {
            Log.say("Creating new game window");

            this.FormClosing += onClose;

            actualGameWindow = this;
            Camera.defaultCam = new Camera(0, 0, 800, 600);
            InitializeComponent();

            Log.say("Initializing ticker");
            Loop.startTicker();
            Loop.onTick += this.renderNewFrame;

            this.Show();
            Log.say("Calling OnStart");
            ShadowBuildProject.project.OnStart();

        }

        internal void renderNewFrame()
        {
            if (Camera.defaultCameraMode == DefaultCameraMode.RESIZE_WITH_WINDOW)
                Camera.defaultCam.setSize(Render.resolution);
            this.Invoke(new Action(() =>
            {
                Image tmp = this.display.Image;
                this.display.Image = Render.fromCamera(Camera.defaultCam);
                if (tmp != null) tmp.Dispose();
            }));
        }

        private void onClose(object sender, EventArgs a)
        {
            Log.say("Closing...");
            Loop.abortThread();
        }

        private void InitializeComponent()
        {
            this.display = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.display)).BeginInit();
            this.SuspendLayout();
            // 
            // display
            // 
            this.display.Dock = System.Windows.Forms.DockStyle.Fill;
            this.display.Location = new System.Drawing.Point(0, 0);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(782, 553);
            this.display.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.display.TabIndex = 0;
            this.display.TabStop = false;
            // 
            // GameWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.display);
            this.Name = "GameWindow";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.display)).EndInit();
            this.ResumeLayout(false);

        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            Keyboard.setKeyState(e, true);
        }
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            Keyboard.setKeyState(e, false);
        }
    }
}
