using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Button17Bonus : MonoBehaviour
{
    private Button btn;

    private GameObject cap;
    private GameObject txt10;
    private Image ImageRes;
    private void Awake()
    {
        btn = GetComponent<Button>();
        cap = transform.Find("Image (3)").gameObject;
        txt10 = transform.Find("TextTen").gameObject;
        ImageRes = transform.Find("ImageRes").GetComponent<Image>();
    }

    private void Start()
    {
       ResetState(); 
    }

    //1-40 2-20 3-30;
    public void Init()
    {
        btn.onClick.AddListener(() =>
        {
            if (Game.I.mini17Panel.IsGameOver())
            {
                return;
            }
            
        cap.gameObject.SetActive(false);
          int tmpState=  Game.I.mini17Panel.ClickTheBonus(btn.gameObject.Number());
          updateTmpState(tmpState);
            btn.interactable = false;
        });
    }

    private void updateTmpState(int tmpState)
    {
        if (tmpState==4)
        {
            txt10.GetComponent<Text>().text = "x10";
            ImageRes.gameObject.SetActive(false);
        }
        else
        {
            txt10.GetComponent<Text>().text = "";
            ImageRes.gameObject.SetActive(true);
            ImageRes.sprite = Factorys.GetAssetFactory().LoadSprite("bkey" + tmpState);
        }
    }

    public void ResetState()
    {
        btn.interactable =true;
        cap.gameObject.SetActive(true);
    }
}
