using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MapleStory;
using System.Linq;
using DG.Tweening;
using static MapleStory.LogNote;

public class DoTweenSample : MonoBehaviour
{
    private Tweener _tweener;

    private void Start()
    {
        UnityEngine.Time.timeScale = 0.5f;

        _tweener = transform.DOMove(Vector3.one * 2, 5);
        
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {

        yield return new WaitForSeconds(5f);
        Print("delay 1s");
        _tweener.Play();
        yield return
            _tweener.WaitForCompletion();
        Print("animal end");
        yield return new WaitForSeconds(
            1f);
        Print("delay 1s two");
    }
}
