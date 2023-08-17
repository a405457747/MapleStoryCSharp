using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapleStory;
using static MapleStory.LogNote;
public class AppRoot : GameRootBase
{

    public static AppRoot Instance { get; private set; }


     
     public   GiftManager giftManager { get; private set; }

     public ScoreJudge scoreJudge { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Instance = this;

        OpenPanel<MainPanel>();

        giftManager =gameObject.AddComponent<GiftManager>();
        scoreJudge =gameObject.AddComponent<ScoreJudge>();
    }

    void Start()
    {
        Print<int>("AppRoot","Start!!!",new List<int>(){1,2,3,4},true);


        
    }
    

}
