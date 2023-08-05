public class SaveSystem : GameSystem
{
    private ISave CurSave;

    public SaveSystem(Game game) : base(game)
    {
    }

    public override void Initialize()
    {
        base.Initialize();
        CurSave = new UnityJsonSave();
        Load();
    }

    public SaveMap SaveMap => CurSave.SaveMap;

//auto
    private void Awake()
    {
    }

    private void Load()
    {
        CurSave.Load();
    }

    public void Save()
    {
        CurSave.Save();
    }

    public string GetSaveMapString()
    {
        return CurSave.GetSaveMapString();
    }
}