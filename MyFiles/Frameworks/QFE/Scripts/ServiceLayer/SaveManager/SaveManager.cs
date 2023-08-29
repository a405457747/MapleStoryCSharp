using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace CallPalCatGames.QFrameworkExtension
{
    public enum SaveEvent
    {
        Began = QMgrID.Save,
        Save,
        CreateNewSaveMap,
        AddCurrentGameLevel
    }

    public class SaveManager : QMgrBehaviour, ISingleton
    {
        private Dictionary<string, Dictionary<string, string>> _languageMessage;
        private LoadHelper _loadHelper;
        public SaveMap SaveMap { get; private set; }
        public override int ManagerId => QMgrID.Save;
        public static SaveManager Instance => MonoSingletonProperty<SaveManager>.Instance;

        public void OnSingletonInit()
        {
            Load();
            _loadHelper = gameObject.GetOrAddComponent<LoadHelper>();
            Tool.LoadCsvTxt(_loadHelper.LoadThing<TextAsset>("LanguageCfg").text, out _languageMessage);
            RegisterEvent(SaveEvent.Save);
            RegisterEvent(SaveEvent.CreateNewSaveMap);
            RegisterEvent(SaveEvent.AddCurrentGameLevel);
        }

        public string ReturnLanguageMessage(int id)
        {
            var message = "";
            switch (SaveMap.LanguageKey.Value)
            {
                case 0:
                    message = _languageMessage["ChineseText"][id.ToString()];
                    break;
                case 1:
                    message = _languageMessage["EnglishText"][id.ToString()];
                    break;
                case 2:
                    message = _languageMessage["TradChinese"][id.ToString()];
                    break;
                case 3:
                    message = _languageMessage["Korean"][id.ToString()];
                    break;
                case 4:
                    message = _languageMessage["Japanese"][id.ToString()];
                    break;
                case 5:
                    message = _languageMessage["Indonesian"][id.ToString()];
                    break;
                case 6:
                    message = _languageMessage["German"][id.ToString()];
                    break;
                case 7:
                    message = _languageMessage["French"][id.ToString()];
                    break;
                case 8:
                    message = _languageMessage["Spanish"][id.ToString()];
                    break;
                case 9:
                    message = _languageMessage["Arabic"][id.ToString()];
                    break;
                case 10:
                    message = _languageMessage["Russian"][id.ToString()];
                    break;
                case 11:
                    message = _languageMessage["Vietnamese"][id.ToString()];
                    break;
                case 12:
                    message = _languageMessage["Portuguese"][id.ToString()];
                    break;
            }
            return message;
        }

        public void PrintPlayerPrefs()
        {
            var saveMapStr = JsonUtility.ToJson(SaveMap);
            print(saveMapStr);
        }

        protected override void ProcessMsg(int key, QMsg msg)
        {
            switch (msg.EventID)
            {
                case (int) SaveEvent.Save: //set关闭，
                    Save();
                    break;
                case (int) SaveEvent.CreateNewSaveMap: //重新开始
                    CreateNewSaveMap();
                    MainManager.Instance.VoiceCtrl();//这里不完美，但是将就吧，没有完美的代码呢，只要清晰就好了
                    break;
                case (int) SaveEvent.AddCurrentGameLevel: //闯了新关卡
                    AddCurrentGameLevel();
                    break;
            }
        }

        private void Load()
        {
            var getStr = PlayerPrefs.GetString("SaveMap", "");
            if (getStr == "")
                CreateNewSaveMap();
            else
                SaveMap = JsonUtility.FromJson<SaveMap>(getStr);
        }

        private void Save()
        {
            PlayerPrefs.SetString("SaveMap", JsonUtility.ToJson(SaveMap));
        }

        private void AddCurrentGameLevel()
        {
            if (SaveMap.CurrentGameLevel.Value < LevelManager.MaxGameLevel)
            {
                //SaveMap.TimestampSeconds.Value = Tool.GetUnixStartToNowTimeTotal(TimeTotalType.Second);
                SaveMap.CurrentGameLevel.Value += 1;
                Save();
            }
        }

        private void CreateNewSaveMap()
        {
            SaveMap = new SaveMap();
            Save();//这里的保存不冗余
        }

        public void AutoSetLanguage()
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                case SystemLanguage.ChineseTraditional:
                    SetLanguageKey(0);
                    break;
                default:
                    SetLanguageKey(1);
                    break;
            }
        }

        public bool IsChineseLanguage()
        {
            return SaveMap.LanguageKey.Value == 0;
        }

        private void SetLanguageKey(int languageKey)
        {
            SaveMap.LanguageKey.Value = languageKey;
        }
    }
}