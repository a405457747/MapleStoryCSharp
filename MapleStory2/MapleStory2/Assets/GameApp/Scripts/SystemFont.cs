using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MapleStory2
{
    public class SystemFont : MonoBehaviour
    {
        private Text _txt;

        private void Awake()
        {
            _txt = GetComponent<Text>();
        }

        private void Start()
        {
            _txt.font = Resources.Load<Font>("Font/Alibaba-PuHuiTi-Medium");
        }
    }
}