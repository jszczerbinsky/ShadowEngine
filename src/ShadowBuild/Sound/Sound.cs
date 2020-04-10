using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Reflection;
using System.Threading;
using ShadowBuild.Exceptions;

namespace ShadowBuild.Sound
{
    public class Sound
    {
        private List<SoundInstance> intances = new List<SoundInstance>();
        private MemoryStream ms = new MemoryStream();

        public Sound(string path)
        {
            FileStream fs = null;

            try
            {
                fs = new FileStream(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/" + path, FileMode.Open);
            }catch(FileNotFoundException e)
            {
                throw new SoundException("Not found " + Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/" + path + " file", e);
            }
            fs.CopyTo(ms);
            fs.Close();
            ms.Position = 0;
        }
        public void Play()
        {
            SoundInstance i = new SoundInstance();
            i.ms = new MemoryStream();
            ms.CopyTo(i.ms);
            ms.Position = 0;
            i.ms.Position = 0;
            i.Init();
            intances.Add(i);
            i.Play();
        }
        public void Stop()
        {
            foreach (SoundInstance si in intances)
                si.Stop();
            intances.Clear();
        }
    }
}
