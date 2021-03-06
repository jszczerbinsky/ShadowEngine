﻿using ShadowEngine.Exceptions;
using System;
using System.IO;
using System.Reflection;
using System.Web.Script.Serialization;

namespace ShadowEngine.Config
{
    public abstract class ConfigSavable
    {
        protected static dynamic ReadConfigFile(string path)
        {
            path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + '/' + path;
            FileStream fs;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            }
            catch (Exception e)
            {
                Exception ex = new ConfigException("Cannot open file \"" + path + "\"", e);
                Log.Exception(ex);
                throw ex;
            }


            StreamReader sr = new StreamReader(fs);
            string str = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            object obj;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                obj = jss.Deserialize<dynamic>(str);
            }
            catch (Exception e)
            {
                Exception ex = new ConfigException("Cannot convert config file to object in \"" + path + "\"", e);
                Log.Exception(ex);
                throw ex;
            }
            return obj;


        }
        protected static void WriteConfigFile(string path, object o)
        {
            path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + '/' + path;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string str = jss.Serialize(o);
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(str);
            sw.Close();
            fs.Close();
        }
    }
}
