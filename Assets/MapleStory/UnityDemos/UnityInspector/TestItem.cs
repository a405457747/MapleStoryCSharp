using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MapleStory;
using System.Linq;

using static MapleStory.LogNote;
[Serializable]
public class TestItem  //序列化这个不方便的 :MonoBehaviour
{
    private int _age;

    public int Age
    {
        get
        {
            return _age;
        }
        set
        {
            _age = value;
        }
    }
    public string Name;
    public List<int> Ages;
    public List<string> Names=new List<string>(){"aa","bb"};

    public List<TestItem> TestItems;

    public Dictionary<string, int> Dict;

    public GameObject
        skk;
}