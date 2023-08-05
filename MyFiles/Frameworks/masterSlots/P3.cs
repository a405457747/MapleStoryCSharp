using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P3 : MonoBehaviour
{
    public int PosX;


    private void Start()
    {
        RectTransform rect = GetComponent<RectTransform>();

        if (Differences.Pad())
        {
            rect.anchoredPosition = new Vector2 (PosX,rect.anchoredPosition.y );
        }
    }
}