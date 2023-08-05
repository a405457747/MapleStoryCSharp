using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MapleStory;
using System.Linq;

using static MapleStory.LogNote;


public class UnityInspectorTest : MonoBehaviour
{
    //public QS_Dictionary<string, int> _Attributes = new QS_Dictionary<string, int>();

  //序列化会常驻编辑器
    public List<TestItem> tes;

    void Start()
    {
        tes.Clear();
        print(tes.Count);
        print(tes==null);//序列化默认就是实例化了的。
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Print<int>(tes[0].Ages,tes[0].Names,tes[0].Names.Count);
        }
    }
}
