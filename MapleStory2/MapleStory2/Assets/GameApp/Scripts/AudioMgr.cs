using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace MapleStory2
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioMgr : MonoBehaviour
    {
        internal static AudioMgr Inst;

        private AudioSource bgSource;

        private Dictionary<string, AudioSource> effectDict = new Dictionary<string, AudioSource>();
        private void Awake()
        {
            bgSource = GetComponent<AudioSource>();
            bgSource.loop = true;
            
            Inst = this;
            
           
        }

        private void Update()
        {
        }

        internal void Sample()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                MusicPlay("Audio/StartBackground");
                EffectPlay("Audio/Fire");
            }
        }

        internal void MusicPlay(string soundPath)
        {
            AudioClip clip = Resources.Load<AudioClip>(soundPath);
            bgSource.clip = clip;
            bgSource.Play();
        }

        internal void EffectPlay(string soundPath)
        {
            if (effectDict.ContainsKey(soundPath)==false)
            {
                var audioSource = gameObject.AddComponent<AudioSource>();
                
                audioSource.clip =Resources.Load<AudioClip>(soundPath);
                
                effectDict.Add(soundPath,audioSource);
            }
            
         
            effectDict[soundPath].Play();

        }
    }
}