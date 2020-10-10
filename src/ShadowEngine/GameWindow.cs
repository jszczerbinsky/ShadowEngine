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
        private Display display;
        internal static GameWindow actualGameWindow;

        public GameWindow()
        {
            Log.Say("Creating new game window");

            this.FormClosing += OnClose;

            actualGameWindow = this;
            Camera.Default = new Camera(0, 0, 800, 600);
            InitializeComponent();

            this.Size = new Size(
                ShadowEngine.Project.Config.StartResolution.Width,
                ShadowEngine.Project.Config.StartResolution.Height);
            if (ShadowEngine.Project.Config.StartFullscreen)
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = FormBorderStyle.None;
            }

            Log.Say("Initializing GameLoop");
            this.display.Initialize(new Size((int)Render.Resolution.Width, (int)Render.Resolution.Height));
            Loop.OnTick += this.RenderNewFrame;
            Loop.OnTick += Animation.OnTick;
            Mouse.OnStart();
            Loop.OnTick += Mouse.OnTick;

            this.Show();
            Loop.StartTicker();
            Log.Say("Calling OnStart");
            ShadowEngine.Project.OnStart();
            Render.SortLayers();
            Log.Space();
            Log.Say("------Listing Render Layers-----");
            Log.Space();

        }
        internal void RenderNewFrame()
        {
            Camera.Default.ChangeBaseSize(Render.Resolution);
            this.Invoke(new Action(() =>
            {
                Render.FromCamera(this.display.Image, this.display.DisplayGraphics, Camera.Default);
                this.display.Refresh();
            }));
        }

        private void OnClose(object sender, EventArgs a)
        {
            Log.Say("Closing...");
            Loop.AbortThread();
        }

        private void InitializeComponent()
        {
            this.display = new Display();
            ((System.ComponentModel.ISupportInitialize)(this.display)).BeginInit();
            this.SuspendLayout();
            // 
            // display
            // 
            this.display.Dock = System.Windows.Forms.DockStyle.Fill;
            this.display.Location = new System.Drawing.Point(0, 0);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(782, 553);
            this.display.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.display.TabIndex = 0;
            this.display.TabStop = false;
            this.display.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.display.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // GameWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.SizeChanged += OnResize;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.display);
            this.Name = "GameWindow";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.display)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void OnResize(object sender, EventArgs e)
        {
            Render.Resolution = new System.Windows.Size(this.Size.Width, this.Size.Height);
            this.display.Initialize(new Size(this.Size.Width, this.Size.Height));
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
