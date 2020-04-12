using System;
using System.IO;
using System.Media;

namespace ShadowBuild.Sound
{
    internal class SoundInstance 
    {
        internal MemoryStream ms;
        internal SoundPlayer sp;

        internal SoundInstance()
        {
            ms = new MemoryStream();
        }
        internal void Init()
        {
            sp = new SoundPlayer(ms);
        }
        internal void Play()
        {
            sp.Play();
        }
        internal void Stop()
        {
            try
            {
                if (sp != null)
                {
                    sp.Stop();
                    sp.Dispose();
                }
                if (ms != null)
                    ms.Close();
            }catch(Exception)
            {

            }
        }
    }
}
