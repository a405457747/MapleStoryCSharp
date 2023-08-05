using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MapleStory;
using System.Linq;

public class LogoPanel : MonoBehaviour
{

    


    ///AC

    [SerializeField] private Image mainImage = null;
    
    public void mainImageSprite(Sprite sp)=> mainImage.sprite = sp;

   [SerializeField] private Text verText;
    void FindAllComponent()
    {
        mainImage=ToolRoot.FindComponent<Image>(this,"mainImage");
        verText = ToolRoot.FindComponent<Text>(this, "verText");
    }
    
    ///AC
    void Start()
    {
        FindAllComponent();
    }        
}
