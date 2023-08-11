using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MapleStory;
using UniRx;
using UnityEngine;

public class TimeSpeedSample : MonoBehaviour
{
    private float timer = 0;

    void Start()
    {
        TimeSpeed.SpeedScale = 0.25f;

        Debug.Log("延时一秒 start");
        Observable.Timer(TimeSpan.FromSeconds(1))
            .Subscribe(_ => { Debug.Log("延时一秒 over"); });

        return;
        StartCoroutine(ss());

        var cube = GameObject.Find("Cube");
        Debug.Log("move start");

        cube.transform.DOMoveY(5, 10).OnComplete(() => { Debug.Log("move end"); });
    }

    IEnumerator ss()
    {
        Debug.Log("ss start");
        yield return new WaitForSeconds(3f);
        Debug.Log("ss end");
    }


    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            Debug.Log("timer per action");
            timer = 0;
        }
    }
}