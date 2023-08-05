using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class ResourcesManager : MonoSingleton<ResourcesManager>
{
    public SpriteAtlas[] atlasVar;
    public Sprite[] sprites;
    public Material[] materials;
    public Font[] fonts;
    public AudioClip[] audioClips;
    public GameObject[] panels;
    public GameObject[] pools;

    protected override void Awake()
    {
        base.Awake();
    }

    private void LoadScene()
    {
    }

    private SpriteAtlas GetAtlas(string atlasName)
    {
        return GetResources(atlasVar, atlasName);
    }

    public Sprite GetSprite(string spriteName, string atlasName)
    {
        var atlas = GetAtlas(atlasName);
        return atlas.GetSprite(spriteName);
    }

    public Sprite GetSprite(string spriteName)
    {
        return GetResources(sprites, spriteName);
    }

    public Font GetFont(string fontName)
    {
        return GetResources(fonts, fontName);
    }

    public Material GetMaterial(string materialName)
    {
        return GetResources(materials, materialName);
    }

    public GameObject GetPool(string goName)
    {
        return GetResources(pools, goName);
    }

    public AudioClip GetAudio(string audioName)
    {
        return GetResources(audioClips, name);
    }

    public GameObject GetPanel(string panelName)
    {
        return GetResources(panels, panelName);
    }

    private static T GetResources<T>(T[] resources, string resName) where T : Object
    {
        foreach (var item in resources)
            if (item.name == resName)
                return item;

        return default;
    }
}