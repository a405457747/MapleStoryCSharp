﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButtonScrollRect : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    private ScrollRect scrollRect;

    public float smoothing = 4;

    private float[] pageArray = new float[] {0, 0.33333f, 0.66666f, 1};

    //public Toggle[] toggleArray;
    private float targetHorizontalPosition = 0;
    private bool isDraging = false;

    void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();

        ChangePos();
    }

    private void ChangePos()
    {
        var ID = SaveManager.Instance.SaveMap.presentSceneID.Value;

        int pageNum = 9;
        int pageNum2 = pageNum * 2;
        int pageNum3 = pageNum * 3;
        int pageNum4 = pageNum * 4;

        int changeIndex = 0;
        if (ID > -1 && ID <= pageNum)
        {
            changeIndex = 0;
        }
        else if (ID > pageNum && ID <= pageNum2)
        {
            changeIndex = 1;
        }
        else if (ID > pageNum2 && ID <= pageNum3)
        {
            changeIndex = 2;
        }
        else if (ID > pageNum3 && ID <= pageNum4)
        {
            changeIndex = 3;
        }

        targetHorizontalPosition = pageArray[changeIndex];
    }

    void Update()
    {
        if (isDraging == false)
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition,
                targetHorizontalPosition, Time.deltaTime * smoothing);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        isDraging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDraging = false;

        float posX = scrollRect.horizontalNormalizedPosition;
        int index = 0;
        float offset = Mathf.Abs(pageArray[index] - posX);

        for (int i = 1; i < pageArray.Length; i++)
        {
            float offsetTemp = Mathf.Abs(pageArray[i] - posX);
            if (offsetTemp < offset)
            {
                index = i;
                offset = offsetTemp;
            }
        }

        targetHorizontalPosition = pageArray[index];

        //toggleArray[index].isOn = true;

        //scrollRect.horizontalNormalizedPosition = pageArray[index];
    }

    public void MoveToPage1(bool isOn)
    {
        if (isOn)
        {
            targetHorizontalPosition = pageArray[0];
        }
    }

    public void MoveToPage2(bool isOn)
    {
        if (isOn)
        {
            targetHorizontalPosition = pageArray[1];
        }
    }

    public void MoveToPage3(bool isOn)
    {
        if (isOn)
        {
            targetHorizontalPosition = pageArray[2];
        }
    }

    public void MoveToPage4(bool isOn)
    {
        if (isOn)
        {
            targetHorizontalPosition = pageArray[3];
        }
    }
}