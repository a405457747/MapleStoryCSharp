using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MapleStory
{
    public class SceneryManager : MonoBehaviour
    {
        public virtual void Awake()
        {

        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public string GetCurrentSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }
    }
}