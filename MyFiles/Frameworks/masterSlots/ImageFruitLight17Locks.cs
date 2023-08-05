using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ImageFruitLight17Locks : MonoBehaviour
{
    public int otherId;
    
    public Sprite Close;
    public Sprite Open;

    public Text betText;
    
    private int _lockNum;

    private GameObject lock1;
    private GameObject lock2;

    private void Awake()
    {
        lock1 = transform.Find("GameObject/Image").gameObject;
        lock2 = transform.Find("GameObject/Image (1)").gameObject;
    }

    public int LockNum
    {
        get => _lockNum;
        set
        {
            _lockNum = value;

            updateLockState(_lockNum);
        }
    }

    private void updateLockState(int lockNum)
    {
        print("updateLockState lockNum "+lockNum);
        
        if (lockNum==0)
        {
           betText.gameObject.SetActive(true); 
           
           lock1.gameObject.SetActive(false);
           lock2.gameObject.SetActive(false);

           GetComponent<Image>().sprite = Open;
        }else if (lockNum == 1)
        {
            lock1.gameObject.SetActive(false);
            lock2.gameObject.SetActive(true); 
           betText.gameObject.SetActive(false); 
            
        }else if (lockNum == 2)
        {
           lock1.gameObject.SetActive(true);
           lock2.gameObject.SetActive(true);
           betText.gameObject.SetActive(false); 
        }
    }

    private void updateBet(int bet)
    {
        betText.text = "x" + bet;
    }

    public void ResetState()
    {
           GetComponent<Image>().sprite = Close;
        LockNum = 2;
    }
}
