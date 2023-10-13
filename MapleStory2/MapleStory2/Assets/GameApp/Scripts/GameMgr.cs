using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapleStory2
{
    public class GameMgr : MonoBehaviour
    {
        internal static GameMgr Inst;

        private void Awake()
        {
            Inst = this;
            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            
        }
    }
}