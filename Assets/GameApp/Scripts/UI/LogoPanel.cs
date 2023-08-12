using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MapleStory;
using UnityEngine.Events;
using UniRx;
using System.Linq;
using static MapleStory.LogNote;

public class LogoPanel : MonoBehaviour
{



    ///AC

    [SerializeField] public Image mainImage = null;
    
    public void mainImageSprite(Sprite sp)=> mainImage.sprite = sp;
    
    void FindAllComponent()
    {
        mainImage=ToolRoot.FindComponent<Image>(this,"mainImage");
        
    }
    
    ///AC
    void Awake()
    {
        FindAllComponent();
    }        
}
