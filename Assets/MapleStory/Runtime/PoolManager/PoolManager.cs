using System.Collections.Generic;
using UnityEngine;

namespace MapleStory
{
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
                tempGo = Object.Instantiate(AppRoot.Instance._resManager.LoadGameObject(name));

                tempGo.name = name;
            }
            else
            {
                tempGo = _objs.Dequeue();
            }

            tempGo.SetActive(true);
            tempGo.GetComponent<IPool>().OnSpawn();

            return tempGo;
        }

        public void Recycle(GameObject obj)
        {
            obj.GetComponent<IPool>().OnRecycle();
            obj.SetActive(false);

            if (_objs.Contains(obj))
                Debug.LogError("Already existed.");
            else
                _objs.Enqueue(obj);
        }
    }

    public interface IPool
    {
        void OnRecycle();
        void OnSpawn();
    }

    public class PoolManager : MonoBehaviour
    {
        private Dictionary<string, Pool> _pools;

        public virtual void Awake()
        {
            _pools = new Dictionary<string, Pool>();
        }

        public GameObject SpawnPoolObj(string goName)
        {
            if (!_pools.ContainsKey(goName))
            {
                var tempPool = new Pool();
                _pools.Add(goName, tempPool);
            }

            return _pools[goName].Spawn(goName);
        }

        public void RecyclePoolObj(GameObject obj)
        {
            var goName = obj.name;
            if (_pools.ContainsKey(goName))
                _pools[goName].Recycle(obj);
            else
                Debug.LogException(new KeyNotFoundException());
        }
    }


}