using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

// 做為ResourceAssetFactory的Proxy代理者,會記錄已經載入過的資源
public class ResourceAssetProxyFactory : IAssetFactory
{
    private readonly Dictionary<string, AudioClip> m_Audios;
    private readonly Dictionary<string, GameObject> m_Effects;
    private readonly Dictionary<string, TextAsset> m_TextAsset;

    private readonly ResourceAssetFactory m_ResFactory; // 實際負責載入的AssetFactory

    //private readonly QFResourceAssetFactory m_QFFactory; 
    private readonly Dictionary<string, Sprite> m_Sprites;

    public string LoadLuaDatas(int lv)
    {
        var txt = LoadTextAsset($"Lv{lv}/datas");
        return txt.text;
    }

    public Sprite[] LoadSprites(int lv)
    {
        Sprite[] res = Resources.LoadAll<Sprite>($"Sprite/Level{lv}");
        return res;
    }

    public ResourceAssetProxyFactory()
    {
        /*ResKit.Init();
        m_QFFactory = new QFResourceAssetFactory();*/
        m_ResFactory = new ResourceAssetFactory();
        m_Effects = new Dictionary<string, GameObject>();
        m_Audios = new Dictionary<string, AudioClip>();
        m_Sprites = new Dictionary<string, Sprite>();
        m_TextAsset = new Dictionary<string, TextAsset>();
    }

    // 產生特效
    public override GameObject LoadEffect(string AssetName)
    {
        if (m_Effects.ContainsKey(AssetName) == false)
        {
            m_Effects.Add(AssetName, m_ResFactory.LoadEffect(AssetName));
        }

        return m_Effects[AssetName];
    }

    // 產生AudioClip
    public override AudioClip LoadAudioClip(string ClipName)
    {
        ClipName = ClipName.Trim();
        
        if (m_Audios.ContainsKey(ClipName) == false)
        {
            m_Audios.Add(ClipName, m_ResFactory.LoadAudioClip(ClipName));
        }

        return m_Audios[ClipName];
    }

    // 產生Sprite
    public override Sprite LoadSprite(string SpriteName)
    {
        SpriteName = SpriteName.Trim();
        
        if (m_Sprites.ContainsKey(SpriteName) == false)
        {
            m_Sprites.Add(SpriteName, m_ResFactory.LoadSprite(SpriteName));
        }

        return m_Sprites[SpriteName];
    }

    public override Material LoadMaterial(string name)
    {
        return m_ResFactory.LoadMaterial(name);
    }

    public override Font LoadFont(string name)
    {
        return m_ResFactory.LoadFont(name);
    }

    public override TextAsset LoadTextAsset(string name)
    {
        if (m_TextAsset.ContainsKey(name) == false)
        {
            m_TextAsset.Add(name, m_ResFactory.LoadTextAsset(name));
        }

        return m_TextAsset[name];
    }

    public override SpriteAtlas LoadSpriteAtlas(string name)
    {
        return null;
    }

    public override GameObject LoadPanel(string name)
    {
        return m_ResFactory.LoadPanel(name);
    }

    public override GameObject LoadPool(string name)
    {
        return m_ResFactory.LoadPool(name);
    }

    public override GameObject loadGameObject(string name)
    {
        //return m_QFFactory.loadGameObject(name);
        return m_ResFactory.loadGameObject(name);
    }

    public override T LoadScriptableObject<T>()
    {
        return m_ResFactory.LoadScriptableObject<T>();
    }
}