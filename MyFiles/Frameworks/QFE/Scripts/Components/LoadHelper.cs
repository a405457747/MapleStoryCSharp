using QFramework;
using UnityEngine;

namespace CallPalCatGames.QFrameworkExtension
{
    public class LoadHelper : MonoBehaviour
    {
        private ResLoader _resLoader;

        private void Awake() 
        {
            _resLoader = ResLoader.Allocate();
        }

        private void OnDestroy()
        {
            if (_resLoader != null)
            {
                _resLoader.Recycle2Cache();
                _resLoader = null;
            }
        }

        public T LoadThing<T>(string name) where T : class
        {
            var t = typeof(T);
            if (t.Name.Equals("Sprite")) return _resLoader.LoadSprite(name) as T;
            return _resLoader.LoadSync(name) as T;
        }
    }
}