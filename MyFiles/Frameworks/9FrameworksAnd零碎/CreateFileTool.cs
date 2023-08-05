using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System;

public class CreateFileTool : Editor
{
    [MenuItem("Assets/CreateLua", false, -1)]
    static void CreateLua()
    {
        string head = @"
-------------------------------------------------------                                                             
-- author : sky_allen                                                                                                                  
--  email : 894982165@qq.com      
--   time : $time$                                                                                           
-------------------------------------------------------

---@class $table$
$table$ = class('$table$')

function $table$:Start()
    
end
";
        CrateFile(".lua", fileContain: head);
    }

    static void CrateFile(string fileEx, string fileName = "NEW", string fileContain = "")
    {
        var selectFolderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        var fileFullName = $"{fileName}{fileEx}";

        var realFolderPath = GetRealPath(selectFolderPath);
        var writePath = $"{Path.Combine(realFolderPath, fileFullName)}";

        File.WriteAllText(writePath, fileContain, Encoding.UTF8);
        AssetDatabase.Refresh();

        var selectFilePath = Path.Combine(selectFolderPath, fileFullName);
        Selection.activeObject = AssetDatabase.LoadAssetAtPath(selectFilePath, typeof(UnityEngine.Object));
    }

    static string GetRealPath(string willPath)
    {
        var tempPath = willPath.Replace("Assets/", "");
        return Path.Combine(Application.dataPath, tempPath);
    }

    [MenuItem("Assets/RenameLuaAndCopyPath", false, 0)]
    static void RenameLuaAndCopyPath()
    {
        var selectFolderPath = AssetDatabase.GetAssetPath(Selection.activeObject);

        var realPath = GetRealPath(selectFolderPath);

        StreamReader reader = new StreamReader(realPath, Encoding.UTF8);
        String content = reader.ReadToEnd();
        reader.Close();

        var tempStrArray = selectFolderPath.Split('/');
        var fileName = tempStrArray[tempStrArray.Length - 1].Split('.')[0];

        if (content.Contains("$table$"))
        {
            content = content.Replace("$table$", fileName);
            content = content.Replace("$time$", DateTime.Now.ToString());

            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(realPath, content, utf8);

            AssetDatabase.Refresh();
        }

        string copyPath = "";

        for (int i = 0; i < tempStrArray.Length; i++)
        {
            if ((i == 0) || (i == 1) || (i == tempStrArray.Length - 1))
            {
                continue;
            }

            copyPath += tempStrArray[i] + "/";
        }

        copyPath = Path.Combine(copyPath, fileName);
        CopyText(copyPath);
    }

    private static void CopyText(string msg)
    {
        GUIUtility.systemCopyBuffer = msg;
    }
}