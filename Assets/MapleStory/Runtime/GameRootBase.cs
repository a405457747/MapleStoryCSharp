using System;
using System.Collections;
using System.Collections.Generic;
using MapleStory;
using UnityEngine;

namespace MapleStory
{
    public enum PanelTier
    {
        Null,
        Default,
        PopUp,
        AlwaysInFront,
        Effect,
        Guide
    }

    public interface ISystem
    {
        void OnInit();
        void OnFree();
    }


//为了游戏梦想，以后的api接口尽量两个名字，并且不替换，而且工具类只增不删，如果别人的代码只要自己修改了，就算自己的。
//Unity架构本身很好用了，有必要的时候还是要用virtual组件，而不是抽象重系统。重系统是全局组件哈哈。

//todo warn UI组件挂着引用重命名很危险，预制体引用会丢失
    public class GameRootBase : MonoBehaviour
    {
        private readonly Dictionary<string, MonoBehaviour> _views = new Dictionary<string, MonoBehaviour>();
     

        private Transform canvasTrans;

        public ResManager _resManager { get; private set; }

        private SaveManager _saveManager; 
        
        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);

            canvasTrans = GameObject.Find("Canvas").transform;

            _resManager = this.gameObject.AddComponent<ResManager>();
            _saveManager = this.gameObject.AddComponent<SaveManager>();
        }


        protected virtual void FixedUpdate()
        {

        }


        public void InitSystem<T>()
        {
        }

        public void FreeSystem<T>()
        {
        }



        public T OpenPanel<T>(PanelTier panelTier = PanelTier.Default) where T : MonoBehaviour
        {
            var viewName = typeof(T).Name;
            if (_views.ContainsKey(viewName) == false)
            {
                var go = Instantiate(_resManager.LoadGameObject(viewName));
                go.name = viewName;

                var t = go.GetComponent<T>(); //<T>();
                if (t == null) t = go.AddComponent<T>();

                t.transform.SetParent(canvasTrans.Find(panelTier.ToString()), false);
                //t.Initialize(viewName);
                _views[viewName] = t;
            }

            var res = _views[viewName] as T;

            res.gameObject.SetActive(true);

            return res;
        }

        public T ClosePanel<T>(PanelTier panelTier = PanelTier.Default) where T : MonoBehaviour
        {
            var viewName = typeof(T).Name;

            var res = _views[viewName] as T;

            res.gameObject.SetActive(false);

            return res;
        }
    }
}