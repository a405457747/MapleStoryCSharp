using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MapleStory
{
    public interface IPoolObj
    {
        void OnRecycle();
        void OnSpawn();
    }

    internal class GoPool
    {
        private readonly Queue<GameObject> _objs = new Queue<GameObject>();

        public GameObject SpawnGo(string poolObjPath)
        {
            GameObject tempGo;
            if (_objs.Count == 0)
            {
                tempGo = GameObject.Instantiate(Resources.Load<GameObject>(poolObjPath));

                tempGo.name = poolObjPath;
            }
            else
            {
                tempGo = _objs.Dequeue();
            }

            tempGo.SetActive(true);

            tempGo.GetComponent<IPoolObj>().OnSpawn();
            return tempGo;
        }

        public void RecycleGo(GameObject obj)
        {
            obj.GetComponent<IPoolObj>().OnRecycle();
            obj.SetActive(false);

            if (_objs.Contains(obj))
            {
                Debug.LogFormat("The queue have the obj {0}", obj);
            }
            else
            {
                _objs.Enqueue(obj);
            }
        }
    }


    public class PoolMgr : MonoBehaviour
    {
        internal static PoolMgr Inst;

        private Dictionary<string, GoPool> _goPools = new Dictionary<string, GoPool>();

        private void Awake()
        {
            if(Inst==null)     Inst = this;
        }

        private void Start()
        {
        }

        internal GameObject SpawnGo(string poolObjPath)
        {
            if (!_goPools.ContainsKey(poolObjPath))
            {
                var tempPool = new GoPool();
                _goPools.Add(poolObjPath, tempPool);
            }

            return _goPools[poolObjPath].SpawnGo(poolObjPath);
        }

        internal void RecycleGo(GameObject obj)
        {
            var goName = obj.name;

            if (_goPools.ContainsKey(goName))
            {
                _goPools[goName].RecycleGo(obj);
            }
            else
            {
                Debug.LogFormat("The dict don't have the key {0}", goName);
            }
        }
    }
}