using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System;

public class CreateFile : Editor
{
    [MenuItem("Assets/CreateLua", false, 1)]
    static void CreateLua()
    {
        string head = @"
-------------------------------------------------------                                                             
-- author : sky_allen                                                                                                                  
--  email : 894982165@qq.com      
--   time : $time$                                                                                           
-------------------------------------------------------

---@class $table$
local $table$ = class('$table$')

function $table$:ctor()
    
end

return $table$
";
        CrateFile(".lua", fileContain: head);
    }

    static void CrateFile(string fileEx, string fileName = "new", string fileContain = "")
    {
        var selectFolderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        var realFolderPath = GetRealPath(selectFolderPath);
        var fileFullName = $"{fileName}{fileEx}";
        var writePath = $"{Path.Combine(realFolderPath, fileFullName)}";

        File.WriteAllText(writePath, fileContain, new UTF8Encoding(false));

        AssetDatabase.Refresh();

        Selection.activeObject =
            AssetDatabase.LoadAssetAtPath(Path.Combine(selectFolderPath, fileFullName), typeof(UnityEngine.Object));
    }

    static string GetRealPath(string willPath)
    {
        var tempPath = willPath.Replace("Assets/", "");
        return Path.Combine(Application.dataPath, tempPath);
    }

    [MenuItem("Assets/RenameLuaAndCopyPath", false, 2)]
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

            File.WriteAllText(realPath, content, new UTF8Encoding(false));

            AssetDatabase.Refresh();
        }


        string copyPath = "";
        for (int i = 0; i < tempStrArray.Length; i++)
        {
            if ((i == 0) || (i == 1) || (i == 2) || (i == tempStrArray.Length - 1))
            {
                continue;
            }

            copyPath += tempStrArray[i] + "/";
        }

        copyPath = Path.Combine(copyPath, fileName);
        CopyText(copyPath);
    }

    public static void CopyText(string msg)
    {
        GUIUtility.systemCopyBuffer = msg;
    }
}