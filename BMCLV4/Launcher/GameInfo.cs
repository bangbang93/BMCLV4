using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using BMCLV4.Libraries;

namespace BMCLV4.Launcher
{
    [DataContract]
    public class Gameinfo : ICloneable
    {
        [DataMember(IsRequired = true)]
// ReSharper disable InconsistentNaming
        public string id = "";
        [DataMember(IsRequired = false)]
        public string time = "";
        [DataMember(IsRequired = false)]
        public string releaseTime = "";
        [DataMember(IsRequired = false)]
        public string type = "";
        [DataMember(IsRequired = true)]
        public string minecraftArguments = "";
        [DataMember(IsRequired = true)]
        public string mainClass = "";
        [DataMember(IsRequired = true)]
        public Libraryies[] libraries = null;
        [DataMember(IsRequired = false)]
        public int minimumLauncherVersion = 0;
        [DataMember(IsRequired = false)]
        public string assets;
        // ReSharper restore InconsistentNaming
        object ICloneable.Clone()
        {
            return this.clone();
        }
// ReSharper disable once InconsistentNaming
        public Gameinfo clone()
        {
            return (Gameinfo)this.MemberwiseClone();
        }

        static public void Write(Gameinfo info,string path)
        {
            var j = new DataContractJsonSerializer(typeof(Gameinfo));
            var fs = new FileStream(path, FileMode.Create);
            j.WriteObject(fs, info);
            fs.Close();
        }

        static public Gameinfo Read(string path)
        {
            try
            {
                var jsonFile = new StreamReader(path);
                var infoReader = new DataContractJsonSerializer(typeof(Gameinfo));
                var info = infoReader.ReadObject(jsonFile.BaseStream) as Gameinfo;
                jsonFile.Close();
                return info;
            }
            catch (SerializationException ex)
            {
                Logger.Log(ex);
                try
                {
                    var jsonFile = new StreamReader(path, Encoding.Default);
                    var infoReader = new DataContractJsonSerializer(typeof(Gameinfo));
                    var info = infoReader.ReadObject(jsonFile.BaseStream) as Gameinfo;
                    jsonFile.Close();
                    Logger.Log("JSON文件使用", Encoding.Default.EncodingName, "解析成功，将转换为UTF8编码");
                    jsonFile = new StreamReader(path, Encoding.Default);
                    string jsonString = jsonFile.ReadToEnd();
                    jsonFile.Close();
                    var sw = new StreamWriter(path, false, Encoding.UTF8);
                    sw.WriteLine(jsonString);
                    sw.Close();
                    Logger.Log("JSON文件转存完毕");
                    return info;
                }
                catch (SerializationException ex1)
                {
                    Logger.Log(ex1);
                    return null;
                }
            }
        }

        static public string GetGameInfoJsonPath(string version)
        {
            var jsonFilePath = new StringBuilder();
            jsonFilePath.Append(AppDomain.CurrentDomain.BaseDirectory + @"\.minecraft\versions\");
            jsonFilePath.Append(version);
            jsonFilePath.Append(@"\");
            jsonFilePath.Append(version);
            jsonFilePath.Append(".json");
            if (!File.Exists(jsonFilePath.ToString()))
            {
                var mcpath = new DirectoryInfo(Path.GetDirectoryName(jsonFilePath.ToString()));
                bool find = false;
                foreach (FileInfo js in mcpath.GetFiles())
                {
                    if (js.FullName.EndsWith(".json"))
                    {
                        if (Read(js.FullName) != null)
                        {
                            jsonFilePath = new StringBuilder(js.FullName);
                            find = true;
                        }
                    }
                }
                if (!find)
                {
                    return null;
                }
                return jsonFilePath.ToString();
            }
            return jsonFilePath.ToString();
        }
    }
}
