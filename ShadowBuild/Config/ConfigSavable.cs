using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Config
{
    public abstract class ConfigSavable
    {
        protected static T ReadConfigFile<T>(string path, T type, ConfigType cfgType)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            if (cfgType == ConfigType.JSON)
            {
                
                StreamReader sr = new StreamReader(fs);
                string str = sr.ReadToEnd();
                sr.Close();
                fs.Close();
                return (T)JsonConvert.DeserializeAnonymousType(str, type);
            }else
            {
                BinaryFormatter bf = new BinaryFormatter();
                string str = (string)bf.Deserialize(fs);
                fs.Close();
                object o = JsonConvert.DeserializeAnonymousType(str, type);
                return (T)o;
            }
        }
        protected static void WriteConfigFile(string path, object o, ConfigType cfgType)
        {
            if(cfgType == ConfigType.BINARY)
            {
                string str = JsonConvert.SerializeObject(o);
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                bf.Serialize(fs, str);
            }
        }
    }
}
