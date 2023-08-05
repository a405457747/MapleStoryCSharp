using System;
using System.Collections;
using System.Collections.Generic;
using MapleStory;
using UnityEngine;

public class EObj
{
    public string name = "EObj";
}

public class EventTest : MonoBehaviour
{


    private EventTool _eventTool;
    private void Start()
    {
        _eventTool = GetComponent<EventTool>();
        
        _eventTool.AddEventListener<EObj>("nihao",Nihao);
        _eventTool.AddEventListener<EObj>("nihao",Nihao2);
        _eventTool.CanselEventListener<EObj>("nihao",Nihao2);
        
        _eventTool.EventTrigger<EObj>("nihao",new EObj());
    }

    private void Nihao2(EObj e)
    {
        print("Nihao2"+e.name);
    }

    private void Nihao(EObj e)
    {
        print("Nihao"+e.name);
    }
}
