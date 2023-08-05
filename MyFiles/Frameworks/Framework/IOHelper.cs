using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class IOHelper 
{
    public static void DirectoryCopy(string sourceDirPath, string saveDirPath, string suffix = "")
    {
        //如果指定的存储路径不存在，则创建该存储路径
        if (!Directory.Exists(saveDirPath))
            //创建
            Directory.CreateDirectory(saveDirPath);
        //获取源路径文件的名称
        var files = Directory.GetFiles(sourceDirPath);
        //遍历子文件夹的所有文件
        foreach (var file in files)
        {
            if (file.Contains(".meta")) continue;
            var pFilePath = saveDirPath + "\\" + Path.GetFileName(file) + suffix;
            File.Copy(file, pFilePath, true);
        }

        var dirs = Directory.GetDirectories(sourceDirPath);
        //递归，遍历文件夹
        foreach (var dir in dirs) DirectoryCopy(dir, saveDirPath + "\\" + Path.GetFileName(dir), suffix);
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