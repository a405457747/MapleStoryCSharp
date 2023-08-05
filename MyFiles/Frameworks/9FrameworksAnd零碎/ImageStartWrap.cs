using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using Image = UnityEngine.UI.Image;

public class ImageStartWrap : MonoBehaviour
{
    public GameObject[] starts;

    public void ShowStart(int startNum, Action callBack = null,float fillTotalTime =0f,float scaleTotalTime=0.23f)
    {
        var s = DOTween.Sequence();
        float per = fillTotalTime / startNum;

        for (int i = 0; i < startNum; i++)
        {
            var image = starts[i].transform.Find("Image").GetComponent<Image>();
            var t =image .DOFillAmount(1f, per).OnComplete(() =>
            {
                ActivityManager.Instance.ScaleBigToOriginal(image.transform,null,scaleTotalTime,2f);
            });
            s.Append(t);
        }

        s.AppendInterval(scaleTotalTime+scaleTotalTime);
        s.OnComplete(() =>
        {
            callBack?.Invoke();
        });
    }
}