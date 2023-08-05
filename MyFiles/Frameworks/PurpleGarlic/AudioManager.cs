using System;
using System.Collections.Generic;
using UnityEngine;


public partial class AudioManager : MonoSingleton<AudioManager>
{
    private readonly Dictionary<string, AudioSource> _soundAudioSources = new Dictionary<string, AudioSource>();
    private AudioSource _musicAudioSource;

    private bool _musicEnable;
    private bool _soundEnable;

    public bool MusicEnable
    {
        get => _musicEnable;
        set
        {
            _musicEnable = value;
            _musicAudioSource.mute = !_musicEnable;
        }
    }

    public bool SoundEnable
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

    protected override void Start()
    {
        base.Start();
    }

    public void PlayMusic(string name, bool isLoop = false, float volume = 1f)
    {
        if (_musicAudioSource == null)
        {
            _musicAudioSource = gameObject.AddComponent<AudioSource>();
        }
        AudioSourceCommon(_musicAudioSource, name, isLoop, volume);
    }

    public void PauseMusic()
    {
        _musicAudioSource.Pause();
    }

    public void RecoverMusic()
    {
        _musicAudioSource.UnPause();
    }

    public void PlaySound(string name, bool isLoop = false, float volume = 1f)
    {
        if (_soundAudioSources.ContainsKey(name))
        {
            _soundAudioSources[name].Play();
        }
        else
        {
            var tempAudioSource = gameObject.AddComponent<AudioSource>();

            AudioSourceCommon(tempAudioSource, name, isLoop, volume);

            _soundAudioSources.Add(name, tempAudioSource);
        }
    }

    private void AudioSourceCommon(AudioSource audioSource, string pathName, bool isLoop = false,
        float volume = 1f)
    {
        audioSource.loop = isLoop;
        audioSource.volume = volume;
        audioSource.clip = ResManager.Instance.GetAudioClip(pathName);
        audioSource.Play();
    }
}