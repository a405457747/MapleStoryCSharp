using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;


public interface UI
{
    void Show();
    void Hide();
}

//最方便的方法才有资格放在这里，不能太多了
public static class Kit2
{

    
    //这是深度优先，不能用广度。
    public static T Find<T>(this MonoBehaviour mono, string targetName) where T : Component
    {
        if (mono.name.StartsWith(targetName))
            return mono.GetComponent<T>(); //先看自己满足不，用Startswith会有(clone)后缀。
        
        
        Transform FindTrans(Transform aParent, string aName) //本地函数用不用static，我看官网示例没有用。
        {
            foreach (Transform child in aParent)
            {
                if (child.name == aName) return child;
                var result = FindTrans(child, aName);
                if (result != null) return result;
            }

            return null;
        }

        var t = FindTrans(mono.transform, targetName);
        return t != null ? t.GetComponent<T>() : default;
    }

    public static void Delay(this MonoBehaviour mono, Action callback, float animalCost = 0.02f) //延迟一帧
    {
        IEnumerator delay()
        {
            yield return new WaitForSeconds(animalCost);
            callback?.Invoke();
        }

        mono.StartCoroutine(delay());
    }

    //UI类型的MonoBehaviour用的
    public static void ShowUI(this MonoBehaviour mono)
    {
        CanvasGroup cg = Kit.GetOrAddComponent<CanvasGroup>(mono.gameObject);
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    //UI类型的MonoBehaviour用的
    public static void HideUI(this MonoBehaviour mono, float animalCost = 0f)
    {
        mono.Delay(() =>
        {
            CanvasGroup cg = Kit.GetOrAddComponent<CanvasGroup>(mono.gameObject);
            cg.alpha = 0;
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }, animalCost);
    }

    //Component或者非UI类型的MonoBehaviour用的
    public static void Show(this GameObject mono, float animalCost = 0f)
    {
        mono.gameObject.SetActive(true);
    }

    //Component或者非UI类型的MonoBehaviour用的
    public static void Hide(this GameObject mono, float animalCost = 0f) => mono.GetComponent<MonoBehaviour>()
        .Delay(() => { mono.gameObject.SetActive(false); }, animalCost);
    
    public static int Idx(this MonoBehaviour mono)// todo 可能移除呢
    {
        return mono.transform.GetSiblingIndex();
    }
}
