using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1 : MonoBehaviour
{
    public int top=82;
    public int bottom=-82;
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        if (Differences.Pad())
        {
            GetComponent<RectTransform> ().offsetMax = new Vector2 (GetComponent<RectTransform> ().offsetMax.x, -top);
            GetComponent<RectTransform> ().offsetMin = new Vector2 (GetComponent<RectTransform> ().offsetMin.x, bottom); 
        }
    }
}
