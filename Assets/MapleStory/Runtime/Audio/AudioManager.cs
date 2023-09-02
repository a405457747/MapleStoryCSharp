using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace MapleStory
{
    public class AudioManager : MonoBehaviour
    {
        private AudioSource _musicAudioSource;
        private Dictionary<string, AudioSource> _soundAudioSources;
        private ResManager _resManager;
        public static AudioManager Inst { get; private set; }

        public virtual void Awake()
        {
            Inst = this;
            
            _resManager = FindObjectOfType<ResManager>();
            _soundAudioSources = new Dictionary<string, AudioSource>();

            _musicAudioSource = GetComponent<AudioSource>();
            if (_musicAudioSource == null) _musicAudioSource = gameObject.AddComponent<AudioSource>();
        }

        private bool _musicEnable
        {
            get { return true; }
        }

        private bool _soundEnable
        {
            get { return true; }
        }


        private void AudioSourceCommon(AudioSource audioSource, string audioName, bool isLoop ,
            float volume , bool isPlayOneShot)
        {
            AudioClip tempClip = _resManager.LoadAudioClip(audioName); 
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
                var tempAudioSource =
                    gameObject.AddComponent<AudioSource>();
                _soundAudioSources.Add(audioName, tempAudioSource);

                AudioSourceCommon(tempAudioSource, audioName, isLoop, volume, isPlayOneShot);
            }
        }

        public void PlayMusic(string audioName, bool isLoop = true, float volume = 1f, bool isPlayOneShot = false)
        {
            if (_musicEnable == false) return;

            AudioSourceCommon(_musicAudioSource, audioName, isLoop, volume, isPlayOneShot);
        }

    }
}