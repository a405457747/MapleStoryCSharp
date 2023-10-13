using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MapleStory2
{
    public class SetPanel : MonoBehaviour
    {
        internal static SetPanel Inst;
        private Slider musicSlider;
        private Slider effectSlider;

        private void Awake()
        {
            if (Inst == null) Inst = this;
            
            musicSlider = transform.Find("musicSlider").GetComponent<Slider>();
            effectSlider = transform.Find("effectSlider").GetComponent<Slider>();
        }

        private void OnDisable()
        {
            AudioMgr.Inst.SaveData();
        }

        private void Start()
        {
            musicSlider.value = AudioMgr.Inst.MusicVolume;
            effectSlider.value = AudioMgr.Inst.EffectVolume;

            musicSlider.onValueChanged.AddListener((f) => { AudioMgr.Inst.MusicVolume = f; });
            effectSlider.onValueChanged.AddListener((f) => { AudioMgr.Inst.EffectVolume = f; });
        }
    }
}