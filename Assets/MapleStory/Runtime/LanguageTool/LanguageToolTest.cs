using System.Collections;
using System.Collections.Generic;
using MapleStory;
using UnityEngine;

public class LanguageToolTest : MonoBehaviour
{

    void Start()
    {
        var tool = GetComponent<LanguageTool>();
        print(tool.GetMessage(1));
        print(tool.FillMessage(1,"hello"));
    }


}
