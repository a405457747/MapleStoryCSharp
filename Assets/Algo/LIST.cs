using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MapleStory;
using UnityEngine.Events;
using UniRx;
using System.Linq;
using Unity.Mathematics;
using static MapleStory.LogNote;
using Random = UnityEngine.Random;
using DG.Tweening;
using UnityEngine.Serialization;

public class LIST : MonoBehaviour
{
    [FormerlySerializedAs("Item")] public GameObject CubeItem;
    public GameObject Pointer;

    public List<INT> datas = new List<INT>();

    public Pointer l;
    public Pointer r;

    private void Start()
    {
    }

    public void Init(List<int> intData)
    {
        foreach (var intVal in intData)
        {
            Append(intVal);
        }
    }

    public Pointer InitPointer(int idx, Color c)
    {
        var go = GameObject.Instantiate(Pointer, transform.localPosition, quaternion.identity);
        go.transform.SetParent(transform);

        var p = go.GetComponent<Pointer>();
        p.Init(idx, this, c);
        return p;
    }

    public void Append(int val)
    {
        var go = GameObject.Instantiate(CubeItem, transform.localPosition, quaternion.identity);
        go.transform.SetParent(transform);

        var intC = go.GetComponent<INT>();
        datas.Add(intC);

        intC.Init(val, this);
    }

    public int GetIdx(int val)
    {
        for (var index = 0; index < datas.Count; index++)
        {
            var d = datas[index];
            if (d.val == val)
            {
                return index;
            }
        }

        return -1;
    }

    public void UpdateAllIndexAbout()
    {
        foreach (var data in datas)
        {
            data.UpdateIndexAbout();
        }
    }

    public void Swap(List<INT> datas, int i, int j)
    {
        var t = datas[i];
        datas[i] = datas[j];
        datas[j] = t;
        UpdateAllIndexAbout();
    }


    public IEnumerator Reverse(List<INT> list)
    {
        l = InitPointer(0, Color.cyan);
        r = InitPointer(list.Count - 1, Color.yellow);

        while (l.val <= r.val)
        {
            yield return new WaitForSeconds(0.75f);
            AudioManager.Inst.PlaySound("Fire");
            Swap(list, l.val, r.val);
            yield return new WaitForSeconds(0.25f);

            l.Add(1);
            r.Reduce(1);
        }
    }


    public IEnumerator QuickSort(List<INT> arr, int low, int high)
    {
        if (low < high)
        {
            
            yield return new WaitForSeconds(0.5f);
            
            AudioManager.Inst.PlaySound("Fire");
            
            l.SetVal(low);
            r.SetVal(high);
            
            int pi = Partition(arr, low, high);
            
            yield return new WaitForSeconds(0.5f);
            
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine( QuickSort(arr, low, pi - 1));
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine( QuickSort(arr, pi + 1, high));
        }
    }

    public void DestroyAllPointer()
    {
        Destroy(l.gameObject);
        Destroy(r.gameObject);
        l = null;
        r = null;
    }

    int Partition(List<INT> arr, int low, int high)
    {
   
        
        INT pivot = arr[high]; // 将最后一个元素作为基准
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (arr[j].val <= pivot.val)
            {
                i++;
                Swap(arr, i, j); // 原地交换
            }
        }

        Swap(arr, i + 1, high); // 将基准放到正确的位置上
        
        return i + 1;
    }
}