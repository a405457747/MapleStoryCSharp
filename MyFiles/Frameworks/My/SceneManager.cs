/*⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵
 ☠ ©2020 CallPalCatGames. All rights reserved.                                                                        
 ⚓ Author: Sky_Allen                                                                                                                  
 ⚓ Email: 894982165@qq.com                                                                                                  
 ⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵*/

using System.Collections.Generic;
using CallPalCatGames.Singleton;
using UnityEngine;
using XLua;
using Manager = UnityEngine.SceneManagement.SceneManager;

namespace CallPalCatGames.SceneManager
{
    /// <summary>
    ///     场景管理
    /// </summary>
    [LuaCallCSharp]
    public class SceneManager : MonoSingleton<SceneManager>
    {
        private Dictionary<int, string> _sceneNames;

        protected override void Awake()
        {
            base.Awake();
            _sceneNames = new Dictionary<int, string>();
        }

        /// <summary>
        ///     加载场景
        /// </summary>
        /// <param name="sceneKey"></param>
        public void LoadScene(int sceneKey)
        {
            if (_sceneNames.ContainsKey(sceneKey))
                Manager.LoadScene(_sceneNames[sceneKey]);
            else
                Log.Log.LogException(new KeyNotFoundException());
        }

        //获取当前场景的名字
        public string GetCurrentLevelName()
        {
            return Manager.GetActiveScene().name;
        }

        public void StartLoadGamePlayScene()
        {
            Manager.LoadScene("GamePlay2");
        }
    }

    /// <summary>
    ///     场景的入口
    /// </summary>
    public abstract class SceneEntrance : MonoBehaviour
    {
        protected virtual void Awake()
        {
        }

        protected void OnDestroy()
        {
        }
    }
}