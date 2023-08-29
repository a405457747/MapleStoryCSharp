using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CallPalCatGames.QFrameworkExtension
{
    public class LanguageTransformation : MonoBehaviour
    {
        private Text _text;
        [FormerlySerializedAs("LanguageId")] public int languageId;

        private void Start()
        {
            _text = GetComponent<Text>();
            _text.text = SaveManager.Instance.ReturnLanguageMessage(languageId);
        }
    }
}