using System;
using System.Collections;
using System.Collections.Generic;
using CallPalCatGames.Utility;
using QFramework;
using UnityEngine;
using UnityEngine.Serialization;
using XLua;
using Log = CallPalCatGames.Log.Log;

[LuaCallCSharp]
public class LuaMonoBehaviour : MonoBehaviour
{
    internal const float GCInterval = 1;
    internal static float lastGCTime;
    public List<GameObject> Injections = new List<GameObject>();

    [FormerlySerializedAs("LuaMonoBehaviourText")]
    public TextAsset luaMonoBehaviourText;

    public LuaTable LuaMonoBehaviourLua { get; private set; }

    private Action _luaAwake;
    private Action _luaStart;
    private Action _luaUpdate;
    private Action _luaOnDestroy;

    private Action _luaFixedUpdate;
    private Action _luaLateUpdate;
    private Action _luaOnDisable;
    private Action _luaOnEnable;

    private Action _luaOnMouseDown;
    private Action _luaOnMouseUp;
    private Action _luaOnMouseDrag;

    private Action<Collision2D> _luaOnCollisionEnter2D;
    private Action<Collision2D> _luaOnCollisionStay2D;
    private Action<Collision2D> _luaOnCollisionExit2D;

    private Action<Collider2D> _luaOnTriggerEnter2D;
    private Action<Collider2D> _luaOnTriggerStay2D;
    private Action<Collider2D> _luaOnTriggerExit2D;

    private Action<Collision> _luaOnCollisionEnter;
    private Action<Collision> _luaOnCollisionStay;
    private Action<Collision> _luaOnCollisionExit;

    private Action<Collider> _luaOnTriggerEnter;
    private Action<Collider> _luaOnTriggerStay;
    private Action<Collider> _luaOnTriggerExit;

    public void Awake()
    {
        LuaMonoBehaviourLua = GameLuaEnv.Instance.LuaEnv.NewTable();

        var meta = GameLuaEnv.Instance.LuaEnv.NewTable();
        meta.Set("__index", GameLuaEnv.Instance.LuaEnv.Global);
        LuaMonoBehaviourLua.SetMetaTable(meta);
        meta.Dispose();

        string luaMonoBehaviourTextPrev = "";
        if (luaMonoBehaviourText != null)
        {
            luaMonoBehaviourTextPrev = luaMonoBehaviourText.name.Split('.')[0];
        }

        LuaMonoBehaviourLua.Set("mono", this);
        LuaMonoBehaviourLua.Set("PrevName", luaMonoBehaviourTextPrev);
        if (Injections.Count != 0)
        {
            foreach (var injection in Injections)
            {
                if (injection != null)
                {
                    LuaMonoBehaviourLua.Set(injection.name, injection);
                }
                else
                {
                    Log.LogWarning("注入对象有空引用key");
                }
            }
        }

        if (luaMonoBehaviourText.text == null)
        {
            Log.LogWarning("LuaMonoBehaviour没有对应Lua脚本");
        }
        else
        {
            GameLuaEnv.Instance.LuaEnv.DoString(luaMonoBehaviourText.text, LuaMonoBehaviourLua.Get<string>("PrevName"),
                LuaMonoBehaviourLua);
        }

        LuaMonoBehaviourLua.Get("Awake", out _luaAwake);
        LuaMonoBehaviourLua.Get("Start", out _luaStart);
        LuaMonoBehaviourLua.Get("Update", out _luaUpdate);
        LuaMonoBehaviourLua.Get("OnDestroy", out _luaOnDestroy);

        LuaMonoBehaviourLua.Get("FixedUpdate", out _luaFixedUpdate);
        LuaMonoBehaviourLua.Get("LateUpdate", out _luaLateUpdate);
        LuaMonoBehaviourLua.Get("OnDisable", out _luaOnDisable);
        LuaMonoBehaviourLua.Get("OnEnable", out _luaOnEnable);

        LuaMonoBehaviourLua.Get("OnMouseDown", out _luaOnMouseDown);
        LuaMonoBehaviourLua.Get("OnMouseUp", out _luaOnMouseUp);
        LuaMonoBehaviourLua.Get("OnMouseDrag", out _luaOnMouseDrag);

        LuaMonoBehaviourLua.Get("OnCollisionEnter2D", out _luaOnCollisionEnter2D);
        LuaMonoBehaviourLua.Get("OnCollisionStay2D", out _luaOnCollisionStay2D);
        LuaMonoBehaviourLua.Get("OnCollisionExit2D", out _luaOnCollisionExit2D);

        LuaMonoBehaviourLua.Get("OnTriggerEnter2D", out _luaOnTriggerEnter2D);
        LuaMonoBehaviourLua.Get("OnTriggerStay2D", out _luaOnTriggerStay2D);
        LuaMonoBehaviourLua.Get("OnTriggerExit2D", out _luaOnTriggerExit2D);

        LuaMonoBehaviourLua.Get("OnCollisionEnter", out _luaOnCollisionEnter);
        LuaMonoBehaviourLua.Get("OnCollisionStay", out _luaOnCollisionStay);
        LuaMonoBehaviourLua.Get("OnCollisionExit", out _luaOnCollisionExit);

        LuaMonoBehaviourLua.Get("OnTriggerEnter", out _luaOnTriggerEnter);
        LuaMonoBehaviourLua.Get("OnTriggerStay", out _luaOnTriggerStay);
        LuaMonoBehaviourLua.Get("OnTriggerExit", out _luaOnTriggerExit);

        if (_luaAwake != null) _luaAwake();
    }

