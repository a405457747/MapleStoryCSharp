using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MapleStory
{
    public class TimeSpeed : MonoBehaviour
    {
        private static float _speedScale = 1f;

        public static float SpeedScale
        {
            get => _speedScale;
            set
            {
                _speedScale = value;
                UnityEngine.Time.timeScale = _speedScale;
            }
        }

        public virtual void Awake()
        {
            
        }
    }
}