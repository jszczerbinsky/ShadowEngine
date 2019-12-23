using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Config
{
    public class ConfigSavable
    {
        protected static T ReadConfigFile<T>(string path, T type)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string str = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            return (T)JsonConvert.DeserializeAnonymousType(str, type);
        }
    }
}
