using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using  UnityEngine.UI;
using MapleStory;


public class keaiPanel : MonoBehaviour
{

    ///AC

    public Toggle aasaToggle = null;
    
    public ssGameObject ssGameObject = null;
    
    public Button gg2Button = null;
    
    public void gg2ButtonOnClick(UnityAction clickAction)=> gg2Button.onClick.AddListener(clickAction.Invoke);
    
    public Image bkImage = null;
    
    public void bkImageSprite(Sprite sp)=> bkImage.sprite = sp;
    
    public kbGameObject kbGameObject = null;
    
    public Image DWImage = null;
    
    public void DWImageSprite(Sprite sp)=> DWImage.sprite = sp;
    
    public kkGameObject kkGameObject = null;
    
    void FindAllComponent()
    {
        aasaToggle=ToolRoot.FindComponent<Toggle>(this,"aasaToggle");
        ssGameObject=ToolRoot.FindComponent<ssGameObject>(this,"ssGameObject");
        gg2Button=ToolRoot.FindComponent<Button>(this,"gg2Button");
        bkImage=ToolRoot.FindComponent<Image>(this,"bkImage");
        kbGameObject=ToolRoot.FindComponent<kbGameObject>(this,"kbGameObject");
        DWImage=ToolRoot.FindComponent<Image>(this,"DWImage");
        kkGameObject=ToolRoot.FindComponent<kkGameObject>(this,"kkGameObject");
        
    }
    
    ///AC

    private void Start()
    {
        FindAllComponent();

        print(ssGameObject);
        
       // LogTool.   Assert(3==4,"noskk");
        
        Debug.Log(gg2Button);
        print(bkImage);
        print(kbGameObject);
        print(DWImage);
        print(kkGameObject);
    }
}
