using System.IO;

namespace BMCLV4.Util
{
    static class FileHelper
    {
        static public void Dircopy(string from, string to)
        {
            var dir = new DirectoryInfo(from);
            if (!Directory.Exists(to))
            {
                Directory.CreateDirectory(to);
            }
            foreach (DirectoryInfo sondir in dir.GetDirectories())
            {
                Dircopy(sondir.FullName, to + "\\" + sondir.Name);
            }
            foreach (FileInfo file in dir.GetFiles())
            {
                File.Copy(file.FullName, to + "\\" + file.Name, true);
            }
        }

        static public bool IfFileVaild(string path, long length = -1)
        {
            if (!File.Exists(path))
            {
                return false;
            }
            if (new FileInfo(path).Length == 0)
            {
                return false;
            }
            if (length != -1)
            {
                if (new FileInfo(path).Length != length)
                    return false;
            }
            return true;
        }

        static public void CreateDirectoryIfNotExist(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        static public void CreateDirectoryForFile(string file)
        {
            CreateDirectoryIfNotExist(Path.GetDirectoryName(file));
        }
        
    }
}
