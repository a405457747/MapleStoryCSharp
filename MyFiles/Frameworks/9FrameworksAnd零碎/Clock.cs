using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [HideInInspector]
    public int TotalTime;
    private Text TimeText;
    private int mumite;
    private int second;

    void Awake()
    {
        TimeText = GetComponent<Text>();
    }

    public IEnumerator StartTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);//由于开始倒计时，需要经过一秒才开始减去1秒，
            TotalTime++;
            TimeText.text = "Time:" + TotalTime;
            mumite = TotalTime / 60; //输出显示分
            second = TotalTime % 60; //输出显示秒
            if (second >= 10)
            {
                TimeText.text = "0" + mumite + ":" + second;
            }     //如果秒大于10的时候，就输出格式为 00：00
            else
                TimeText.text = "0" + mumite + ":0" + second;      //如果秒小于10的时候，就输出格式为 00：00
        }
    }
}
