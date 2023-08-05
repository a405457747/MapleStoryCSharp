using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapleStory;

public class AppRoot : GameRootBase
{

    public static AppRoot Instance { get; private set; }
    
    public  TodoMainPanel TodoMainPanel { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Instance = this;
        TodoMainPanel=OpenPanel<TodoMainPanel>();
    }

    void Start()
    {
        
      
    }
    
}
