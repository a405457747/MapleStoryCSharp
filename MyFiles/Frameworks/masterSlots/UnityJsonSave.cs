using UnityEngine;

public class UnityJsonSave : ISave
{
    public SaveMap SaveMap { get; set; }

    public void Load()
    {
        var tempStr = GetSaveMapString();
        SaveMap = tempStr == "" ? new SaveMap() : JsonUtility.FromJson<SaveMap>(tempStr);
        
    }

    public void Save()
    {
        PlayerPrefs.SetString("SaveKey", JsonUtility.ToJson(SaveMap));
        Log.LogParas("Save success and the saveMap is"+GetSaveMapString());
    }

    public string GetSaveMapString()
    {
        return PlayerPrefs.GetString("SaveKey", "");
    }
}