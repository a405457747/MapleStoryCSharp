using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapleStory;
using static MapleStory.LogNote;
public class LogNoteTest : MonoBehaviour
{

    void Start()
    {
        LogNote.LogLv = 4;

        Print<int>("niaho",true,2,new List<int>(){44,55,66},new Dictionary<string,int>(){{"a",22},{"b",33}});
        
        Print("nihao",true,223);
        Print<int>(new List<int>(){1,2,3,4},"nihao");
        
        
        LogNote.Debug("debug");
        LogNote.Info("info");
        LogNote.Warning("warning");
        LogNote.Error("error");

      
        Dictionary<string, object> dic = new Dictionary<string, object>()
        {
            { "name", "bill" },
            { "age", 32 },
            {"kw",false}
        };
        //LogNote.Info(dic["name"],dic["age"],"gg",dic["kw"]);
        
        LogNote.Info(LogNote.DictStr(dic),LogNote.ListStr(new List<int>(){1,2,3,4}));
        var obj = new { name = "bill", age = 332 };
       LogNote.Info( LogNote.AnonymousObjStr(obj));
    }

}
