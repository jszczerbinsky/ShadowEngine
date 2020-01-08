using ShadowBuild.Input.Keyboard;
using ShadowBuild.Input.Mouse;
using ShadowBuild.Objects.Animationing;
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
            Log.Say("Creating new game window");

            this.FormClosing += OnClose;

            actualGameWindow = this;
            Camera.Default = new Camera(0, 0, 800, 600);
            InitializeComponent();

            Log.Say("Initializing ticker");
            Loop.StartTicker();
            Loop.OnTick += this.RenderNewFrame;
            Loop.OnTick += Animation.OnTick;
            Mouse.OnStart();
            Loop.OnTick += Mouse.OnTick;

            this.Show();
            Log.Say("Calling OnStart");
            ShadowBuildProject.Project.OnStart();
            Log.Space();
            Log.Say("------Listing Render Layers-----");
            Log.Space();
            Log.ListLayers();

        }

        internal void RenderNewFrame()
        {
            Camera.Default.SetSize(Render.Resolution);
            this.Invoke(new Action(() =>
            {
                Image tmp = this.display.Image;
                this.display.Image = Render.FromCamera(Camera.Default);
                if (tmp != null) tmp.Dispose();
            }));
        }

        private void OnClose(object sender, EventArgs a)
        {
            Log.Say("Closing...");
            Loop.AbortThread();
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
            this.display.TabIndex = 0;
            this.display.TabStop = false;
            this.display.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.display.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // GameWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.display);
            this.Name = "GameWindow";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.display)).EndInit();
            if (ShadowBuildProject.Project.Config.StartFullscreen)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
            this.ResumeLayout(false);

        }
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            Mouse.SetMouseButtonState(e, true);
        }
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            Mouse.SetMouseButtonState(e, false);
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            Keyboard.SetKeyState(e, true);
        }
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            Keyboard.SetKeyState(e, false);
        }
    }
}
