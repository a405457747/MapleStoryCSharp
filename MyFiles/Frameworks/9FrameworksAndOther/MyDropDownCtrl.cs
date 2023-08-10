using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using QFramework;

namespace xmaolol.com
{

    public class MyDropDownCtrl : MonoSingleton<MyDropDownCtrl>
    {

        public static event Action LanguageChangeHandler;

        private Dropdown dropdown;

        private void Start()
        {
            dropdown = transform.parent.GetComponent<Dropdown>();
            dropdown.value = MySaveManager.Instance.SaveMapping.LanguageKey;
        }

        public void Show()
        {
            int value = dropdown.value;
            if (value == 0)
            {
                MySaveManager.Instance.SaveMapping.LanguageKey = 0;
            }
            else
            if (value == 1)
            {
                MySaveManager.Instance.SaveMapping.LanguageKey = 1;
            }
            else
            if (value == 2)
            {
                MySaveManager.Instance.SaveMapping.LanguageKey = 2;
            }

            MySaveManager.Instance.Save();

            if (LanguageChangeHandler != null)
            {
                LanguageChangeHandler();
            }
        }
    }
}

