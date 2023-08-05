using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Model : MonoBehaviour, ICall
{
    public string Name;
    public bool IsShow;
    public Game game;

/*    public void log(string str)
    {
        Debug.Log($"{Name} {str}");
    }*/

    public virtual void EachFrame()
    {
    }
    public virtual void Hide()
    {
        IsShow = false;
    }
    public virtual void Initialize(string name)
    {
        game = Game.I;
        Name = name;
        Hide();
    }
    public virtual void Release() { }
    public virtual void Show()
    {
        IsShow = true;
    }

    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}
