using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MapleStory
{
    public class AutoRunActionAttribute : Attribute
    {
        public string Description { get; }
    }

    public class GMManager : MonoBehaviour
    {
        string cmdStr = "";
        string resultStr = "";

        public bool IsRelease;

        public virtual void Awake()
        {
            AutoRunFunc();
        }

        [AutoRunAction]
        private static void Func1()
        {

        }

        private void AutoRunFunc()
        {
            var asm = Assembly.GetExecutingAssembly();
            var types = asm.GetTypes();

            foreach (var t in types)
            {
                var _methods = t.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);

                foreach (var m in _methods)
                {
                    var _attrs = m.GetCustomAttributes(typeof(AutoRunActionAttribute), false);
                    if (_attrs.Length > 0)
                    {
                        m.Invoke(null, new object[] { });
                        break;
                    }
                }
            }
        }

        string RunFunc(string ipt)
        {
            string[] argArr = ipt.Split(" "[0]);
            string funcName = argArr[0];
            if (funcName == "add")
            {
            }
            else if (funcName == "reduce")
            {
            }

            return "运行成功";
        }

        void OnGUI()
        {
            if (IsRelease == false)
            {
                GUI.color = Color.yellow;
                cmdStr = GUI.TextField(new Rect(10, 30, 100, 30), cmdStr, 10);
                if (GUI.Button(new Rect(10, 60, 100, 30), "运行"))
                {
                    resultStr = RunFunc(cmdStr);
                }
                GUI.Label(new Rect(10, 90, 100, 30), resultStr);
            }
        }

        private void FixedUpdate()
        {
        }
    }
}