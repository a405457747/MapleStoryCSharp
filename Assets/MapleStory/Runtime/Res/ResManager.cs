using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.U2D;

namespace MapleStory
{
    public interface IAssetFactory
    {
        AudioClip LoadAudioClip(string name);
        Sprite LoadSprite(string name);
        Material LoadMaterial(string name);
        Font LoadFont(string name);
        TextAsset LoadTextAsset(string name);
        SpriteAtlas LoadSpriteAtlas(string name);
        GameObject LoadGameObject(string name);
        T LoadScriptableObject<T>() where T : ScriptableObject;



        public GameObject LoadEffect(string name);
        public GameObject LoadPanel(string name);
        public GameObject LoadPool(string name);
        public Sprite LoadAtlasSprite(string name, string name2);

    }

    public class ResManager : MonoBehaviour, IAssetFactory
    {
        private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

        public virtual void Awake()
        {

        }

        public T LoadJson<T>(string txtName) where T : class
        {
            T res = default;

            return res;
        }

        public AudioClip LoadAudioClip(string name)
        {
            if (_audioClips.ContainsKey(name)==false)
            {
                _audioClips.Add(name,Resources.Load<AudioClip>( Path.Combine("audio",name)));
            }

            return _audioClips[name];
        }

        public Sprite LoadSprite(string name)
        {
            return Resources.Load<Sprite>(name);
        }

        public Material LoadMaterial(string name)
        {
            return Resources.Load<Material>(name);
        }

        public Font LoadFont(string name)
        {
            return Resources.Load<Font>(name);
        }

        public TextAsset LoadTextAsset(string name)
        {
            return Resources.Load<TextAsset>(name);
        }

        public SpriteAtlas LoadSpriteAtlas(string name)
        {
            return Resources.Load<SpriteAtlas>(name);
        }

        public GameObject LoadGameObject(string name)
        {
            return Resources.Load<GameObject>(name);
        }

        public T LoadScriptableObject<T>() where T : ScriptableObject
        {
            return Resources.Load<T>(name);
        }

        public GameObject LoadEffect(string name)
        {
            throw new System.NotImplementedException();
        }

        public GameObject LoadPanel(string name)
        {
            throw new System.NotImplementedException();
        }

        public GameObject LoadPool(string name)
        {
            throw new System.NotImplementedException();
        }

        public Sprite LoadAtlasSprite(string name, string name2)
        {
            throw new System.NotImplementedException();
        }
    }
}