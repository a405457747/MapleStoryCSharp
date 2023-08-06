using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

    public class HpClickUI : MonoBehaviour
    {
        public Image img;
    
        public Button btn;

        public Text text;

        public Text text2;
        
        
        public void btnClick(UnityAction aa)=> btn.onClick.AddListener(aa.Invoke);


        public void textUpdate(string txt)
        {
            text.text = txt;
        }
        public void textUpdate2(string txt)=>text2.text = txt;
        
    
    }

