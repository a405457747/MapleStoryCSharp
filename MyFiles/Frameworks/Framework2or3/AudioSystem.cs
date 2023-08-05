using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : GameSystem
{
    private AudioSource _musicAudioSource;

#pragma warning disable 414
    private bool _musicEnable = false;
#pragma warning restore 414
    private readonly Dictionary<string, AudioSource> _soundAudioSources = new Dictionary<string, AudioSource>();

    private readonly bool _soundEnable = false;

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
        _musicAudioSource.Pause();
    }

    public void RecoverMusic()
    {
        _musicAudioSource.UnPause();
    }

    public void PlaySound(string audioName, bool isLoop = false, float volume = 1f, bool isPlayOneShot = false)
    {
        if (_soundEnable == false) return;

        if (_soundAudioSources.ContainsKey(audioName))
        {
            _soundAudioSources[audioName].Play();
        }
        else
        {
            var tempAudioSource = gameObject.AddComponent<AudioSource>();
            _soundAudioSources.Add(audioName, tempAudioSource);

            AudioSourceCommon(tempAudioSource, audioName, isLoop, volume, isPlayOneShot);
        }
    }

    private void AudioSourceCommon(AudioSource audioSource, string audioName, bool isLoop = false,
        float volume = 0.65f, bool isPlayOneShot = false)
    {
        audioSource.loop = isLoop;
        audioSource.volume = volume;
        audioSource.clip = Factorys.GetAssetFactory().LoadAudioClip(audioName);

        if (audioSource.enabled)
        {
            if (isPlayOneShot)
                audioSource.PlayOneShot(audioSource.clip);
            else
                audioSource.Play();
        }
    }
}