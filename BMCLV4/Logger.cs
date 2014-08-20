using System;
using System.Globalization;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Collections;
using BMCLV4.Windows;

namespace BMCLV4
{
    static public class Logger
    {
        public enum LogType
        {
            Error,Info,Crash,Exception,Game,Fml,
        }
        
        static public bool Debug = false;
        static readonly LogWindow LogWindow = new LogWindow();
        static public void Start()
        {
            var fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\bmcl.log", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            fs.Close();
            if (Debug)
            {
                LogWindow.Show();
            }
        }
        static public void Stop()
        {
            if (Debug)
            {
                LogWindow.Close();
            }
        }

        static private string WriteInfo(LogType type = LogType.Info)
        {
            switch (type)
            {
                case LogType.Error:
                    return (DateTime.Now.ToString(CultureInfo.InvariantCulture) + "错误:");
                case LogType.Info:
                    return (DateTime.Now.ToString(CultureInfo.InvariantCulture) + "信息:");
                case LogType.Crash:
                    return (DateTime.Now.ToString(CultureInfo.InvariantCulture) + "崩溃:");
                case LogType.Exception:
                    return (DateTime.Now.ToString(CultureInfo.InvariantCulture) + "异常:");
                case LogType.Game:
                    return (DateTime.Now.ToString(CultureInfo.InvariantCulture) + "游戏:");
                case LogType.Fml:
                    return (DateTime.Now.ToString(CultureInfo.InvariantCulture) + "FML :");
                default:
                    return (DateTime.Now.ToString(CultureInfo.InvariantCulture) + "信息:");
            }
        }
        static private void Write(string str, LogType type = LogType.Info)
        {
            var fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\bmcl.log", FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            var sw = new StreamWriter(fs, Encoding.UTF8);
            sw.WriteLine(WriteInfo(type) + str);
            sw.Close();
            fs.Close();
            if (Debug)
            {
                LogWindow.WriteLine(str, type);
            }
        }
        static private void Write(Stream s, LogType type = LogType.Info)
        {
            var sr = new StreamReader(s);
            Write(sr.ReadToEnd(), type);
            if (Debug)
            {
                s.Position = 0;
                LogWindow.WriteLine(sr.ReadToEnd(),type);
            }
        }


        static public void Log(string str, LogType type = LogType.Info)
        {
            Write(str, type);
        }
        static public void Log(Config cfg, LogType type = LogType.Info)
        {
            var cfgSerializer = new DataContractSerializer(typeof(Config));
            var ms=new MemoryStream();
            cfgSerializer.WriteObject(ms, cfg);
            ms.Position = 0;
            Write(ms, type);
        }
        static public void Log(Stream s, LogType type = LogType.Info)
        {
            var sr = new StreamReader(s);
            Write(sr.ReadToEnd(), type);
        }
        static public void Log(Exception ex, LogType type = LogType.Exception)
        {
            var message = new StringBuilder();
            message.AppendLine(ex.Source);
            message.AppendLine(ex.ToString());
            message.AppendLine(ex.Message);
            foreach (DictionaryEntry data in ex.Data)
                message.AppendLine(string.Format("Key:{0}\nValue:{1}", data.Key, data.Value));
            message.AppendLine(ex.StackTrace);
            var iex = ex;
            while (iex.InnerException != null)
            {
                message.AppendLine("------------------------");
                iex = iex.InnerException;
                message.AppendLine(iex.Source);
                message.AppendLine(iex.ToString());
                message.AppendLine(iex.Message);
                foreach (DictionaryEntry data in ex.Data)
                    message.AppendLine(string.Format("Key:{0}\nValue:{1}", data.Key, data.Value));
                message.AppendLine(iex.StackTrace);
            }
            Write(message.ToString(), type);
        }

        static public void Log(LogType type = LogType.Info, params string[] messages)
        {
            var sb = new StringBuilder();
            foreach (string str in messages)
            {
                sb.Append(str);
            }
            Write(sb.ToString(), type);
        }

        static public void Log(params string[] messages)
        {
            var sb = new StringBuilder();
            foreach (string str in messages)
            {
                sb.Append(str);
            }
            Write(sb.ToString());
        }

        static public void Info(string message)
        {
            Logger.Log(message);
        }

        static public void Error(string message)
        {
            Logger.Log(message, LogType.Error);
        }

        static public void Error(Exception ex)
        {
            Logger.Log(ex);
        }
        

    }
}
