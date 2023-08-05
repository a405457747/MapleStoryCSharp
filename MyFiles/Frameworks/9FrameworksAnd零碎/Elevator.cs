using System;
using System.Collections;
using System.Collections.Generic;
using CallPalCatGames.QFrameworkExtension;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float Length = 50f;
    private RectTransform rect;
    public float Speed = 100f;

    private void Awake()
    {
        rect = transform as RectTransform;
    }

    private void Update()
    {
        rect.SetPosY(Mathf.PingPong(Time.time*Speed , Length));
    }
}