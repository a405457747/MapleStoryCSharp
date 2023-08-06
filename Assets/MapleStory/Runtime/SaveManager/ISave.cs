using System;

namespace MapleStory
{
    [Serializable]
    public class SaveMap
    {
        // public UserSet userSet = new UserSet();
        // public  object k2 = new { name = "bill", age = 32 };
        public SaveMap()
        {

        }
    }


    public interface ISave
    {
        string SaveFileName { get; set; }

        SaveMap SaveMap { get; set; }
        void LoadData();
        void SaveData();
        string GetSaveMapString();
    }
}