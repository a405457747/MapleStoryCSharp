using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    private Image img;

    public float DelayTime = 1f;
    private void Awake()
    {
        img = GetComponent<Image>();
    }

    private void Start()
    {
        
    }

    public void DelayHide()
    {
        img.DOFade(0, DelayTime).OnComplete(() =>
        {
            gameObject.SetActive(false);
            img.DOFade(1, 0f);
        });
    }
}
