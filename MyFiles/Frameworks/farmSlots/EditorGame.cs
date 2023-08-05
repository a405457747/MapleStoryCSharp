using System.IO;
using UnityEditor;
using UnityEngine;

public class EditorGame
{
    public const string format = "\r\n\t";

    public static string GetAssetsPathAbsolute(string willPath)
    {
        var tempPath = willPath.Replace("Assets/", "");
        return Path.Combine(Application.dataPath, tempPath);
    }

    public static string GetHFS_A()
    {
        return Path.Combine(GetHFS(), HotUpdateSystem.GetA());
    }

    public static string GetHFS()
    {
        return Path.Combine(EditorGame.GetMyOtherPath(), "HFS");
    }

    public static string GetMyOtherVersionPath()
    {
        return GetMyOtherPath() + "/version.txt";
    }

    public static string GetMyOtherPath()
    {
        var p = Path.Combine(GetAssetsPathPrev(), "MyOther");
        return p;
    }

    public static string GetAssetsPathPrev()
    {
        return Application.dataPath + "/..";
    }

    public static string GetAssetsPath(string name)
    {
        return Path.Combine(Application.dataPath, name);
    }

    public static void Refresh()
    {
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static void CopyToBoard(string content)
    {
        var editor = new TextEditor();
        editor.text = content;
        editor.SelectAll();
        editor.Copy();
    }
}