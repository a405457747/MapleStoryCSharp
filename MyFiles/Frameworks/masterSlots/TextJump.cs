using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using  UnityEngine.UI;

public class TextJump : MonoBehaviour
{
    private Text _txt;
    public float costTime = 0.5f;

    private void Awake()
    {
        _txt = GetComponent<Text>();
    }

    private void Start()
    {
       // _txt = GetComponent<Text>();
       // Log.LogParas("costTime is"+costTime);
    }

    public void TxtJump( float newVal,bool isFixed =false,bool isReset =false,float delay =0.5f,bool isHopeFixed =false)
    {
        if (isFixed)
        {
            costTime = Game.I.SuPerSlotsAndCoreAndBrainsAndCoreGoodModel.GetHopeTime(delay);
            if (isHopeFixed)
            {
                costTime = delay;
            }
        }

        if (isReset)
        {
            _txt.text = 0.ToString();
        }
        
        var b = float.TryParse(_txt.text, out float oldVal);
        if (b==false)
        {
            oldVal = default;
        }
        
        DOTween.To(delegate(float f)
            {
                _txt.text = Tool.ThousandsCoin((int) f);
            },

            oldVal, newVal,
            costTime);
    }
}
