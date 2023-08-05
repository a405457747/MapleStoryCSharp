using System.Collections.Generic;
using UnityEngine;


public partial class PoolsManage : MonoSingleton<PoolsManage>
{
    private readonly Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();


    protected override void Start()
    {
        base.Start();
    }

    public GameObject Spawn(string name)
    {
        if (!_pools.ContainsKey(name))
        {
            var tempPool = new Pool();
            _pools.Add(name, tempPool);
        }
        DebugText.Instance.ShowInfo("Spwan success");
        return _pools[name].Spawn(name);
    }

    public void Recycle(GameObject obj)
    {
        var name = obj.name;
        if (_pools.ContainsKey(name))
            _pools[name].Recycle(obj);
        else
            Debug.LogException(new KeyNotFoundException());
    }
}

public class Pool
{
    private readonly Queue<GameObject> _objs = new Queue<GameObject>();

    public GameObject Spawn(string name)
    {
        GameObject tempGo;
        if (_objs.Count == 0)
        {
            tempGo = GameObject.Instantiate(ResManager.Instance.GetGameObject(name));
            DebugText.Instance.ShowInfo(tempGo.ToString());
            tempGo.name = name;
        }
        else
        {
            tempGo = _objs.Dequeue();
        }

        tempGo.SetActive(true);
        tempGo.SendMessage("OnSpawn");

        return tempGo;
    }

    public void Recycle(GameObject obj)
    {
        obj.SendMessage("OnRecycle");
        obj.SetActive(false);

        if (_objs.Contains(obj))
            Debug.LogError("already existed.");
        else
            _objs.Enqueue(obj);
    }
}