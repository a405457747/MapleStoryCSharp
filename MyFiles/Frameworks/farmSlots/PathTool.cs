/*⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵
☠ ©2020 Chengdu Mighty Vertex Games. All rights reserved.                                                                        
⚓ Author: SkyAllen                                                                                                                  
⚓ Email: 894982165@qq.com                                                                                  
⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵*/

using System.IO;
using QFramework;
using UnityEditor;
using UnityEngine;

public class PathTool
{
    [MenuItem("Framework/PathTool/OpenPersistentDataPath")]
    public static void OpenPersistentDataPath()
    {
        var path = Application.persistentDataPath;

        Application.OpenURL(path);
    }

    [MenuItem("Framework/PathTool/OpenStreamingAssetsPath")]
    public static void OpenStreamingAssetsPath()
    {
        var path = Application.streamingAssetsPath;

        Application.OpenURL(path);
    }

    [MenuItem("Assets/PathTool/OpenMyOther &H")]
    public static void OpenMyOther()
    {
        Application.OpenURL(EditorGame.GetMyOtherPath());
    }

    [MenuItem("Framework/PathTool/OpenMyHFS")]
    public static void OpenMyHFS()
    {
        Application.OpenURL(EditorGame.GetHFS());
    }


    [MenuItem("Framework/PathTool/ClearABFilePathByThreePlaces")]
    public static void ClearABFilePathByThreePlaces()
    {
        PlayerPrefs.DeleteAll();

        var p1 = Application.streamingAssetsPath;
        var p2 = Application.persistentDataPath;
        var p3 = EditorGame.GetHFS();

        p1.DeleteDirIfExists();
        p2.DeleteDirIfExists();
        p3.DeleteDirIfExists();

        p1.CreateDirIfNotExists();
        p2.CreateDirIfNotExists();
        p3.CreateDirIfNotExists();

        EditorGame.Refresh();

        Log.LogPrint("clear over");
    }
}