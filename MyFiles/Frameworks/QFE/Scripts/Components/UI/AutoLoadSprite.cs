using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CallPalCatGames.QFrameworkExtension
{
    [RequireComponent(typeof(LoadHelper))]
    public class AutoLoadSprite : MonoBehaviour
    {
        private Image _image;
        private LoadHelper _loadHelper;
        [FormerlySerializedAs("SpriteName")] public string spriteName;

        private void Start()
        {
            _image = GetComponent<Image>();
            _loadHelper = GetComponent<LoadHelper>();
            _image.sprite = _loadHelper.LoadThing<Sprite>(spriteName);
        }
    }
}