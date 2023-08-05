using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    private AudioSource _musicAudioSource;
    private bool _musicEnable;
    private Dictionary<string, AudioSource> _soundAudioSources;
    private bool _soundEnable;

    private bool MusicEnable
    {
        get => _musicEnable;
        set
        {
            _musicEnable = value;
            _musicAudioSource.mute = !_musicEnable;
        }
    }

    private bool SoundEnable
    {
        get => _soundEnable;
        set
        {
            _soundEnable = value;
            if (_soundAudioSources.Count != 0)
                foreach (var kv in _soundAudioSources)
                    kv.Value.mute = !_soundEnable;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        _soundAudioSources = new Dictionary<string, AudioSource>();
        _musicAudioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        var saveMap = SaveManager.Instance.SaveMap;

        saveMap.musicEnable.Subscribe(isEnable => { MusicEnable = isEnable; });
        saveMap.soundEnable.Subscribe(isEnable => { SoundEnable = isEnable; });
    }

    public void PlayMusic(string audioName, bool isLoop = false, float volume = 1f)
    {
        AudioSourceCommon(_musicAudioSource, audioName, isLoop, volume);
    }

    public void PauseMusic()
    {
        _musicAudioSource.Pause();
    }

    public void RecoverMusic()
    {
        _musicAudioSource.UnPause();
    }

    public void PlaySound(string audioName, bool isLoop = false, float volume = 1f)
    {
        if (_soundAudioSources.ContainsKey(audioName))
        {
            _soundAudioSources[audioName].Play();
        }
        else
        {
            var tempAudioSource = gameObject.AddComponent<AudioSource>();
            _soundAudioSources.Add(audioName, tempAudioSource);

            AudioSourceCommon(tempAudioSource, audioName, isLoop, volume);
        }
    }

    private void AudioSourceCommon(AudioSource audioSource, string audioName, bool isLoop = false,
        float volume = 1f)
    {
        audioSource.loop = isLoop;
        audioSource.volume = volume;
        audioSource.clip = ResourcesManager.Instance.GetAudio(audioName);
        audioSource.Play();
    }
}