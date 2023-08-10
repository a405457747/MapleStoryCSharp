using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MapleStory;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MapleStory
{
    public class TransparentUI : UIMotionBase, IActive
    {


        public void Show(Action act = null)
        {
            gameObject.SetActive(true);

            var canvasGroup = gameObject.AddOrGetComponent<CanvasGroup>(); //.AddOrGetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
            canvasGroup.DOFade(1f, AnimationCost).OnComplete(() => { });
        }

        public void Hide(Action act = null)
        {
            transform.DOScale(0, AnimationCost).OnComplete(() =>
            {
                gameObject.SetActive(false);
                var canvasGroup = gameObject.AddOrGetComponent<CanvasGroup>();
                canvasGroup.alpha = 1f;
            });
        }


        public virtual void Show(Action act)
        {
            gameObject.SetActive(true);
            Image img = transform.GetComponent<Image>();
            img.DOFade(0, 0).OnComplete(() => { img.DOFade(1, motionCost); });
        }

        public virtual void Hide(Action act)
        {
            Image img = transform.GetComponent<Image>();
            img.DOFade(0, motionCost).OnComplete(() =>
            {
                gameObject.SetActive(false);
                img.DOFade(1, 0);
            });
        }
    }
}