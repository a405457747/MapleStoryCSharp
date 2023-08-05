using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public static class IOHelper
{

        /// <summary>
        /// 创建新的文件夹,如果存在则不创建
        /// <code>
        /// var testDir = "Assets/TestFolder";
        /// testDir.CreateDirIfNotExists();
        /// // 结果为，在 Assets 目录下创建 TestFolder
        /// </code>
        /// </summary>
        public static string CreateDirIfNotExists(this string dirFullPath)
        {
            if (!Directory.Exists(dirFullPath))
            {
                Directory.CreateDirectory(dirFullPath);
            }

            return dirFullPath;
        }

        /// <summary>
        /// 删除文件夹，如果存在
        /// <code>
        /// var testDir = "Assets/TestFolder";
        /// testDir.DeleteDirIfExists();
        /// // 结果为，在 Assets 目录下删除了 TestFolder
        /// </code>
        /// </summary>
        public static void DeleteDirIfExists(this string dirFullPath)
        {
            if (Directory.Exists(dirFullPath))
            {
                Directory.Delete(dirFullPath, true);
            }
        }

    public static void CreateText(string path, string content)
    {
        File.WriteAllText(path, content, new UTF8Encoding(false));
    }

    public static string GetFileName(string filePath)
    {
        FileInfo f = new FileInfo(filePath);
        return f.Name.Replace(f.Extension, "");
    }

    public static List<string> GetAllFilePaths(string dirPath, string mode = "*")
    {
        List<string> res = new List<string>();

        DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);

        var fs = directoryInfo.GetFiles(mode);

        foreach (var f in fs)
        {
            res.Add(f.FullName);
        }

        return res;
    }

    public static void CreateFileByStream(string path, string content)
    {
        if (File.Exists(path)) File.Delete(path);

        var utf8WithoutBom = new System.Text.UTF8Encoding(false);

        var file = new FileStream(path, FileMode.CreateNew);
        var fileW = new StreamWriter(file, utf8WithoutBom);
        fileW.Write(content);
        fileW.Flush();
        fileW.Close();
        file.Close();
    }

    public static void CreateDir(string path)
    {
        if (Directory.Exists(path))
        {
            Log.LogWarning(" have the dir");
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

    public static void CreateFileByStream2(string filePath, byte[] bytes)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        FileInfo file = new FileInfo(filePath);
        Stream stream = file.Create();
        stream.Write(bytes, 0, bytes.Length);

        //stream.Flush();

        stream.Close();
        stream.Dispose();
        Log.LogParas("create success");
    }

    /*public static void DerectoryFilter(string targetPath, ref List<string> fileList)
    {
        var files = Directory.GetFiles(targetPath, "*");
        foreach (var file in files)
        {
            if ((new Regex(".meta")).IsMatch(file))
            {
                fileList.Add(file);
            }
        }
        var dirs = Directory.GetDirectories(targetPath);
        //递归，遍历文件夹
        foreach (var dir in dirs) DerectoryFilter(dir, targetPath + "\\" + Path.GetFileName(dir), suffix);
    }*/

    public static void RemoveBoomFileOne(string path)
    {
        string content = File.ReadAllText(path);
        if (File.Exists(path)) File.Delete(path);
        var utf8WithoutBom = new System.Text.UTF8Encoding(false);

        var file = new FileStream(path, FileMode.CreateNew);
        var fileW = new StreamWriter(file, utf8WithoutBom);
        fileW.Write(content);
        fileW.Flush();
        fileW.Close();
        file.Close();
    }

    public static void RemoveBoomFiles(string sourceDirPath)
    {
        var files = Directory.GetFiles(sourceDirPath, "*");
        foreach (var file in files)
        {
            if (file.EndsWith(".meta"))
            {
                continue;
            }

           // Debug.Log("file is :" + file);
              RemoveBoomFileOne(file);
        }

        var dirs = Directory.GetDirectories(sourceDirPath);
        //递归，遍历文件夹
        foreach (var dir in dirs)
        {
        //    Debug.Log("dir is :" + dir);
            RemoveBoomFiles(dir);
        }
    }

    public static void DirectoryCopy(string sourceDirPath, string saveDirPath, string suffix = "")
    {
        //如果指定的存储路径不存在，则创建该存储路径
        if (!Directory.Exists(saveDirPath))
            //创建
            Directory.CreateDirectory(saveDirPath);
        //获取源路径文件的名称
        var files = Directory.GetFiles(sourceDirPath, "*");
        //遍历子文件夹的所有文件
        foreach (var file in files)
        {
            if (file.EndsWith(".meta"))
            {
                continue;
            }

            /*if (delMeta == true && (new Regex(".meta")).IsMatch(file))
            {
                continue;
            }*/

            var pFilePath = saveDirPath + "\\" + Path.GetFileName(file) + suffix;
            File.Copy(file, pFilePath, true);
        }

        var dirs = Directory.GetDirectories(sourceDirPath);
        //递归，遍历文件夹
        foreach (var dir in dirs) DirectoryCopy(dir, saveDirPath + "\\" + Path.GetFileName(dir), suffix);
    }

    public static void DirectoryCopy2(
        string sourceDirName, string destDirName, bool copySubDirs, bool isOverwrite = true)
    {
        //Log.LogParas(sourceDirName);
        DirectoryInfo dir = new DirectoryInfo(sourceDirName);
        DirectoryInfo[] dirs = dir.GetDirectories();

        // If the source directory does not exist, throw an exception.
        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException(
                "Source directory does not exist or could not be found: "
                + sourceDirName);
        }

        //Log.LogParas(destDirName);
        // If the destination directory does not exist, create it.
        if (!Directory.Exists(destDirName))
        {
            Directory.CreateDirectory(destDirName);
        }

        // Get the file contents of the directory to copy.
        FileInfo[] files = dir.GetFiles();

        foreach (FileInfo file in files)
        {
            // Create the path to the new copy of the file.
            string temppath = Path.Combine(destDirName, file.Name);

            // Copy the file.
            file.CopyTo(temppath, isOverwrite);
            //Log.LogParas(temppath);
        }

        // If copySubDirs is true, copy the subdirectories.
        if (copySubDirs)
        {
            foreach (DirectoryInfo subdir in dirs)
            {
                // Create the subdirectory.
                string temppath = Path.Combine(destDirName, subdir.Name);
                //Log.LogParas(temppath);
                // Copy the subdirectories.
                DirectoryCopy2(subdir.FullName, temppath, copySubDirs);
            }
        }
    }

    public static long GetDirectorySize(string dirPath)
    {
        if (!Directory.Exists(dirPath))
            return 0;
        long len = 0;
        var di = new DirectoryInfo(dirPath);
        //获取di目录中所有文件的大小
        foreach (var item in di.GetFiles()) len += item.Length;
        //获取di目录中所有的文件夹,并保存到一个数组中,以进行递归
        var dis = di.GetDirectories();
        if (dis.Length > 0)
            for (var i = 0; i < dis.Length; i++)
                len += GetDirectorySize(dis[i].FullName); //递归dis.Length个文件夹,得到每隔dis[i]
        return len;
    }

    public static T DeepCopy<T>(T originObj) where T : class
    {
        using (var ms = new MemoryStream())
        {
            var fm = new BinaryFormatter();
            fm.Serialize(ms, originObj);
            ms.Seek(0, SeekOrigin.Begin); //就是从开始地方，移动指针
            return (T) fm.Deserialize(ms);
        }
    }
}