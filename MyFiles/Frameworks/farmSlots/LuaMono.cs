using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using XLua;
using XLua.LuaDLL;

//[DefaultExecutionOrder(2)]
public class LuaMono : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler,IPointerExitHandler
{
    public string FilePath;
    public List<LuaArg> Args;
    public bool Singleton = false;

    public LuaTable LuaClass { get; private set; }
    public LuaTable TableIns { get; private set; }

    private Action<LuaTable> _luaAwake;
    private Action<LuaTable> _luaStart;
    private Action<LuaTable> _luaOnEnable;
    private Action<LuaTable> _luaUpdate;
    private Action<LuaTable> _luaFixedUpdate;
    private Action<LuaTable> _luaOnDestroy;

    private Action<LuaTable, Collision2D> _luaOnCollisionEnter2D;
    private Action<LuaTable, Collision2D> _luaOnCollisionStay2D;
    private Action<LuaTable, Collision2D> _luaOnCollisionExit2D;

    private Action<LuaTable, PointerEventData> _luaClickHandler;
    private Action<LuaTable, PointerEventData> _luaDownHandler;
    private Action<LuaTable, PointerEventData> _luaUpHandler;
    private Action<LuaTable, PointerEventData> _luaExitHandler;

    private void Awake()
    {
        var luaEnv = LuaSystem.LuaEnv;

        if (FilePath == default)
        {
            FilePath = "root";
        }

        if (Args == default)
        {
            Args = new List<LuaArg>();
        }

        var requirePath = FilePath.Replace('/', '.');
        string className = requirePath.Split('.').Last();

        object[] objs = luaEnv.DoString($" return  require ('{requirePath}')", className);
        LuaClass = objs[0] as LuaTable;

        if (LuaClass == null)
        {
            throw new Exception("LuaClass dont't find.");
        }

        LuaFunction newFunc = LuaClass.Get<LuaFunction>("new");
        TableIns = (newFunc.Call(LuaSystem.GetAllChange(Args, className))[0]) as LuaTable;

        if (Singleton)
        {
            LuaClass.Set<string, LuaTable>("ins", TableIns);
        }

        TableIns.Set<string, MonoBehaviour>("this", this);
        TableIns.Set<string, GameObject>("gameObject", gameObject);
        TableIns.Set<string, Transform>("transform", transform);

        LuaClass.Get("Awake", out _luaAwake);
        _luaAwake?.Invoke(TableIns);

        LuaClass.Get("Start", out _luaStart);
        LuaClass.Get("OnEnable", out _luaOnEnable);
        LuaClass.Get("Update", out _luaUpdate);
        LuaClass.Get("FixedUpdate", out _luaFixedUpdate);
        LuaClass.Get("OnDestroy", out _luaOnDestroy);

        LuaClass.Get("OnCollisionEnter2D", out _luaOnCollisionEnter2D);
        LuaClass.Get("OnCollisionStay2D", out _luaOnCollisionStay2D);
        LuaClass.Get("OnCollisionExit2D", out _luaOnCollisionExit2D);

        LuaClass.Get("OnPointerClick", out _luaClickHandler);
        LuaClass.Get("OnPointerDown", out _luaDownHandler);
        LuaClass.Get("OnPointerUp", out _luaUpHandler);
        LuaClass.Get("OnPointerExit", out _luaExitHandler);
    }

    void Start()
    {
        if (_luaStart != null)
        {
            _luaStart(TableIns);
        }
    }

    void OnEnable()
    {
        if (_luaOnEnable != null)
        {
            _luaOnEnable(TableIns);
        }
    }


    void Update()
    {
        if (_luaUpdate != null)
        {
            _luaUpdate(TableIns);
        }

    }

    void FixedUpdate()
    {
        if (_luaFixedUpdate != null)
        {
            _luaFixedUpdate(TableIns);
        }
    }

    void OnDestroy()
    {
        if (_luaOnDestroy != null)
        {
            _luaOnDestroy(TableIns);
        }
        
        LuaClass.Dispose();
        TableIns.Dispose();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_luaOnCollisionEnter2D != null) _luaOnCollisionEnter2D(TableIns, other);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (_luaOnCollisionStay2D != null) _luaOnCollisionStay2D(TableIns, other);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (_luaOnCollisionExit2D != null) _luaOnCollisionExit2D(TableIns, other);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_luaClickHandler != null)
        {
            _luaClickHandler(TableIns, eventData);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_luaDownHandler != null)
        {
            _luaDownHandler(TableIns, eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_luaUpHandler != null)
        {
            _luaUpHandler(TableIns, eventData);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_luaExitHandler != null)
        {
            _luaExitHandler(TableIns, eventData);
        }
    }
}