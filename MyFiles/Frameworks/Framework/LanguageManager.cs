using UnityEngine;
using UnityEngine.Serialization;

public class LanguageManager : MonoSingleton<LanguageManager>
{
    public int languageId;

    protected override void Awake()
    {
        base.Awake();

        AutoSetLanguageId();
    }

    private void AutoSetLanguageId()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Chinese:
            case SystemLanguage.ChineseSimplified:
                languageId = 0;
                break;

            case SystemLanguage.ChineseTraditional:
                languageId = 1;
                break;

            default:
                languageId = 2;
                break;
        }
    }

    public static string GetMessage(int messageId)
    {
        return "";
    }
}