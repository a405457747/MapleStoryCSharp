using System;
using System.Collections.Generic;
using UnityEngine;

namespace PurpleGarlic
{
    //对话系统只是一个子Panel
    public class GUIManager : MonoSingleton<GUIManager>
    {
        private readonly Stack<Panel> _panels = new Stack<Panel>();

        public T GetPanel<T>() where T : Panel
        {
            T res = null;

            foreach (var panel in _panels)
                if (panel is T)
                {
                    res = (T) panel;
                    break;
                }

            return res;
        }

        public void ForwardPaenl(string name)
        {
            var uiObj = Instantiate(GetPanelGameObject(name));

            if (uiObj == null) Debug.LogError("Didn't find  The UIPanelPrefab.");

            uiObj.transform.SetParent(transform, false);

            var uiPanel = uiObj.GetComponent<Panel>();

            if (uiPanel == null) Debug.LogException(new NullReferenceException());

            if (_panels.Contains(uiPanel))
                Debug.LogError("Already existed.");
            else
                _panels.Push(uiPanel);
        }

        public void BackPanel()
        {
            if (_panels.Count != 0)
                Destroy(_panels.Pop().gameObject);
            else
                Debug.LogError("The stack is empty.");
        }

        private GameObject GetPanelGameObject(string name)
        {
            GameObject res = null;
            /*foreach (var panlObj in MainManager.GetResData().Panels)
            {
                if (panlObj.name == name)
                {
                    res = panlObj;
                    break;
                }
            }*/
            return res;
        }

        public void PanelsSwitch(List<Panel> closePanels, List<Panel> openPanels)
        {
        }
    }

    public class Panel : MonoBehaviour
    {
    }
}