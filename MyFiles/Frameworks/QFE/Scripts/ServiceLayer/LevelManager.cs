using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CallPalCatGames.QFrameworkExtension
{
    public enum LevelEvent
    {
        Began = QMgrID.Level,
        LoadMainScene,
        LoadLevelSelectScene,
        LoadEndScene,
        LoadLevel
    }

    public class LevelQMsg : QMsg
    {
        public LevelQMsg(int level) : base((int) LevelEvent.LoadLevel)
        {
            Level = level;
        }

        public int Level { get; set; }
    }

    public class LevelManager : QMgrBehaviour, ISingleton
    {
        public static int MaxGameLevel = 5;
        public const int LevelSpan = 30;
        public static Dictionary<string, Dictionary<string, string>> LevelMessage;
        private LoadHelper _loadHelper;
        public override int ManagerId => QMgrID.Level;
        public static LevelManager Instance => MonoSingletonProperty<LevelManager>.Instance;

        public void OnSingletonInit()
        {
            _loadHelper = gameObject.GetOrAddComponent<LoadHelper>();
            Tool.LoadCsvTxt(_loadHelper.LoadThing<TextAsset>("LevelCfg").text, out LevelMessage);
            RegisterEvent(LevelEvent.LoadMainScene);
            RegisterEvent(LevelEvent.LoadLevelSelectScene);
            RegisterEvent(LevelEvent.LoadEndScene);
            RegisterEvent(LevelEvent.LoadLevel);
        }

        protected override void ProcessMsg(int key, QMsg msg)
        {
            switch (msg.EventID)
            {
                case (int) LevelEvent.LoadMainScene:
                    LoadSceneAsync(1);
                    break;
                case (int) LevelEvent.LoadLevelSelectScene:
                    LoadSceneAsync(2);
                    break;
                case (int) LevelEvent.LoadEndScene:
                    LoadSceneAsync(5);
                    break;
                case (int) LevelEvent.LoadLevel:
                    var levelMsg = msg as LevelQMsg;
                    LoadSceneAsync(levelMsg.Level + LevelSpan);
                    break;
            }
        }

        private void LoadSceneAsync(int sceneId) //异步加载场景
        {
            var sceneName = LevelMessage["LevelSceneName"][sceneId.ToString()];
            LoadSceneAsync(sceneName);
        }

        private void LoadSceneAsync(string sceneName)
        {
            UIMgr.OpenPanel<LoadingPanel>(UILevel.Forward,
                new LoadingPanelData {NextSceneName = sceneName}, null, nameof(LoadingPanel));
        }

        private static string GetCurrentLevelName() //获取当前场景名字
        {
            return SceneManager.GetActiveScene().name;
        }

        public int GetLevelTailNum() //获取当前场景名字的尾巴
        {
            return Tool.GetNumberByString(GetCurrentLevelName());
        }

        /*
        private static int GetAnalysisCsvTxtSceneId(string sceneName) //根据场景名字获取场景的ID，舍去应该是工具那里的泛型方法
        {
            var result = 0;
            foreach (var smallDicKeyAndValue in LevelMessage["LevelSceneName"])
                if (sceneName == smallDicKeyAndValue.Value)
                {
                    result = int.Parse(smallDicKeyAndValue.Key);
                    break;
                }

            if (result == 0) Log.E("错误id至少是1");
            return result;
        }
        */

        /*public static Sprite GetSpriteWithCurrentSceneLevelAndMaxLevelRate(Sprite[] sprites) //这个方法需要的，但是现在可能不对，因为有LevelSpan
        {
            var index = GetAnalysisCSVTxtSceneID(GetCurrentLevelName()) / (MaxGameLevel / sprites.Length) - 1;
            index = Mathf.Clamp(index, 0, sprites.Length - 1);
            return sprites[index];
        }*/
    }
}