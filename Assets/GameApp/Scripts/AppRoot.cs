using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapleStory;
using static MapleStory.LogNote;
public class AppRoot : GameRootBase
{

    public static AppRoot Instance { get; private set; }

                        //-DEL-我喜欢你
     
     
             /**-DEL-他们
              * 我爱
              *  你
              * 
              */
    protected override void Awake()
    {
        base.Awake();

        Instance = this;
    }

    void Start()
    {
        Print<int>("AppRoot","Start!!!",new List<int>(){1,2,3,4},true);


        
    }
    

}
