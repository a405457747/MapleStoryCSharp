using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System;

public static class CreateFile
{
    [MenuItem("Framework/CreateFile/CreateTxt")]
    static void CreateTxt()
    {
        CrateFile(".txt");
    }

    static void CrateFile(string fileEx, string fileName = "new", string fileContain = "")
    {
        var selectFolderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        var fileFullName = $"{fileName}{fileEx}";

        var realFolderPath = EditorBasic.GetRealPath(selectFolderPath);
        var writePath = $"{Path.Combine(realFolderPath, fileFullName)}";

        UTF8Encoding utf8 = new UTF8Encoding(false);
        File.WriteAllText(writePath, fileContain, utf8);
        AssetDatabase.Refresh();

        var selectFilePath = Path.Combine(selectFolderPath, fileFullName);
        Selection.activeObject = AssetDatabase.LoadAssetAtPath(selectFilePath, typeof(UnityEngine.Object));
    }

}