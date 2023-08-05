public class SaveSystem : GameSystem
{
    private readonly ISave CurSave = new UnityJsonSave();

    public SaveSystem(Game game) : base(game)
    {
    }

    public SaveMap SaveMap => CurSave.SaveMap;

//auto
    private void Awake()
    {
    }

    private void Load()
    {
        CurSave.Save();
    }

    private void Save()
    {
        CurSave.Save();
    }

    private string GetSaveMapString()
    {
        return CurSave.GetSaveMapString();
    }
}