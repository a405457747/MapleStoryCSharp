using System;
using UnityEngine;

public class SaveManager : MonoSingleton<SaveManager>
{
    public SaveMap SaveMap { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Load();
    }

    protected override void OnDestroy()
    {
        Save();
        //Log.LogPrint("Save");
    }

    private void Load()
    {
        var tempStr = PlayerPrefs.GetString("SaveKey", "");
        SaveMap = tempStr == "" ? new SaveMap() : JsonUtility.FromJson<SaveMap>(tempStr);
    }

    private void Save()
    {
        PlayerPrefs.SetString("SaveKey", GetSaveMapSerialization());
    }

    public string GetSaveMapSerialization()
    {
        return JsonUtility.ToJson(SaveMap);
    }
}