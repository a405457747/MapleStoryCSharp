using System;
using UnityEngine;

namespace MapleStory
{
    public interface ISave
    {
        string SaveFileName { get; set; }
        SaveMap SaveMap { get; set; }
        void LoadData();
        void SaveData();
        string GetSaveMapString();
    }

    [Serializable]
    public class SaveMap
    {
        public UserSet userSet = new UserSet();

        public SaveMap()
        {
        }
    }

    public class SaveManager : MonoBehaviour
    {
        private ISave CurSave;
        public SaveMap SaveMap;

        public virtual void Awake()
        {
            CurSave = new JsonWaySave();

            LoadData();

            SaveMap = CurSave.SaveMap;
        }

        private void LoadData()
        {
            CurSave.LoadData();
        }

        public void SaveData()
        {
            CurSave.SaveData();
        }

        private string GetSaveMapString()
        {
            return CurSave.GetSaveMapString();
        }
    }
}