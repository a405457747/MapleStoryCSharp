using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapleStory;

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
        LogNote.Print("AppRoot Start");
    }
    
}
