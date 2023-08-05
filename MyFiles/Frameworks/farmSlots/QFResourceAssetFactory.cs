using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.U2D;

public class QFResourceAssetFactory : IAssetFactory
{
    private ResLoader _resLoaderTextAsset;
    private ResLoader _resLoaderSprite;
    private ResLoader _resLoaderGameObject;

    public QFResourceAssetFactory()
    {
        _resLoaderTextAsset = ResLoader.Allocate();
        _resLoaderSprite =ResLoader.Allocate();
        _resLoaderGameObject =ResLoader.Allocate();
    }

    public override GameObject LoadEffect(string name)
    {
        throw new System.NotImplementedException();
    }

    public override AudioClip LoadAudioClip(string name)
    {
        throw new System.NotImplementedException();
    }

    public override Sprite LoadSprite(string name)
    {
       return _resLoaderSprite.LoadSprite(name) as Sprite;
    }

    public override Material LoadMaterial(string name)
    {
        throw new System.NotImplementedException();
    }

    public override Font LoadFont(string name)
    {
        throw new System.NotImplementedException();
    }

    public override TextAsset LoadTextAsset(string name)
    {
        return _resLoaderTextAsset.LoadSync(name) as TextAsset;
    }

    public override SpriteAtlas LoadSpriteAtlas(string name)
    {
        throw new System.NotImplementedException();
    }

    public override GameObject LoadPanel(string name)
    {
        throw new System.NotImplementedException();
    }

    public override GameObject LoadPool(string name)
    {
        throw new System.NotImplementedException();
    }

    public override GameObject loadGameObject(string name)
    {
       return _resLoaderGameObject.LoadSync(name) as GameObject;
    }

    public override T LoadScriptableObject<T>()
    {
        throw new System.NotImplementedException();
    }
}