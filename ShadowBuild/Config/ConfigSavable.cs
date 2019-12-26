using Newtonsoft.Json;
using ShadowBuild.Exceptions;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ShadowBuild.Config
{
    public abstract class ConfigSavable
    {
        protected static T ReadConfigFile<T>(string path, T type, ConfigType cfgType)
        {
            FileStream fs;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            }
            catch (Exception e)
            {
                throw new ConfigException("Cannot open file \"" + path + "\"", e);
            }

            if (cfgType == ConfigType.JSON)
            {

                StreamReader sr = new StreamReader(fs);
                string str = sr.ReadToEnd();
                sr.Close();
                fs.Close();
                T obj;
                try
                {
                    obj = (T)JsonConvert.DeserializeAnonymousType(str, type);
                }
                catch (Exception e)
                {
                    throw new ConfigException("Cannot convert config file to object in \"" + path + "\"", e);
                }
                return obj;
            }
            else
            {
                BinaryFormatter bf = new BinaryFormatter();
                string str = (string)bf.Deserialize(fs);
                fs.Close();
                T o;
                try
                {
                    o = (T)JsonConvert.DeserializeAnonymousType(str, type);
                }
                catch (Exception e)
                {
                    throw new ConfigException("Cannot convert config file to object in \"" + path + "\"", e);

                }
                return o;
            }
        }
        protected static void WriteConfigFile(string path, object o, ConfigType cfgType)
        {
            string str = JsonConvert.SerializeObject(o);
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);


            if (cfgType == ConfigType.BINARY)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, str);
                fs.Close();
            }
            else
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(str);
                sw.Close();
                fs.Close();
            }
        }
    }
}
