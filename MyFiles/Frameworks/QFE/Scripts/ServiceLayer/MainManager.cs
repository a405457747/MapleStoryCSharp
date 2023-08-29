using QFramework;
using UniRx;
using UnityEngine;

namespace CallPalCatGames.QFrameworkExtension
{
    public enum MainEvent
    {
        Began = QMgrID.Main,
        GameWinOrSave
    }

    //TNTodo : 
    public class MainManager : QMgrBehaviour, ISingleton
    {
        public override int ManagerId => QMgrID.Main;
        public static MainManager Instance => MonoSingletonProperty<MainManager>.Instance;

        public void OnSingletonInit()
        {
            DontDestroyOnLoad(gameObject);
            HotFixManager.Instance.Parent(transform);
            MenuManager.Instance.Parent(transform);
            GuiManager.Instance.Parent(transform);
            SaveManager.Instance.Parent(transform);
            VoiceManager.Instance.Parent(transform);
            LevelManager.Instance.Parent(transform);
            GameManager.Instance.Parent(transform);
            DialogueManager.Instance.Parent(transform);
            PoolManager.Instance.Parent(transform);
            VoiceCtrl();
            RegisterEvent(MainEvent.GameWinOrSave);
        }

        private void Awake()
        {
            ResMgr.Init();
            UIManager.Instance.SetMatchOnWidthOrHeight(1f);

            Instance.gameObject.name = nameof(MainManager);
        }

        public (string title, string content) GetLevelMessage(int id) //获取关卡文字信息
        {
            id += LevelManager.LevelSpan;
            (string title, string content) res;
            /*
            res.title = SaveManager.Instance.IsChineseLanguage()
                ? LevelManager.LevelMessage["Title"][id.ToString()]
                : LevelManager.LevelMessage["TitleEN"][id.ToString()];
                */
            res.title = $"{SaveManager.Instance.ReturnLanguageMessage(28)}{id-LevelManager.LevelSpan+1}";
            res.content = SaveManager.Instance.IsChineseLanguage()
                ? LevelManager.LevelMessage["Introduce"][id.ToString()]
                : LevelManager.LevelMessage["IntroduceEN"][id.ToString()];
            return res;
        }

        public bool IsArriveMaxLevelNum()//是否有条件要去EndScene
        {
            return SaveManager.Instance.SaveMap.CurrentGameLevel.Value == LevelManager.MaxGameLevel &&
                   LevelManager.Instance.GetLevelTailNum() == LevelManager.MaxGameLevel;
        }

        protected override void ProcessMsg(int eventId, QMsg msg)
        {
            switch (eventId)
            {
                case (int) MainEvent.GameWinOrSave:
                    GameWinOrSave();
                    break;
            }
        }

        public void VoiceCtrl()
        {
            SaveManager.Instance.SaveMap.MusicOn.Subscribe(isOn => { VoiceManager.Instance.MusicMute = !isOn; })
                .AddTo(this);
            SaveManager.Instance.SaveMap.SoundOn.Subscribe(ison => { VoiceManager.Instance.SoundMute = !ison; })
                .AddTo(this);
            SaveManager.Instance.AutoSetLanguage();
        }

        private void GameWinOrSave()
        {
            if (SaveManager.Instance.SaveMap.CurrentGameLevel.Value == LevelManager.Instance.GetLevelTailNum())
                SendEvent(SaveEvent.AddCurrentGameLevel);
        }
    }
}