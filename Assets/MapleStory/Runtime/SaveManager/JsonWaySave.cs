using UnityEngine;
using System.IO;
using System.Text;
using MapleStory;

namespace MapleStory
{
    public class JsonWaySave : ISave
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
                : JsonTool<SaveMap>.CurTool
                    .FromJson(tempStr);
        }

        public void SaveData()
        {
            var jsonStr = JsonTool<SaveMap>.CurTool.ToJson(SaveMap, false);

            File.WriteAllText(System.IO.Path.Combine(Application.persistentDataPath, SaveFileName), jsonStr,
                new UTF8Encoding(false));
            
            LogNote.Info("SaveData Success!");
        }

        public string GetSaveMapString()
        {
            return File.ReadAllText(System.IO.Path.Combine(Application.persistentDataPath, SaveFileName),
                new UTF8Encoding(false));
        }
    }
}