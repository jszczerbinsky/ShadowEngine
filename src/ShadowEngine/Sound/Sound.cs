using ShadowEngine.Exceptions;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Media;

namespace ShadowEngine.Sound
{

    /// <summary>
    /// With this class you can manage sounds effects.
    /// </summary>
    public sealed class Sound
    {
        private MediaPlayer player = new MediaPlayer();

        /// <value>Volume of sound effect</value>
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
        private Sound()
        {

        }
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="path">Path to an audio file</param>
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
        /// <summary>
        /// Clones sound.
        /// It can be useful for playing multiple sounds with same audio effect.
        /// </summary>
        /// <returns>
        /// Clone of sound object
        /// </returns>
        public Sound Clone()
        {
            Sound s = new Sound();
            s.player = (MediaPlayer)player.Clone();
            s.volume = volume;
            return s;
        }

        /// <summary>
        /// Plays sound
        /// </summary>
        public void Play()
        {
            GameWindow.actualGameWindow.Invoke(new Action(() => { player.Play(); }));
        }

        /// <summary>
        /// Pauses sound
        /// </summary>
        public void Pause()
        {
            GameWindow.actualGameWindow.Invoke(new Action(() =>
            {
                player.Pause();
            }));
        }

        /// <summary>
        /// Plays sound from start
        /// </summary>
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
