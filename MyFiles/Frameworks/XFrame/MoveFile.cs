using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MoveFile
{
    [MenuItem("XFrame/MoveLuaFiles")]
    public static void MoveLuaFiles()
    {
        var luaPath = Application.dataPath + "/XFrame/LuaFiles";
        var resPath = Application.dataPath + "/XFrame/Resources/LuaFiles";

        Directory.Delete(resPath,true);
        
        if (Directory.Exists(resPath)==false)
        {
            Directory.CreateDirectory(resPath);
        }

        DirectoryCopy(luaPath, resPath, ".txt");
        AssetDatabase.Refresh();
        Debug.Log("执行完毕");
    }

    private static void DirectoryCopy(string sourceDirPath, string saveDirPath, string suffix = "")
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
            if (file.Contains(".meta"))
            {
                continue;
            }

            var pFilePath = saveDirPath + "\\" + Path.GetFileName(file) + suffix;
            File.Copy(file, pFilePath, true);
        }

        var dirs = Directory.GetDirectories(sourceDirPath);
        //递归，遍历文件夹
        foreach (var dir in dirs) DirectoryCopy(dir, saveDirPath + "\\" + Path.GetFileName(dir), suffix);
    }

}