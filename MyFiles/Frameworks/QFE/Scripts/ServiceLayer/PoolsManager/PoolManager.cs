using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace CallPalCatGames.QFrameworkExtension
{
    public enum PoolEvent
    {
        Began = QMgrID.Pool,
        DeSpawnAll,
        Spawn,//这个事件很奇怪，因为有可能要设置他的位置，靠事件获取不到对象
        DeSpawn
    }

    public class SpawnQMsg : QMsg
    {
        public SpawnQMsg() : base((int) PoolEvent.Spawn)
        {
        }

        public string Name { get; set; }
    }

    public class DeSpawnQMsg : QMsg
    {
        public DeSpawnQMsg() : base((int) PoolEvent.Spawn)
        {
        }

        public GameObject Go { get; set; }
    }

    public class PoolManager : QMgrBehaviour, ISingleton
    {
        private LoadHelper _loadHelper;
        private Dictionary<string, Pool> _pools;
        public override int ManagerId => QMgrID.Pool;
        public static PoolManager Instance => MonoSingletonProperty<PoolManager>.Instance;

        public void OnSingletonInit()
        {
            _pools = new Dictionary<string, Pool>();
            _loadHelper = gameObject.GetOrAddComponent<LoadHelper>();
            RegisterEvent(PoolEvent.DeSpawnAll);
            RegisterEvent(PoolEvent.Spawn);
            RegisterEvent(PoolEvent.DeSpawn);
        }

        protected override void ProcessMsg(int eventId, QMsg msg)
        {
            switch (msg.EventID)
            {
                case (int) PoolEvent.DeSpawnAll:
                    DeSpawnAll();
                    break;
                case (int) PoolEvent.Spawn:
                    if (msg is SpawnQMsg tempMsg1) Spawn(tempMsg1.Name);
                    break;
                case (int) PoolEvent.DeSpawn:
                    if (msg is DeSpawnQMsg tempMsg2) DeSpawn(tempMsg2.Go);
                    break;
            }
        }

        public GameObject Spawn(string name)
        {
            if (!_pools.ContainsKey(name))
            {
                var pool = new Pool();
                _pools.Add(name, pool);
                return pool.Spawn(name, _loadHelper);
            }

            return _pools[name].Spawn(name, _loadHelper);
        }

        public void DeSpawn(GameObject go) //回收单池单个
        {
            if (_pools.ContainsKey(go.name))
            {
                _pools[go.name].DeSpawn(go);
            }
        }
        
        private void DeSpawnAll() //回收所有池所有个
        {
            foreach (var pool in _pools.Values) pool.DeSpawnAll();
        }
    }
}