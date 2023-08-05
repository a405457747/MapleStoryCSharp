using UnityEngine;

public class LanguageSystem : GameSystem
{
#pragma warning disable 414
    private int _languageId;
#pragma warning restore 414

    public LanguageSystem(Game game) : base(game)
    {
    }

//auto
    private void Awake()
    {
    }

    private void AutoSetLanguageId()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Chinese:
            case SystemLanguage.ChineseSimplified:
                _languageId = 0;
                break;

            case SystemLanguage.ChineseTraditional:
                _languageId = 1;
                break;

            default:
                _languageId = 2;
                break;
        }
    }

    public static string GetMessage(int messageId)
    {
        return "";
    }

    public override void Initialize()
    {
        base.Initialize();
        AutoSetLanguageId();
    }

    public override void Release()
    {
        base.Release();
    }
}