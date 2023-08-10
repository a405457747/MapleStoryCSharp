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