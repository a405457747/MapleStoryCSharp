public interface ISave
{
    SaveMap SaveMap { get; set; }
    void Load();
    void Save();
    string GetSaveMapString();
}