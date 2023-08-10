using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace MapleStory
{
    public static class IOHelper
    {
        public static void CreateFileByStream(string path, string content)
        {
            if (File.Exists(path)) File.Delete(path);

            var file = new FileStream(path, FileMode.CreateNew);
            var fileW = new StreamWriter(file, Encoding.UTF8);
            fileW.Write(content);
            fileW.Flush();
            fileW.Close();
            file.Close();
        }

        public static void CreateDir(string path)
        {
            if (Directory.Exists(path))
            {
                return;
            }

            Directory.CreateDirectory(path);
        }

        public static void FileChangeName(string sourceFile, string newName)
        {
            var fileInfo = new FileInfo(sourceFile);
            if (fileInfo.Exists == false) return;

            var folderName = fileInfo.DirectoryName;
            var p1 = Path.GetExtension(sourceFile);
            var p3 = $"{newName}{p1}";
            var p4 = Path.Combine(folderName, p3);

            File.Move(fileInfo.FullName, p4);
        }

        public static void DirectoryCopy(string sourceDirPath, string saveDirPath, string suffix = "")
        {
            if (!Directory.Exists(saveDirPath))
                Directory.CreateDirectory(saveDirPath);

            var files = Directory.GetFiles(sourceDirPath);

            foreach (var file in files)
            {
                if (file.Contains(".meta")) continue;
                var pFilePath = saveDirPath + "\\" + Path.GetFileName(file) + suffix;
                File.Copy(file, pFilePath, true);
            }

            var dirs = Directory.GetDirectories(sourceDirPath);

            foreach (var dir in dirs) DirectoryCopy(dir, saveDirPath + "\\" + Path.GetFileName(dir), suffix);
        }

        public static long GetDirectorySize(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                return 0;
            long len = 0;
            var di = new DirectoryInfo(dirPath);

            foreach (var item in di.GetFiles()) len += item.Length;

            var dis = di.GetDirectories();
            if (dis.Length > 0)
                for (var i = 0; i < dis.Length; i++)
                    len += GetDirectorySize(dis[i].FullName);

            return len;
        }

        public static T DeepCopy<T>(T originObj) where T : class
        {
            using (var ms = new MemoryStream())
            {
                var fm = new BinaryFormatter();
                fm.Serialize(ms, originObj);
                ms.Seek(0, SeekOrigin.Begin);
                return (T)fm.Deserialize(ms);
            }
        }
    }
}