using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPurchase : UnityEngine.MonoBehaviour
{
    public string Prefix { get; set; }

    public virtual void Init()
    {
        Log(nameof(Init));
    }

    public abstract bool IsInit();

    public virtual void Buy(int i)
    {
        Log(nameof(Buy));

        if (IsInit() == false)
        {
            Init();
           // Game.I.luaSystem.LuaTip("Make sure the network is active, and then retry the purchase");
        }
    }

    public virtual void Restore()
    {
        
    }

    public virtual void Log(string str)
    {
    }

    public virtual void BuyFail(string msg)
    {
        Log(nameof(BuyFail));
       // Game.I.luaSystem.LuaTip("Purchase failed the reason is " + msg);
    }

    public virtual void BuySuccess(string msg)
    {
        Log(nameof(BuySuccess));
        //Game.I.luaSystem.LuaBuySuccess(msg);
    }
}