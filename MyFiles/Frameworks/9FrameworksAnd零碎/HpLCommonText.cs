/*/****************************************************************************
 * Copyright (c) 2019 ~ 2019.12 S.Allen
 * 
 * 894982165@qq.com
 * https://github.com/a405457747
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 ***************************************************************************#1#

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CallPalCatGames.WaterFramework
{
    public class HpLCommonText : PoolBase
    {
        public const float OffsetYBase = 17f;
        public const float OffsetXBase = 3.3f;
        public TextMeshProUGUI TextMeshProUGUI;
        public GameObject bigStateIconObject;
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
                    TextMeshProUGUI.fontSize = bigStateFontSize;
                    TextMeshProUGUI.color = Color.red;
                    if (bigStateIconObject != null)
                    {
                        bigStateIconObject.SetActive(true);
                    }
                }
                else
                {
                    TextMeshProUGUI.fontSize = smallStateFontSize;
                    TextMeshProUGUI.color = Color.white;
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

        private CanvasGroup canvasGroup;
        private RectTransform rectTransform;
        private float stage1Time;
        private float stage2Time;
        //是否是暴击
        private bool isBigState;
        private float smallStateFontSize;
        private float bigStateFontSize;
        private float fontLargerRatio;
        private float InitY;
        private float InitX;

        private void Awake()
        {
            //这里来配置初始数据
            canvasGroup = GetComponentInChildren<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();
            NeedInitData();
            smallStateFontSize = TextMeshProUGUI.fontSize;
            bigStateFontSize = (smallStateFontSize * fontLargerRatio);
            InitY = rectTransform.anchoredPosition.y;
            InitX = rectTransform.anchoredPosition.x;
        }

        public void NeedData(int Number, bool isBigState, float offsetY = OffsetYBase)
        {
            rectTransform.DOAnchorPosY(offsetY - OffsetYBase, Consts.InstantaneousRecoveryTime);

            IsBigState = isBigState;
            TextMeshProUGUI.text = Number.ToString();
            var s = DOTween.Sequence();
            s.Append(rectTransform.DOAnchorPosY(offsetY, stage1Time + stage2Time)).SetEase(Ease.Linear);
            s.Insert(0, rectTransform.DOAnchorPosX(InitX + Tool.GetRandomPositiveAndNegative(OffsetXBase), Consts.InstantaneousRecoveryTime)).SetEase(Ease.Linear);
            s.Insert(stage1Time, canvasGroup.DOFade(0, stage2Time).SetEase(Ease.Linear));
            s.InsertCallback((stage1Time + stage2Time), () => { PoolsManager.Instance.DeSpawn(this.gameObject); });
        }

        public override void OnSapwan()
        {

        }

        public override void OnDespawn()
        {
            var s = DOTween.Sequence();
            s.Append(rectTransform.DOAnchorPosY(InitY, Consts.InstantaneousRecoveryTime)).SetEase(Ease.Linear);
            s.Insert(0, canvasGroup.DOFade(1, Consts.InstantaneousRecoveryTime).SetEase(Ease.Linear));
            s.Insert(0, rectTransform.DOAnchorPosX(InitX, Consts.InstantaneousRecoveryTime).SetEase(Ease.Linear));
        }

        private void NeedInitData(float fontLargerRatio = 1.2f, float stage1Time = 0.4f,
            float stage2Time = 0.2f, bool isBigState = false)
        {
            this.fontLargerRatio = fontLargerRatio;
            this.stage1Time = stage1Time;
            this.stage2Time = stage2Time;
        }
    }
}*/
