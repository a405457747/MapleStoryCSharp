using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace MapleStory2
{
    class AudioSet
    {
        public float musicVolume = 1f;
        public float effectVolume = 1f;
    }

    [RequireComponent(typeof(AudioSource))]
    public class AudioMgr : MonoBehaviour
    {
        public string musicPath = "";

        internal static AudioMgr Inst;

        private AudioSource bgSource;

        private Dictionary<string, AudioSource> effectDict = new Dictionary<string, AudioSource>();

        internal AudioSet audioSet;

        internal float MusicVolume
        {
            get { return audioSet.musicVolume; }
            set
            {
                audioSet.musicVolume = value;
                bgSource.volume = value;
            }
        }

        internal float EffectVolume
        {
            get { return audioSet.effectVolume; }
            set { audioSet.effectVolume = value; }
        }

        private void Awake()
        {
            Inst = this;
            bgSource = GetComponent<AudioSource>();
            audioSet = GetComponent<SaveMgr>().LoadData<AudioSet>();
//            print("audio awake");
        }

        private void Start()
        {
            bgSource.loop = true;
            MusicPlay(musicPath);
        }

        internal void SaveData()
        {
            SaveMgr.Inst.SaveData<AudioSet>(audioSet);
//            Debug.Log("SaveData!");
        }

        private void Update()
        {
            //Sample();
        }

        internal void Sample()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                EffectPlay("Audio/Fire");
            }
        }

        internal void MusicPlay(string soundPath)
        {
            AudioClip clip = Resources.Load<AudioClip>(soundPath);
            bgSource.clip = clip;

            bgSource.volume = MusicVolume;
            bgSource.Play();
        }

        internal void EffectPlay(string soundPath)
        {
            if (effectDict.ContainsKey(soundPath) == false)
            {
                var audioSource = gameObject.AddComponent<AudioSource>();

                audioSource.clip = Resources.Load<AudioClip>(soundPath);

                effectDict.Add(soundPath, audioSource);
            }


            effectDict[soundPath].volume = EffectVolume;
            effectDict[soundPath].Play();
        }
    }
}