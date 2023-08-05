using System.Collections.Generic;
using UnityEngine;

public interface IPool
{
    void OnSpawn();
    void OnRecycle();
}

public class Pool
{
    private readonly Queue<GameObject> _objs;

    public Pool()
    {
        _objs = new Queue<GameObject>();
    }

    public GameObject Spawn(string name)
    {
        GameObject tempGo;
        if (_objs.Count == 0)
        {
            tempGo = Object.Instantiate(Factorys.GetAssetFactory().LoadPanel(name));
            tempGo.Name(name);
        }
        else
        {
            tempGo = _objs.Dequeue();
        }

        tempGo.SetActive(true);
        tempGo.SendMessage("OnSpawn", SendMessageOptions.RequireReceiver);

        return tempGo;
    }

    public void Recycle(GameObject obj)
    {
        obj.SendMessage("OnRecycle");
        obj.SetActive(false);

        if (_objs.Contains(obj))
            Log.LogError("Already existed.");
        else
            _objs.Enqueue(obj);
    }
}