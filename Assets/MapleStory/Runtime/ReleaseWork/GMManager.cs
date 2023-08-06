using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MapleStory
{
    public class AutoRunActionAttribute : Attribute
    {
        public string Description { get; }
    }

public class GMManager : MonoBehaviour
{

    public bool IsRelease;

    public virtual void Awake()
    {
        AutoRunFunc();
    }

    [AutoRunAction]
    private static void printClassName()
    {
        Debug.Log("I am GMManager");
    }
    
    /*
    public static void Emit<T>(string funcName, T args) //注册的方法可以不带参数，照样能调用。这个方法对静态方法无效
    {
        List<GameObject> rootObjects = new List<GameObject>(); //只有几个根对象
        SceneManager.GetActiveScene().GetRootGameObjects(rootObjects); //隐藏对象也能获取。Resources.FindObjectsOfTypeAll这个是备选

        rootObjects.Insert(0, Obj<Kit>().gameObject);
        foreach (var gameObject in rootObjects) //可以手动调顺序很爽的。
        {
            gameObject.BroadcastMessage(funcName, args, SendMessageOptions.DontRequireReceiver);
        }
    }
    */

    
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

    string  RunFunc(string ipt)//动态的做函数，避免各种if
    {
        string[] argArr = ipt.Split(" "[0]);
        string funcName = argArr[0];
        if (funcName == "add")
        {
            
        }else if (funcName == "reduce")
        {
            
        }
        //Debug.Log("ipt is "+ipt);
        return "运行成功";
    }
    string cmdStr = "";
    string resultStr = "";
    void OnGUI()
    {
        if (IsRelease==false)
        {
            
            GUI.color = Color.yellow;
            cmdStr = GUI.TextField(new Rect(10, 30, 100, 30), cmdStr, 10);
            if (GUI.Button(new Rect(10, 60, 100, 30), "运行"))
            {
                resultStr=  RunFunc(cmdStr);
            }
            GUI.Label(new Rect(10,90,100,30),resultStr);
        }
    }

    private void FixedUpdate()
    {

        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     print("keydown a");
        // }



    }
}
}