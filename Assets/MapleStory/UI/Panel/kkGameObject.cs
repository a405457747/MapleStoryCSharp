using System.Collections;
using System.Collections.Generic;
using MapleStory;
using UnityEngine;
using UnityEngine.UI;

public class kkGameObject : MonoBehaviour
{

    ///AC

    public Button ainiButton = null;
    void FindAllComponent()
    {
        ainiButton=ToolRoot.FindComponent<Button>(this,"ainiButton");
        print("ainiButton"+":"+ainiButton);
    }
    
    ///AC
    void Start()
    {
        FindAllComponent();
    }        
}
