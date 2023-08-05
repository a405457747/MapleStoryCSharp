using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2 : MonoBehaviour
{
    public int PosY;


    private void Start()
    {
        RectTransform rect = GetComponent<RectTransform>();

        if (Differences.Pad())
        {
              rect.anchoredPosition = new Vector2 (rect.anchoredPosition.x, PosY);
        }
    }
}
