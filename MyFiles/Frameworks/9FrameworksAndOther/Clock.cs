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
            yield return new WaitForSeconds(1);//���ڿ�ʼ����ʱ����Ҫ����һ��ſ�ʼ��ȥ1�룬
            TotalTime++;
            TimeText.text = "Time:" + TotalTime;
            mumite = TotalTime / 60; //�����ʾ��
            second = TotalTime % 60; //�����ʾ��
            if (second >= 10)
            {
                TimeText.text = "0" + mumite + ":" + second;
            }     //��������10��ʱ�򣬾������ʽΪ 00��00
            else
                TimeText.text = "0" + mumite + ":0" + second;      //�����С��10��ʱ�򣬾������ʽΪ 00��00
        }
    }
}
