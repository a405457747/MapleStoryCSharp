using System.Collections.Generic;
using UnityEngine;

public class PoolSystem : GameSystem
{
    private readonly Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();

    public PoolSystem(Game game) : base(game)
    {
    }

//auto
    private void Awake()
    {
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public GameObject Spawn(string goName)
    {
        if (!_pools.ContainsKey(goName))
        {
            var tempPool = new Pool();
            _pools.Add(goName, tempPool);
        }

        return _pools[goName].Spawn(goName);
    }

    public void Recycle(GameObject obj)
    {
        var goName = obj.name;
        if (_pools.ContainsKey(goName))
            _pools[goName].Recycle(obj);
        else
            Log.LogException(new KeyNotFoundException());
    }
}