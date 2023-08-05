using UnityEngine.SceneManagement;

public class SceneryManager : MonoSingleton<SceneryManager>
{
    public const int MaxLevel = 36;

    protected override void Awake()
    {
        base.Awake();
    }

    public void LoadScene(string sceneName)
    {
       SceneManager.LoadScene(sceneName);
    }

    public static int GetLevelSceneId()
    {
        var name = SceneManager.GetActiveScene().name;

        if (name.Contains("Level"))
        {
            return StringHelper.GetPureNumber(name);
        }
        else
        {
            Log.LogError("The name is not right.");
            return -1;
        }
    }

    public static int GetLevelSceneIndex()
    {
        return GetLevelSceneId() - 1;
    }
}