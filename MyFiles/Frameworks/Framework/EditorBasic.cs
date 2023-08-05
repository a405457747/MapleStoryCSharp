using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class EditorBasic
{
    public static string GetRealPath(string willPath)
    {
        var tempPath = willPath.Replace("Assets/", "");
        return Path.Combine(Application.dataPath, tempPath);
    }

    public static void CopyToBoard(string content)
    {
        TextEditor editor = new TextEditor();
        editor.text = content;
        editor.SelectAll();
        editor.Copy();
    }
}
