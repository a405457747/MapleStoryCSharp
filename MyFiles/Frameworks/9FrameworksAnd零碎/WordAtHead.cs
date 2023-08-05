using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using xmaolol.com;
using QFramework;
using Lean.Pool;


public class WordAtHead : MonoBehaviour
{
    public Color BigStateFontColor;
    public Color SmallStateFontColor;
    public Text wordAtHeadText;
    public GameObject bigStateIconObject;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private float offsetY;
    private float targetY;
    private float initPosY;
    private float stage1Time;
    private float stage2Time;
    //是否是暴击
    private bool isBigState;
    private int smallStateFontSize;
    private int bigStateFontSize;
    private float fontLargerRatio;

    public bool IsBigState
    {
        get
        {
            return isBigState;
        }
        set
        {
            isBigState = value;
            if (value == true)
            {
                wordAtHeadText.fontSize = bigStateFontSize;
                wordAtHeadText.color = BigStateFontColor;
                if (bigStateIconObject != null)
                {
                    bigStateIconObject.SetActive(true);
                }
            }
            else
            {
                wordAtHeadText.fontSize = smallStateFontSize;
                wordAtHeadText.color = SmallStateFontColor;
                if (bigStateIconObject != null)
                {
                    if (bigStateIconObject.activeInHierarchy == true)
                    {
                        bigStateIconObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void Awake()
    {
        //这里来配置初始数据
        NeedData();

        canvasGroup = GetComponentInChildren<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        smallStateFontSize = wordAtHeadText.fontSize;
        bigStateFontSize = (int)(smallStateFontSize * fontLargerRatio);
    }

    private void NeedData(float fontLargerRatio = 2f, float offsetY = 0.96f, float stage1Time = 0.45f,
        float stage2Time = 0.25f, bool isBigState = false)
    {
        this.fontLargerRatio = fontLargerRatio;
        this.offsetY = offsetY;
        this.stage1Time = stage1Time;
        this.stage2Time = stage2Time;
        this.isBigState = isBigState;
    }

    private void OnSpawn()
    {
        initPosY = rectTransform.anchoredPosition.y;
        targetY = initPosY + offsetY;
        var s = DOTween.Sequence();
        s.Append(rectTransform.DOAnchorPos3DY(initPosY, Consts.InstantaneousRecoveryTime)).SetEase(Ease.Linear);
        s.Insert(0, canvasGroup.DOFade(1, Consts.InstantaneousRecoveryTime).SetEase(Ease.Linear));
    }

    public void NeedData(int Number, bool isBigState)
    {
        IsBigState = isBigState;
        wordAtHeadText.text = Number.ToString();
        var s = DOTween.Sequence();
        s.Append(rectTransform.DOAnchorPos3DY(targetY, stage1Time + stage2Time)).SetEase(Ease.Linear);
        s.Insert(stage1Time, canvasGroup.DOFade(0, stage2Time).SetEase(Ease.Linear));
        s.InsertCallback((stage1Time + stage2Time), () => { RecycleObject(); });
    }

    public void RecycleObject()
    {
        LeanPool.Despawn(this.gameObject);
    }
}
