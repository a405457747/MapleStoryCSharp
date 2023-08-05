using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class IOHelpLua
{
    private static string targetDir = "Game/Resources/Text";

    public static string GetFullPath()
    {
        return Path.Combine(Application.dataPath, targetDir);
    }

    public static string GetLVFullPath(int lv)
    {
        string str = $"Lv{lv}";
        return Path.Combine(GetFullPath(), str);
    }

    public static void CreateLevelDatas(int lv, string content)
    {
        Log.LogParas("开始写入");
        string path = Path.Combine(GetLVFullPath(lv), "datas.txt");
        IOHelper.CreateText(path, content);
        Log.LogParas("写入完成了");
    }
}