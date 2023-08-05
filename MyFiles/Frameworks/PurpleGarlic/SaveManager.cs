using UnityEngine;
using System;

[Serializable]
public class SaveMap
{
    public int CurrentLevelIndex;
}

public partial class SaveManager : MonoSingleton<SaveManager>
{
    public SaveMap SaveMap;

    protected override void Start()
    {
        base.Start();

        Load();
    }

    private void OnDestroy()
    {
        Save();
    }

    private void Load()
    {
        var getStr = PlayerPrefs.GetString(nameof(SaveMap), "");
        if (getStr == "")
            SaveMap = new SaveMap();
        else
            SaveMap = JsonUtility.FromJson<SaveMap>(getStr);
    }

    private void Save()
    {
        PlayerPrefs.SetString(nameof(SaveMap), JsonUtility.ToJson(SaveMap));
    }
}