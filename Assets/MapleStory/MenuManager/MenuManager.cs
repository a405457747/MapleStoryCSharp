using System;
using System.Collections;
using System.Collections.Generic;
//using DG.Tweening;
using UnityEngine;

namespace  MapleStory
{
    public interface IActive
    {
        void Show(Action act=null);
        void Hide(Action act=null);
    }

public class MenuManager : MonoBehaviour
{

    public virtual void Awake()
    {
        
    }
  
    
        /*
    // 果冻效果
    public void JellyEffect(Transform t, Vector3 punch = default, float duration = 0.35f, int vibrato = 12, float elasticity = 0.5f)
    {
        if (punch == default)
        {
            punch = new Vector3(-0.2f, 0.2f, 0f);
        }

        var origin = t.localScale;
        t.DOPunchScale(punch, duration, vibrato, elasticity)
            .OnComplete(() => { t.localScale = origin; });
    }
        */
}

}
