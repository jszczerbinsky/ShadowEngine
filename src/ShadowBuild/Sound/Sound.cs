using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Reflection;
using System.Threading;
using System.Windows.Media;
using System.Windows.Threading;
using ShadowBuild.Exceptions;

namespace ShadowBuild.Sound
{
    public sealed class Sound
    {
        private MediaPlayer player = new MediaPlayer();
        public double volume
        {
            get
            {
                double vol = 0;
                GameWindow.actualGameWindow.Invoke(new Action(() =>
                {
                    vol = player.Volume;
                }));

                return vol;
            }
            set
            {
                GameWindow.actualGameWindow.Invoke(new Action(() =>
                {
                    player.Volume = value;
                }));
            }
        }

        public Sound(string path)
        {
            try
            {
                player.Open(new Uri(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/" + path));
            }
            catch (FileNotFoundException e)
            {
                throw new SoundException("Not found " + Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/" + path + " file", e);
            }
        }
        public void Play()
        {
            GameWindow.actualGameWindow.Invoke(new Action(() => { player.Play(); }));
        }
        public void Pause()
        {
            GameWindow.actualGameWindow.Invoke(new Action(() =>
            {
                player.Pause();
            }));
        }
        public void PlayAgain()
        {
            GameWindow.actualGameWindow.Invoke(new Action(() =>
            {
                player.Position = new TimeSpan(0);
            }));
            GameWindow.actualGameWindow.Invoke(new Action(() =>
            {
                Play();
            }));
        }
    }
}
