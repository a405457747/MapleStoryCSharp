using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MapleStory;

public class ssGameObject : MonoBehaviour
{

    ///AC

    public Button ssButton = null;
    void FindAllComponent()
    {
        ssButton=ToolRoot.FindComponent<Button>(this,"ssButton");
    }
    
    ///AC
    void Start()
    {
        FindAllComponent();
    }        
}
