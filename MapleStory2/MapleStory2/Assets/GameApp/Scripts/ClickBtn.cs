using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MapleStory2
{
    public class ClickBtn : MonoBehaviour
    {
        public float aniCost = 0.25f;
        public string audioPath;
        
        private Button btn;
        private void Awake()
        {
            btn = GetComponent<Button>();
        }

        private void Start()
        {
            
            btn.onClick.AddListener(() =>
            {
                AudioMgr.Inst.EffectPlay(audioPath);
                transform.DOScale(Vector3.one*0.5f,0).OnComplete(() =>
                {
                    transform.DOScale(Vector3.one, aniCost);
                });
            });
        }
    }
}