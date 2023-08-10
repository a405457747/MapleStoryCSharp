using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using xmaolol.com;
using System;

public class PropFlyToUI : MonoBehaviour
{
    private Vector3 target;

    public void StartFly(float screenCoordinatesX, float screenCoordinatesY, Action ArrivedCallback, float flyTime = 1f)
    {
        target = Camera.main.ScreenToWorldPoint(new Vector3(screenCoordinatesX, screenCoordinatesY));
        transform.DOMove(target, flyTime).OnComplete(() =>
        {
            if (ArrivedCallback != null)
            {
                ArrivedCallback();
            }
        });
    }
}
