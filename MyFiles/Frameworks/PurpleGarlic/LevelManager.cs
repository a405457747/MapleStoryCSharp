/*⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵
 ☠ ©2020 CallPalCatGames. All rights reserved.                                                                        
 ⚓ Author: Sky_Allen                                                                                                                  
 ⚓ Email: 894982165@qq.com                                                                                                  
 ⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PurpleGarlic
{
    /// <summary>
    ///     场景管理
    /// </summary>
    public class LevelManager : MonoSingleton<LevelManager>
    {
        private Dictionary<int, string> _sceneNames;


        /// <summary>
        ///     加载场景
        /// </summary>
        /// <param name="sceneKey"></param>
        public void LoadScene(int sceneKey)
        {
            if (_sceneNames.ContainsKey(sceneKey))
                SceneManager.LoadScene(_sceneNames[sceneKey]);
            else
                Log.LogException(new KeyNotFoundException());
        }

        //获取当前场景的名字
        public string GetCurrentLevelName()
        {
            return SceneManager.GetActiveScene().name;
        }
    }

    /// <summary>
    ///     场景的入口
    /// </summary>
    public abstract class SceneEntrance : MonoBehaviour
    {
        protected virtual void Start()
        {
        }

        protected void Update()
        {
        }
    }
}