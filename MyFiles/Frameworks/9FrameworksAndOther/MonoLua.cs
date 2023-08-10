using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XLua;


[DefaultExecutionOrder(2)]
public class MonoLua : MonoBehaviour
{
    public string FilePath;
    private string className;
    public List<LuaArg> Args;
    [HideInInspector] public LuaTable LuaClass;
    [HideInInspector] public LuaTable tableIns;

    private Action<LuaTable> luaAwake;
    private Action<LuaTable> luaStart;
    private Action<LuaTable> luaUpdate;
    private Action<LuaTable> luaOnDestroy;

    private Action<LuaTable, Collision2D> _luaOnCollisionEnter2D;
    private Action<LuaTable, Collision2D> _luaOnCollisionStay2D;
    private Action<LuaTable, Collision2D> _luaOnCollisionExit2D;

    private void Awake()
    {
        var luaEnv = LuaManager.LuaEnv;

        var requirePath = FilePath.Replace('/', '.');
        className = requirePath.Split('.').Last();

        luaEnv.DoString($"require ('{requirePath}')", className);

        LuaClass = luaEnv.Global.Get<LuaTable>(className);
        if (LuaClass == null)
        {
            throw new Exception("LuaClass dont't find.");
        }

        LuaFunction newFunc = LuaClass.Get<LuaFunction>("new");
        tableIns = (newFunc.Call(LuaHelp.GetAllChange(Args, className))[0]) as LuaTable;

        tableIns.Set<string, MonoBehaviour>("this", this);
        tableIns.Set<string, GameObject>("gameObject", this.gameObject);
        tableIns.Set<string, Transform>("transform", this.transform);

        LuaClass.Get("Awake", out luaAwake);
        luaAwake?.Invoke(tableIns);

        LuaClass.Get("Start", out luaStart);
        LuaClass.Get("Update", out luaUpdate);
        LuaClass.Get("OnDestroy", out luaOnDestroy);

        LuaClass.Get("OnCollisionEnter2D", out _luaOnCollisionEnter2D);
        LuaClass.Get("OnCollisionStay2D", out _luaOnCollisionStay2D);
        LuaClass.Get("OnCollisionExit2D", out _luaOnCollisionExit2D);
    }

    void Start()
    {
        if (luaStart != null)
        {
            luaStart(tableIns);
        }
    }

    void Update()
    {
        if (luaUpdate != null)
        {
            luaUpdate(tableIns);
        }
    }

    void OnDestroy()
    {
        if (luaOnDestroy != null)
        {
            luaOnDestroy(tableIns);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_luaOnCollisionEnter2D != null) _luaOnCollisionEnter2D(tableIns, other);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (_luaOnCollisionStay2D != null) _luaOnCollisionStay2D(tableIns, other);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (_luaOnCollisionExit2D != null) _luaOnCollisionExit2D(tableIns, other);
    }
}