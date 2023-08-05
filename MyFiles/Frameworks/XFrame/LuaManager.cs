using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using UnityEngine;
using UnityEngine.Networking;
using XLua;
using XLua.LuaDLL;

[DefaultExecutionOrder(1)]
public class LuaManager : MonoBehaviour
{
    public static readonly LuaEnv LuaEnv = new LuaEnv();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

#if UNITY_EDITOR
        LuaEnv.AddLoader(CustomLoader);
#else
        LuaEnv.AddLoader(CustomLoader2);
#endif
        LuaEnv.DoString("require('main')", "main");
    }

    private byte[] CustomLoader(ref string filepath)
    {
        filepath = Application.dataPath + "/XFrame/LuaFiles/" + filepath.Replace('.', '/') + ".lua";

        if (File.Exists(filepath))
        {
            return File.ReadAllBytes(filepath);
        }
        else
        {
            return null;
        }
    }

    private byte[] CustomLoader2(ref string filepath)
    {
        filepath = "LuaFiles/" + filepath.Replace('.', '/') + ".lua";

        TextAsset file = GetLuaTextAsset(filepath);

        if (file != null)
        {
            return file.bytes;
        }
        else
        {
            return null;
        }
    }
    
    public static GameObject GetPanel(string name)
    {
        return GetAsset<GameObject>(name, "Prefabs/Panel");
    }

    public static AudioClip GetAudioClip(string name)
    {
        return GetAsset<AudioClip>(name, "Audios");
    }

    public static TextAsset GetLuaTextAsset(string name)
    {
        return GetAsset<TextAsset>(name, "LuaFiles");
    }

    public static T GetAsset<T>(string name, string prefix = "") where T : UnityEngine.Object
    {
        if (!name.Contains("/"))
        {
            name = $"{prefix}/{name}";
        }

        return Resources.Load<T>(name);
    }
    
     public static LuaTable GetLua(GameObject go, LuaTable baseClass
    )
    {
         LuaMono[] behaviourLuas = go.GetComponents<LuaMono>();

        foreach (var behaviour in behaviourLuas)
        {
            if (behaviour.LuaClass.GetHashCode() == baseClass.GetHashCode())
            {
                return behaviour.TableIns;
            }
        }

        return null;
    }

    private static Dictionary<string, List<LuaArg>> VerifyArgsDic = new Dictionary<string, List<LuaArg>>();

    public static object[] GetAllChange(List<LuaArg> args, string luaClassName)
    {
        if (!VerifyArgsDic.ContainsKey(luaClassName))
        {
            VerifyArgsDic.Add(luaClassName, args);
        }
        else
        {
            var tempArgs = VerifyArgsDic[luaClassName];

            if (!VerifyArgsRight(tempArgs, args))
            {
                throw new Exception($"LuaTable {luaClassName} 's ctor args disunion.");
            }
        }


        List<object> objs = new List<object>();

        foreach (var luaArg in args)
        {
            switch (luaArg.ArgType)
            {
                case ArgTypes.Int:
                    objs.Add(ChangeInt(luaArg.ArgValue));
                    break;
                case ArgTypes.Float:
                    objs.Add(ChangeFloat(luaArg.ArgValue));
                    break;
                case ArgTypes.String:
                    objs.Add(ChangeString(luaArg.ArgValue));
                    break;
                case ArgTypes.Boolean:
                    objs.Add(ChangeBoolean(luaArg.ArgValue));
                    break;
                case ArgTypes.LuaTable:
                    objs.Add(ChangeTable(luaArg.ArgValue));
                    break;
                case ArgTypes.LuaFunction:
                    objs.Add(ChangeFunction(luaArg.ArgValue));
                    break;
                case ArgTypes.GameObject:
                    objs.Add(ChangeGameObject(luaArg.ArgValue));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return objs.ToArray();
    }

    private static bool VerifyArgsRight(List<LuaArg> cacheArgs, List<LuaArg> newArgs)
    {
        bool bo1 = (cacheArgs.Count == newArgs.Count);

        if (bo1 == false)
        {
            return false;
        }

        int count = cacheArgs.Count;
        for (var index = 0; index < count; index++)
        {
            var cacheArg = cacheArgs[index];
            var newArg = newArgs[index];

            if (cacheArg.ArgType != newArg.ArgType)
            {
                return false;
            }
        }

        return true;
    }

    public static int ChangeInt(string val)
    {
        return Convert.ToInt32(val);
    }

    public static float ChangeFloat(string val)
    {
        return Convert.ToSingle(val);
    }

    public static string ChangeString(string val)
    {
        return Convert.ToString(val);
    }

    public static bool ChangeBoolean(string val)
    {
        return Convert.ToBoolean(val);
    }

    public static LuaTable ChangeTable(string val)
    {
        throw new Exception("Wait realize.");
    }

    public static LuaFunction ChangeFunction(string val)
    {
        throw new Exception("Wait realize.");
    }

    public static GameObject ChangeGameObject(string val)
    {
        return GameObject.Find(val);
    }
}

public enum ArgTypes
{
    Float,
    Int,
    Boolean,
    String,
    GameObject,
    LuaTable,
    LuaFunction,
}

[System.Serializable]
public class LuaArg
{
    public ArgTypes ArgType;
    public string ArgValue;
}