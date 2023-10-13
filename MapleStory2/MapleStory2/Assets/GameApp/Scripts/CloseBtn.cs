using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MapleStory2
{
    public class CloseBtn : MonoBehaviour
    {
        private Button btn;

        private void Awake()
        {
            btn = GetComponent<Button>();
        }

        private void Start()
        {
            btn.onClick.AddListener(() => { transform.parent.gameObject.SetActive(false); });
        }
    }
}