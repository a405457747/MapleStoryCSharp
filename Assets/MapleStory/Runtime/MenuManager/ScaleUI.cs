using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MapleStory;
using UnityEngine;
using UnityEngine.Serialization;

namespace MapleStory
{
    public class ScaleUI : UIMotionBase, IActive
    {

        public virtual void Show(Action act)
        {
            gameObject.SetActive(true);
            transform.DOScale(0, 0).OnComplete(() => { transform.DOScale(1, motionCost); });
        }

        public virtual void Hide(Action act)
        {
            transform.DOScale(0, motionCost).OnComplete(() =>
            {
                gameObject.SetActive(false);
                transform.DOScale(1, 0);
            });
        }
    }
}