using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class LuaHelp
{
    public static LuaTable GetIns(GameObject go, LuaTable baseClassName)
    {
        MonoLua[] behaviourLuas = go.GetComponents<MonoLua>();

        foreach (var behaviour in behaviourLuas)
        {
            if (behaviour.LuaClass.GetHashCode() == baseClassName.GetHashCode())
            {
                return behaviour.tableIns;
            }
        }

        return null;
    }

    public static void AllMonoLuaSetActive(bool active)
    {
        MonoLua[] behaviourLuas = Resources.FindObjectsOfTypeAll<MonoLua>();

        foreach (var monoLua in behaviourLuas)
        {
            monoLua.gameObject.SetActive(active);
        }
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
                case ArgTypes.SelfOtherTable:
                    objs.Add(ChangeTable(luaArg.ArgValue));
                    break;
                case ArgTypes.Function:
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
        return null;
    }

    public static LuaFunction ChangeFunction(string val)
    {
        return null;
    }

    public static GameObject ChangeGameObject(string val)
    {
        return null;
    }
}


public enum ArgTypes
{
    GameObject,
    SelfOtherTable,
    Int,
    Float,
    String,
    Boolean,
    Function,
}

[System.Serializable]
public class LuaArg
{
    public ArgTypes ArgType;
    public string ArgValue;
}

