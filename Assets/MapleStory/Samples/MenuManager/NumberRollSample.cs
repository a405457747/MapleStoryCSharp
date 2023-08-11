using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MapleStory;
using UnityEngine.Events;
using UniRx;
using System.Linq;
using static MapleStory.LogNote;
using DG.Tweening;

public class NumberRollSample : MonoBehaviour
{


    public Text text;
    private Sequence mScoreSequence;
    private int mOldScore = 0;
    private int newScore = 0;
    void Awake()
    {
        mScoreSequence = DOTween.Sequence();
        mScoreSequence.SetAutoKill(false);
    }
    void Start()
    {
    }
    void DigitalAnimation() {
        mScoreSequence.Append(DOTween.To(delegate (float value) {
            var temp = Math.Floor(value);
            text.text = temp + "";
        }, mOldScore, newScore, 5f));
        mOldScore = newScore;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            newScore += 1234;
            DigitalAnimation();
        }
    }




}
