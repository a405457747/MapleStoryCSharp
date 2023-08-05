using UnityEngine;
using UnityEngine.U2D;

// 將Unity Asset實體化成GameObject的工廠類別
public abstract class IAssetFactory
{
    public abstract GameObject LoadEffect(string name); // 產生特效
    public abstract AudioClip LoadAudioClip(string name);
    public abstract Sprite LoadSprite(string name);
    public abstract Material LoadMaterial(string name);
    public abstract Font LoadFont(string name);
    public abstract TextAsset LoadTextAsset(string name);
    public abstract SpriteAtlas LoadSpriteAtlas(string name);
    public abstract GameObject LoadPanel(string name);
    public abstract GameObject LoadPool(string name);
    public abstract GameObject loadGameObject(string name);
    public abstract T LoadScriptableObject<T>() where T : ScriptableObject;
}

/*
 * 使用Abstract Factory Patterny簡化版,
 * 讓GameObject的產生可以依Uniyt Asset放置的位置來載入Asset
 * 先實作放在Resource目錄下的Asset及Remote(Web Server)上的
 * 當Unity隨著版本的演進，也許會提供不同的載入方式，那麼我們就可以
 * 再先將一個IAssetFactory的子類別來因應變化
 */