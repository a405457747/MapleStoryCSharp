using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour, ICall
{
    public Game game;
    public string Name;
    public bool IsShow;

    public List<string> AttentionEvents = new List<string>();

    public abstract void HandleEvent(string eventName, object data);

/*    public void log(string str)
    {
        Log.LogPrint($"{Name} {str}");
    }*/

    protected Model GetModel<T>() where T : Model
    {
        return MVC.GetModel<T>();
    }

    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }

    public virtual void Initialize(string name)
    {
        game = Game.I;
        Name = name;
        Hide();
        var preview = transform.Find("ImagePreview");
        if (preview!=null)
        {
        preview.gameObject.SetActive(false);
        }

    }

    public virtual void Release()
    {
    }

    public virtual void EachFrame()
    {
    }

    public virtual void Show()
    {
        IsShow = true;
        gameObject.SetActive(IsShow);
    }

    public virtual void Hide()
    {
        IsShow = false;
        gameObject.SetActive(IsShow);
    }
}