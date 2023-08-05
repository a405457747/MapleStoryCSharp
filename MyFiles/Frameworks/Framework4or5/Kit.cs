using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;


/**
 * todo 事件系统大改thinkphpmvc 面板层级管理需要了再说吧 music暂时不用去中心化 用上sqllite 自动化打包 kit重命名
 */
public abstract partial class Kit : MonoBehaviour
{
    static Kit() //这个方法在调用任何一个静态方法前就会调用，太方便了卧槽。
    {
    }

    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        var temp = go.GetComponent<T>();
        if (temp == null) temp = go.AddComponent<T>();
        return temp;
    }

    public static T Obj<T>() where T : UnityEngine.Object
    {
        T[] arr = FindObjectsOfType<T>(); //包括隐藏的
        return (arr.Length == 1) ? arr[0] : default;
    }

    public static T AddGo<T>(string goName="", List<T> tList = null, Transform parent = null, bool stays = false,
        bool isUI = true)
        where T : MonoBehaviour
    {
        T item = default;
        if (isUI)
        {
            item = GameObject.Instantiate(Load<GameObject>(goName), parent, stays).GetComponent<T>();
        }
        else
        {
            var go = (new GameObject(typeof(T).Name+"(Clone)"));
            go.transform.SetParent(parent);
            item = Kit.GetOrAddComponent<T>(go);
        }

        if (tList != null) tList.Add(item);
        return item;
    }
    public static T DelGo<T>(int idx, List<T> tList) where T : MonoBehaviour
    {
        var item = tList[idx];
        Destroy(item.gameObject);
        tList.RemoveAt(idx);
        return item;
    }

    public static T Load<T>(string path) where T : UnityEngine.Object => Resources.Load<T>(path);

    public static void Emit<T>(string funcName, T args) //注册的方法可以不带参数，照样能调用。这个方法对静态方法无效
    {
        List<GameObject> rootObjects = new List<GameObject>(); //只有几个根对象
        SceneManager.GetActiveScene().GetRootGameObjects(rootObjects); //隐藏对象也能获取。Resources.FindObjectsOfTypeAll这个是备选

        rootObjects.Insert(0, Obj<Kit>().gameObject);
        foreach (var gameObject in rootObjects) //可以手动调顺序很爽的。
        {
            gameObject.BroadcastMessage(funcName, args, SendMessageOptions.DontRequireReceiver);
        }
    }

    public static void WriteText(string path, string content) =>
        File.WriteAllText(path, content, new UTF8Encoding(false));

    public static string ReadText(string path, string content) => File.ReadAllText(path, new UTF8Encoding(false));

    public static void
        NumRoll(Text txt, int startVal, int endVal,
            float cost = 0.5f) //更新UI的参考代码{NumRoll(AddNum, AddNum + startNum);AddNum += startNum;}
    {
        DOTween.To(value => { txt.text = Mathf.Floor(value).ToString(); }, startValue: startVal, endValue: endVal,
            duration: 0.5f);
    }

    /* UI update*/
    public static void Click(Button btn, Action callBack)
    {
        btn.onClick.AddListener(() => { callBack?.Invoke(); });
    }

    public static void Change(Image img, string name)
    {
        img.sprite = Kit.Load<Sprite>(name);
    }

    public static void Change(Text txt, string content = "") //这里可用用null
    {
        txt.text = content;
    }

    public static string GetLanguage(int id)
    {
        string res = "";
        return res;
    }

    public static string Language(params object[] paras)
    {
        int id = (int)paras[0];
        string txtContent = GetLanguage(id); //根据id获取txt的内容，让策划C位出道。
        if (paras.Length == 1)
        {
            return txtContent;
        }
        else if (paras.Length == 2) //只做一个星多了不想做了
        {
            return txtContent.Replace("*", paras[1].ToString());
        }

        return "";
    }

    /* Save */
    public static T Load<T>() where T : new()
    {
        var tempStr = GetSaveMapString<T>();
        T res = tempStr == "" ? new T() : JsonUtility.FromJson<T>(tempStr);
        return res;
    }

    public static void Save<T>(T obj) where T : class
    {
        PlayerPrefs.SetString(typeof(T).Name, JsonUtility.ToJson(obj, true));
    }

    public static string GetSaveMapString<T>() where T : new()
    {
        return PlayerPrefs.GetString(typeof(T).Name, "");
    }

    public static void Save()
    {
        Kit.Emit(nameof(Save), "");
    }

    public static void Load()
    {
        Kit.Emit(nameof(Load), "");
    }

    /* audio */
    private AudioSource _musicAudioSource;
    private Dictionary<string, AudioSource> _soundAudioSources;

    private bool _musicEnable
    {
        get { return true; }
    }

    private bool _soundEnable
    {
        get { return true; }
    }

    protected virtual void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        _musicAudioSource = Kit.GetOrAddComponent<AudioSource>(gameObject);
       // _musicAudioSource.volume = 0.3f;
        _soundAudioSources = new Dictionary<string, AudioSource>();
    }

    public void SwitchMusic()
    {
        if (_musicEnable == false)
        {
            _musicAudioSource.volume = 0;
        }
        else
        {
            _musicAudioSource.volume = 1f;
        }
    }

    private void AudioSourceCommon(AudioSource audioSource, string audioName, bool isLoop = false,
        float volume = 0.65f, bool isPlayOneShot = false)
    {
        AudioClip tempClip = Kit.Load<AudioClip>(audioName);
        audioSource.clip = tempClip;
        audioSource.loop = isLoop;
        audioSource.volume = volume;

        if (isPlayOneShot) audioSource.PlayOneShot(audioSource.clip);
        else audioSource.Play();
    }

    public void PlaySound(string audioName, bool isLoop = false, float volume = 1f,
        bool isPlayOneShot = false)
    {
        if (_soundEnable == false) return;

        if (_soundAudioSources.ContainsKey(audioName))
        {
            if (isPlayOneShot) _soundAudioSources[audioName].PlayOneShot(_soundAudioSources[audioName].clip);
            else _soundAudioSources[audioName].Play();
        }
        else
        {
            var tempAudioSource = Kit.GetOrAddComponent<AudioSource>(gameObject);
            _soundAudioSources.Add(audioName, tempAudioSource);
            AudioSourceCommon(tempAudioSource, audioName, isLoop, volume, isPlayOneShot);
        }
    }

    public void PlayMusic(string audioName, bool isLoop = true, float volume = 1f, bool isPlayOneShot = false)
    {
        AudioSourceCommon(_musicAudioSource, audioName, isLoop, volume, isPlayOneShot);
    }

    /*update*/
    protected virtual void KeyDownA()
    {
    }

    protected virtual void KeyDownS()
    {
    }

    protected virtual void KeyDownJ()
    {
    }

    protected virtual void KeyDownK()
    {
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            KeyDownA();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            KeyDownS();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            KeyDownJ();
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            KeyDownK();
        }
    }
}