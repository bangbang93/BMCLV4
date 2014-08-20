using System;
using System.IO;
using System.Text;

namespace BMCLV4.Util
{
    class Md5
    {
        public static string GetMd5HashFromFile(string fileName)
        {
            try
            {
                var file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                var sb = new StringBuilder();
                foreach (byte t in retVal)
                {
                    sb.Append(t.ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return ("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }
    }
}
