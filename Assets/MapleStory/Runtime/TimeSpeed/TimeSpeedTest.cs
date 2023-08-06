using System.Collections;
using System.Collections.Generic;
//using DG.Tweening;
using MapleStory;
using UnityEngine;

public class TimeSpeedTest : MonoBehaviour
{
    void Start()
    {
       TimeSpeed.SpeedScale = 20;

       //StartCoroutine(ss());

        var cube = GameObject.Find("Cube");
        Debug.Log("move start");
        //cube.transform.DOMoveY(5, 10).OnComplete(() => { Debug.Log("move end"); });
    }

    IEnumerator ss()
    {
        Debug.Log("ss start");
        yield return new WaitForSeconds(3f);
        Debug.Log("ss end");
    }

    private float timer = 0;


    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            //Debug.Log("timer per action");
            timer = 0;
        }

    }
}
