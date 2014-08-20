﻿using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Web;

namespace BMCLV4
{
    class Report
    {
        public Report()
        {
            var thread = new Thread(run);
            thread.Start();
        }

        private void run()
        {
            try
            {
                var sysinfoJsonSerializer = new DataContractJsonSerializer(typeof(Sysinfo));
                var sysinfoJsonStream = new MemoryStream();
                var systeminfo = new Sysinfo();
                sysinfoJsonSerializer.WriteObject(sysinfoJsonStream, systeminfo);
                sysinfoJsonStream.Position = 0;
                var sysinfoJsonReader = new StreamReader(sysinfoJsonStream);
                string sysinfoJson = sysinfoJsonReader.ReadToEnd();
                var ht = new Hashtable
                {
                    {"id", BmclCore.Config.Username},
                    {"sysinfo", sysinfoJson},
                    {"version", BmclCore.BmclVersion}
                };
                string postdata = ParsToString(ht);
                var req = (HttpWebRequest)WebRequest.Create("http://www.bangbang93.com/bmcl/bmcllog.php");
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                byte[] buffer = Encoding.UTF8.GetBytes(postdata);
                req.ContentLength = buffer.Length;
                var s = req.GetRequestStream();
                s.Write(buffer, 0, buffer.Length);
                s.Close();
                req.GetResponse();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }
        public static String ParsToString(Hashtable pars)
        {
            var sb = new StringBuilder();
            foreach (string k in pars.Keys)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }
                sb.Append(HttpUtility.UrlEncode(k) + "=" + HttpUtility.UrlEncode(pars[k].ToString()));
            }
            return sb.ToString();
        }
    }

    [Serializable]
    class Sysinfo
    {
// ReSharper disable InconsistentNaming
        string memory;
        string cpu;
        string bit;
        string video;
        string system;
        // ReSharper restore InconsistentNaming
        public Sysinfo()
        {
            var cimobject1 = new ManagementClass("Win32_PhysicalMemory");
            ManagementObjectCollection moc1 = cimobject1.GetInstances();
            double capacity = moc1.Cast<ManagementObject>().Sum(mo1 => ((Math.Round(Int64.Parse(mo1.Properties["Capacity"].Value.ToString())/1024/1024.0, 1))));
            moc1.Dispose();
            cimobject1.Dispose();
            memory = capacity.ToString("f0")+"MB";

            try  //系统位数，系统名称
            {
                var searcher = new ManagementClass("WIN32_Processor");
                ManagementObjectCollection moc = searcher.GetInstances();
                foreach (var o in moc)
                {
                    var mo = (ManagementObject) o;
                    cpu = mo["Name"].ToString().Trim();
                    bit = mo["AddressWidth"].ToString().Trim() + "Bit";
                }
            }
            catch (Exception)
            { }

            try  //显卡， 支持多显卡
            {
                var searcher = new ManagementClass("Win32_VideoController");
                ManagementObjectCollection moc = searcher.GetInstances();
                foreach (var o in moc)
                {
                    var mo = (ManagementObject) o;
                    video += (mo["Name"].ToString().Trim()) + "\n";
                }
            }
            catch { }

            try  //系统版本
            {
                //ManagementClass searcher = new ManagementClass("Win32_OperatingSystem");
                //ManagementObjectCollection moc = searcher.GetInstances();
                //foreach (ManagementObject mo in moc)
                //{
                //    system += (mo["Name"].ToString().Trim()) + "\n";
                //    system += (mo["CSDVersion"].ToString().Trim()) + "\n";
                //    system += (mo["Version"].ToString().Trim()) + "\n";
                //}
                system = Environment.OSVersion.VersionString;
            }
            catch { }
        }
    }
}
