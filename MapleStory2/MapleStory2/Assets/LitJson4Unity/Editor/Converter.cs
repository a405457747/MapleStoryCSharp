using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using LitJson;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

public class Converter
{
    private static readonly string JsonPath = "Assets/LitJson4Unity/TrasnferData/TestScriptableObj.json";
    private static readonly string ScriptableObjectPath = "Assets/LitJson4Unity/TestScriptableObj.asset";

    private static readonly string TransScritptableObjectPath =
        "Assets/LitJson4Unity/TrasnferData/TestScriptableObj.asset";

    [MenuItem("ColaFramework/序列化为Json")]
    public static void Trans2Json()
    {
        var asset = AssetDatabase.LoadAssetAtPath<TestScriptableObj>(ScriptableObjectPath);
        var jsonContent = JsonMapper.ToJson(asset);
        using (var stream = new StreamWriter(JsonPath))
        {
            stream.Write(jsonContent);
        }

        AssetDatabase.Refresh();
    }

    public static void NewTrans2Json<T>(string ScriptableObjectPath, string JsonPath) where T : UnityEngine.Object
    {
        var asset = AssetDatabase.LoadAssetAtPath<T>(ScriptableObjectPath);
        var jsonContent = JsonMapper.ToJson(asset);
        using (var stream = new StreamWriter(JsonPath))
        {
            stream.Write(jsonContent);
        }

        AssetDatabase.Refresh();
    }

    [MenuItem("ColaFramework/所有序列化为Json")]
    static void DirTrans2Json()
    {
        //这里*.cs和*.asset是放在一起的哦
        var configDir = "Assets/GameApp/Scripts/Config";
        var jsonDir = "Assets/GameApp/Resources/Text";
        string[] aFiles = Directory.GetFiles(configDir, "*.asset");
        foreach (var f in aFiles)
        {
            var cPath = Path.Combine(configDir, Path.GetFileName(f).Replace(".asset", ".cs"));
            var sPath = Path.Combine(configDir, Path.GetFileName(f));
            var jPath = Path.Combine(jsonDir, Path.GetFileName(f).Replace(".asset", ".txt"));

            //Debug.LogFormat("{0}:{1}", File.Exists(sPath), sPath);
            var t = GetTypeByPath(cPath,"MapleStory2");
            //Debug.Log(t);

            System.Type converterType = typeof(Converter);
            MethodInfo method = converterType.GetMethod("NewTrans2Json");
            MethodInfo gm = method.MakeGenericMethod(t);
            gm.Invoke(null, new object[] { sPath, jPath });
        }


        Debug.Log("DirTrans2Json Success");
    }

    static System.Type GetTypeByPath(string filePath,string nameSpace="")
    {
        string code = File.ReadAllText(filePath);
        string classNamePattern = @"class\s+(\w+)";

        Match match = Regex.Match(code, classNamePattern);
        if (match.Success)
        {
            string className = match.Groups[1].Value;
//            Debug.Log("className"+":"+className);
            Assembly assembly = Assembly.Load("Assembly-CSharp");
            //Debug.Log("ass"+":"+assembly);
            System.Type t = assembly.GetType($"{nameSpace}.{className}");
            //Debug.Log("t"+":"+t);
            return t;
        }
        else
        {
            throw new Exception("Match Fail");
        }
    }

    [MenuItem("ColaFramework/反序列化为ScriptableObject")]
    public static void Trans2ScriptableObject()
    {
        if (!File.Exists(JsonPath)) return;
        using (var stream = new StreamReader(JsonPath))
        {
            var jsonStr = stream.ReadToEnd();
            var striptableObj = JsonMapper.ToObject<TestScriptableObj>(jsonStr);
            AssetDatabase.CreateAsset(striptableObj, TransScritptableObjectPath);
            AssetDatabase.Refresh();
        }
    }
}