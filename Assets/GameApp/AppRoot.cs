using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapleStory;
using static MapleStory.LogNote;
public class AppRoot : GameRootBase
{

    public static AppRoot Instance { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        Instance = this;
    }

    void Start()
    {
        Print("AppRoot","Start!!!");

        decimal ks = -222.223m;
        print(ks+0.3m);
        Info("Version is 0.0.2",ks);
    }

}
