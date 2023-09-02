using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MapleStory;
using UnityEngine.Events;
using UniRx;
using System.Linq;
using UnityEngine.SceneManagement;
using static MapleStory.LogNote;
using UnityEngine.UI;

public class AlgoMgr : MonoBehaviour
{
    public Button startBtn;
    public Button restartBtn;
    public Button ctrlBtn;

    public static AlgoMgr Inst { get; private set; }

    private bool IsPause = false;
    private void Awake()
    {
        Inst = this;
    }


    private void Start()
    {
        //InitQuickSort();
        InitReverse();
        
        ctrlBtn.onClick.AddListener(() =>
        {
            IsPause = !IsPause;

            if (IsPause)
            {
                Time.timeScale = 0f;
                ctrlBtn.GetComponentInChildren<Text>().text = "恢复";
            }
            else
            {
                Time.timeScale = 1f;
                ctrlBtn.GetComponentInChildren<Text>().text = "暂停";
            }
        });
        
        restartBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("ReverseList");
        });
        
        startBtn.onClick.AddListener(() =>
        {
            //PlayQuickSort();
            PlayReverse();
        });
    }

    public LIST InitList(List<int> data, Vector3 offsetPos)
    {
        var go = GameObject.Instantiate(Resources.Load<GameObject>("LIST"));
        LIST l = go.GetComponent<LIST>();
        l.Init(data);
        go.transform.localPosition = transform.localPosition + offsetPos;
        return l;
    }

    private LIST l1;
    private LIST l2;

    public void InitReverse()
    {
        l1 = InitList(new List<int>() { 4, 3, 2, 1 }, Vector3.zero);
        l2 = InitList(new List<int>() { 5, 4, 3, 2, 1 }, Vector3.up * 3f);
    }

    public void PlayReverse()
    {
        StartCoroutine(l1.Reverse(l1.datas));
        StartCoroutine(l2.Reverse(l2.datas));
    }

    public void InitQuickSort()
    {
        //l1 =InitList(new List<int>() { 3, 5, 8, 1,2,9,4,7,6 }, Vector3.zero);
        l1 =InitList(new List<int>() { 9, 8, 7, 6,5,4,3,2,1 }, Vector3.zero);
    }

    public void PlayQuickSort()
    {
        int low = 0;
        int high = l1.datas.Count() - 1;
        
        l1.l = l1.InitPointer(low, Color.cyan);
        l1.r = l1.InitPointer(high, Color.yellow);
        
        StartCoroutine(l1.QuickSort(l1.datas, low, high));
    }

    private void Update()
    {
    }


}