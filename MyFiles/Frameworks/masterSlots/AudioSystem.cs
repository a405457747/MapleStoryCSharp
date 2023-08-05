using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : GameSystem
{
    private float musicDefaultVolume = 0.3f;
    private AudioSource _musicAudioSource;

    private Dictionary<string, AudioSource> _soundAudioSources = new Dictionary<string, AudioSource>();

    public AudioSystem(Game game) : base(game)
    {
    }

//auto
    private void Awake()
    {
    }

    public override void Initialize()
    {
        base.Initialize();

        _musicAudioSource = gameObject.AddComponent<AudioSource>();
        // _musicAudioSource.volume = 0.3f;
        _soundAudioSources = new Dictionary<string, AudioSource>();
    }

    public void LOAD_OVER_Callback()
    {
        PlayMusicAndSwitch("public_BGM");
    }

    public void PlayMusicAndSwitch(string audioName)
    {
        PlayMusic(audioName);
        SwitchMusic();
    }

    public void SwitchMusic()
    {
        if (Game.I.myUserAndYouPlayerSettingsAndHappyModel.save._musicEnable == false)
        {
            PauseMusic();
        }
        else
        {
            RecoverMusic();
        }
    }


    private void AudioSourceCommon(AudioSource audioSource, string audioName, bool isLoop = false,
        float volume = 0.65f, bool isPlayOneShot = false)
    {
        AudioClip tempClip = Factorys.GetAssetFactory().LoadAudioClip(audioName);
        if (tempClip != null)
        {
            audioSource.loop = isLoop;
            audioSource.volume = volume;
            audioSource.clip = tempClip;

            if (audioSource.enabled)
            {
                if (isPlayOneShot)
                    audioSource.PlayOneShot(audioSource.clip);
                else
                    audioSource.Play();
            }
        }
    }

    public void PlaySound(AudioEnum audioEnum)
    {
        if (GData.BakingMode)
        {
            return;
        }

        PlaySound(audioEnum.ToString());
    }


    public void PlaySound(string audioName, bool isLoop = false, float volume = 1f, bool isPlayOneShot = false)
    {
        if (Game.I.myUserAndYouPlayerSettingsAndHappyModel.save._soundEnable == false) return;

        if (_soundAudioSources.ContainsKey(audioName))
        {
            if (isPlayOneShot)
            {
                _soundAudioSources[audioName].PlayOneShot(_soundAudioSources[audioName].clip);
            }
            else
            {
                _soundAudioSources[audioName].Play();
            }
        }
        else
        {
            var tempAudioSource = gameObject.AddComponent<AudioSource>();
            _soundAudioSources.Add(audioName, tempAudioSource);

            AudioSourceCommon(tempAudioSource, audioName, isLoop, volume, isPlayOneShot);
        }
    }
    /*private void Start()
    {
        SaveMap saveMap = null; 

        saveMap.musicEnable.Subscribe(isEnable =>
        {
            _musicEnable = isEnable;
            if (_musicEnable == false)
                _musicAudioSource.enabled = false;
            else
                _musicAudioSource.enabled = true;
        });
        saveMap.soundEnable.Subscribe(isEnable => { _soundEnable = isEnable; });
    }*/

    public void PlayMusic(string audioName, bool isLoop = true, float volume = 1f, bool isPlayOneShot = false)
    {
        AudioSourceCommon(_musicAudioSource, audioName, isLoop, volume, isPlayOneShot);
    }

    public void PauseMusic()
    {
        _musicAudioSource.volume = 0;
    }

    public void RecoverMusic()
    {
        Log.LogParas(musicDefaultVolume, "volume");
        _musicAudioSource.volume = 1f;
    }
}