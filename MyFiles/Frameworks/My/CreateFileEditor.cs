using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


/// <summary>
///     Unity Editor 下右键创建文本类文件
/// </summary>
public class CreateFileEditor : Editor
{
    [MenuItem("Assets/Create/xLua File", false, 1)]
    private static void CreatexLuaFile()
    {
        string first = $"--sky_allen wrote it in {DateTime.Now.ToString()}\r\n\r\n";
        string second = @"require('eventCenter');
local cs_coroutine = (require 'cs_coroutine')

local UnityEngine = CS.UnityEngine;
local GameObject = UnityEngine.GameObject;
local Vector2 = UnityEngine.Vector2;
local Vector3 = UnityEngine.Vector3;
local Quaternion = UnityEngine.Quaternion;
local Time = UnityEngine.Time;
local Mathf = UnityEngine.Mathf;

local gameObject = mono.gameObject;
local transform = mono.transform;
local rotation = transform.rotation;
local this = CS.LuaMonoHelper.GetCom(gameObject, PrevName);

function Start()

end

function Update()

end

function OnDestroy()

end";
        string contain = first + second;
        CreateFile("lua.txt", false, "n",
            contain);
    }

    [MenuItem("Assets/Create/Lua File")]
    private static void CreateLuaFile()
    {
        CreateFile("lua");
    }

    [MenuItem("Assets/Create/Text File")]
    private static void CreateTextFile()
    {
        CreateFile("txt");
    }

    [MenuItem("Assets/Create/Ini Config File")]
    private static void CreateIniFile()
    {
        //用于处理prefab的ini文件
        var fileContain = @"[Config]
HudName = HUDPoint
HudPos = 0,3.45,0";

        CreateFile("ini", false, "config", fileContain);
    }


    /// <summary>
    ///     创建文件类的文件
    /// </summary>
    /// <param name="fileEx"></param>
    private static void CreateFile(string fileEx, bool randomName = true, string fileName = "",
        string fileContain = "-- test")
    {
        //获取当前所选择的目录（相对于Assets的路径）
        var selectPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        var path = Application.dataPath.Replace("Assets", "") + "/";
        string newFileName;
        if (!randomName)
            newFileName = fileName + "." + fileEx;
        else
            newFileName = "new_" + fileEx + "." + fileEx;

        var newFilePath = selectPath + "/" + newFileName;
        var fullPath = path + newFilePath;

        //简单的重名处理
        if (File.Exists(fullPath))
        {
            var newName = "new_" + fileEx + "-" + Random.Range(0, 100) + "." + fileEx;
            newFilePath = selectPath + "/" + newName;
            fullPath = fullPath.Replace(newFileName, newName);
        }

        //如果是空白文件，编码并没有设成UTF-8
        File.WriteAllText(fullPath, fileContain, Encoding.UTF8);

        AssetDatabase.Refresh();

        //选中新创建的文件
        var asset = AssetDatabase.LoadAssetAtPath(newFilePath, typeof(Object));
        Selection.activeObject = asset;
    }
}