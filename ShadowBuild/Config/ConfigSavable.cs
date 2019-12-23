using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
            }
            else
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
            string str = JsonConvert.SerializeObject(o);
            if (cfgType == ConfigType.BINARY)
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                bf.Serialize(fs, str);
                fs.Close();
            }
            else
            {
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(str);
                sw.Close();
                fs.Close();
            }
        }
    }
}
