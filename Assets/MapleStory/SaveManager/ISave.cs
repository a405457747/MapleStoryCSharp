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
}