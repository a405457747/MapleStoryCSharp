using UnityEngine;
using System.IO;
using System.Text;

using MapleStory;

namespace MapleStory
{
    public class UnityJsonSave : ISave
    {

        public string SaveFileName { get; set; } = "saveMap.txt";
        public SaveMap SaveMap { get; set; }

        public void LoadData()
        {

            var saveFile = System.IO.Path.Combine(Application.persistentDataPath, SaveFileName);

            if (File.Exists(saveFile) == false)
            {
                File.WriteAllText(saveFile, "", new UTF8Encoding(false));
            }

            var tempStr = GetSaveMapString();




            SaveMap = tempStr == ""
                ? new SaveMap()
                : JsonTool<SaveMap>.CurTool.FromJson(tempStr);// JsonMapper.ToObject<SaveMap>(tempStr); // JsonUtility.FromJson<SaveMap>(tempStr);
        }

        public void SaveData()
        {
            //PlayerPrefs.SetString("SaveKey", JsonUtility.ToJson(SaveMap));

            var jsonStr =JsonTool<SaveMap>.CurTool.ToJson(SaveMap,false);//  JsonMapper.ToJson(SaveMap); //JsonUtility.ToJson(SaveMap);
            File.WriteAllText(System.IO.Path.Combine(Application.persistentDataPath, SaveFileName), jsonStr,
                new UTF8Encoding(false));
            LogNote.Info("SaveData");
        }

        public string GetSaveMapString()
        {
            return File.ReadAllText(System.IO.Path.Combine(Application.persistentDataPath, SaveFileName),
                new UTF8Encoding(false));
            // return PlayerPrefs.GetString("SaveKey", "");
        }
    }
}