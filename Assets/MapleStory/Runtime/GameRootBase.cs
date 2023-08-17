using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MapleStory;
using UniRx;
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




//todo warn UI组件挂着引用重命名很危险，预制体引用会丢失
    public class GameRootBase : MonoBehaviour
    {
        private readonly Dictionary<string, MonoBehaviour> _views = new Dictionary<string, MonoBehaviour>();
        private Transform canvasTrans;

        public ResManager resManager { get; private set; }
        public SaveManager saveManager { get; private set; }
        public AudioManager audioManager { get; private set; }
        public MenuManager menuManager { get; private set; }
        public PoolManager poolManager { get; private set; }
        
        public LogoPanel logoPanel { get; private set; }
        public GMManager gmManager { get; private set; }
        
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
                var go = Instantiate( Resources.Load<GameObject>(Path.Combine("UI", viewName)));
                go.name = viewName;

                var t = go.GetComponent<T>(); 
                if (t == null) t = go.AddComponent<T>();

                t.transform.SetParent(canvasTrans.Find(panelTier.ToString()), false);
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
        
        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);

            canvasTrans = GameObject.Find("Canvas").transform;
            
            logoPanel = OpenPanel<LogoPanel>();
            
            resManager = this.gameObject.AddComponent<ResManager>();
            saveManager = this.gameObject.AddComponent<SaveManager>();
            audioManager = this.gameObject.AddComponent<AudioManager>();
            menuManager = this.gameObject.AddComponent<MenuManager>();
            poolManager = this.gameObject.AddComponent<PoolManager>();
            gmManager = gameObject.AddComponent<GMManager>();

            //LogoShow();
        }

        void LogoShow()
        {
            logoPanel.mainImageSprite(resManager.LoadSprite("logo"));
            Observable.Timer(TimeSpan.FromSeconds(2)).Subscribe(_ =>
            {
                ClosePanel<LogoPanel>();
            });
        }
        

    }
}