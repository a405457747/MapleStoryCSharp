using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Kit3;
using static Kit;
using DG.Tweening;
using XLua;
using MapleStory;

public class Move : MonoBehaviour
{
    public double speed => luaenv.Global.Get<float>("speed");

    [SerializeField] public GameObject cube;

    private LuaEnv luaenv;
    private Action rel;
    [Header("skk")]
    public int age;

    [Space(40)] private int age2;
    private void Awake()
    {
       // MapleStory. Move m = new MapleStory.Move();
       //Debug.Log(m);
        Debug.Log("sni",this);
        luaenv = new LuaEnv();

        // luaenv.DoString("require('tt');"); 
        luaenv.DoString("require('t2');");

        rel = luaenv.Global.Get<Action>("require_ex_some");

        var dic = new Dictionary<string, int>()
        {
            { "sss", 33 },
            { "sswk", 222 }
        };

        var dic2 = new Dictionary<string, string>()
        {
            { "sss", "3" },
            { "sswk", "222" }
        };

        var dic3 = new Dictionary<int, int>()
        {
            { 333, 222 }, // sss
            { 222, 1111 } // sfwl
        };
        var dic4 = new Dictionary<object, object>()
        {
            { 333, 222 },
            { 222, 1111 }
        };

        var liist = new List<int>() { 2, 3, 4, 5, 6, 7, 2 };
        // wwo
        // wok
        /* sskk */

        for (int i = 0; i < liist.Count; i++)
        {
            print(liist[i]);
        }

    }

    private void Update()
    {
        var r = Vector3.up * Time.deltaTime * (float)speed;
        transform.Rotate(r);

        if (Input.GetKeyDown(KeyCode.S))
        {
            rel();
            print("speed is " + ":" + speed);
        }
    }
}