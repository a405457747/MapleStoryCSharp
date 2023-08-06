using System;
using UnityEngine;

namespace MapleStory
{
    public class SaveManager : MonoBehaviour
    {
        private ISave CurSave;


        public SaveMap SaveMap;

        public virtual void Awake()
        {
            CurSave = new UnityJsonSave();


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