using System.Collections;
using System.Collections.Generic;
using MapleStory;
using UnityEngine;

public class LanguageSample : MonoBehaviour
{

    void Start()
    {
        var tool = GetComponent<LanguageTool>();
        print(tool.GetMessage(1));

    }
}