    private void Start()
    {
        if (_luaStart != null) _luaStart();
    }

    public void Update()
    {
        if (_luaUpdate != null) _luaUpdate();

        if (Time.time - lastGCTime > GCInterval)
        {
            GameLuaEnv.Instance.LuaEnv.Tick();
            lastGCTime = Time.time;
        }
    }

    private void OnDestroy()
    {
        if (_luaOnDestroy != null) _luaOnDestroy();

        _luaAwake = null;
        _luaStart = null;
        _luaUpdate = null;
        _luaOnDestroy = null;

        _luaFixedUpdate = null;
        _luaLateUpdate = null;
        _luaOnDisable = null;
        _luaOnEnable = null;

        _luaOnMouseDown = null;
        _luaOnMouseUp = null;
        _luaOnMouseDrag = null;

        _luaOnCollisionEnter2D = null;
        _luaOnCollisionStay2D = null;
        _luaOnCollisionExit2D = null;

        _luaOnTriggerEnter2D = null;
        _luaOnTriggerStay2D = null;
        _luaOnTriggerExit2D = null;

        _luaOnCollisionEnter2D = null;
        _luaOnCollisionStay2D = null;
        _luaOnCollisionExit2D = null;

        _luaOnTriggerEnter2D = null;
        _luaOnTriggerStay2D = null;
        _luaOnTriggerExit2D = null;

        _luaOnCollisionEnter = null;
        _luaOnCollisionStay = null;
        _luaOnCollisionExit = null;

        _luaOnTriggerEnter = null;
        _luaOnTriggerStay = null;
        _luaOnTriggerExit = null;

        luaMonoBehaviourText = null;
        LuaMonoBehaviourLua.Dispose();
        Injections = null;
    }

    private void FixedUpdate()
    {
        if (_luaFixedUpdate != null) _luaFixedUpdate();
    }

    private void LateUpdate()
    {
        if (_luaLateUpdate != null) _luaLateUpdate();
    }

    private void OnDisable()
    {
        if (_luaOnDisable != null) _luaOnDisable();
    }

    private void OnEnable()
    {
        if (_luaOnEnable != null) _luaOnEnable();
    }

    private void OnMouseDown()
    {
        if (_luaOnMouseDown != null) _luaOnMouseDown();
    }

    private void OnMouseUp()
    {
        if (_luaOnMouseUp != null) _luaOnMouseUp();
    }

    private void OnMouseDrag()
    {
        if (_luaOnMouseDrag != null) _luaOnMouseDrag();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_luaOnCollisionEnter != null) _luaOnCollisionEnter(other);
    }

    private void OnCollisionStay(Collision other)
    {
        if (_luaOnCollisionStay != null) _luaOnCollisionStay(other);
    }

    private void OnCollisionExit(Collision other)
    {
        if (_luaOnCollisionExit != null) _luaOnCollisionExit(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_luaOnTriggerEnter != null) _luaOnTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (_luaOnTriggerStay != null) _luaOnTriggerStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_luaOnTriggerExit != null) _luaOnTriggerExit(other);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_luaOnCollisionEnter2D != null) _luaOnCollisionEnter2D(other);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (_luaOnCollisionStay2D != null) _luaOnCollisionStay2D(other);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (_luaOnCollisionExit2D != null) _luaOnCollisionExit2D(other);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_luaOnTriggerEnter2D != null) _luaOnTriggerEnter2D(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_luaOnTriggerStay2D != null) _luaOnTriggerStay2D(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_luaOnTriggerExit2D != null) _luaOnTriggerExit2D(other);
    }
}