using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace CallPalCatGames.QFrameworkExtension
{
    public enum VoiceEvent
    {
        Began = QMgrID.Voice,
        PlayMusic,
        PlaySound,
        MusicSwitch
    }

    public class VoiceSwitchQMsg : QMsg
    {
        public VoiceSwitchQMsg(bool ison, int id) : base(id)
        {
            IsOn = ison;
        }

        public bool IsOn { get; set; }
    }

    public class VoiceMsgQMsg : QMsg
    {
        public VoiceMsgQMsg(string name, int id) : base(id)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

    public class VoiceManager : QMgrBehaviour, ISingleton
    {
        private LoadHelper _loadHelper;
        private AudioSource _musicAudioSource;
        private bool _musicMute;
        private float _musicVolume;
        private Dictionary<string, AudioSource> _soundAudioSources;
        private bool _soundMute;
        public override int ManagerId => QMgrID.Voice;
        public static VoiceManager Instance => MonoSingletonProperty<VoiceManager>.Instance;

        public bool MusicMute
        {
            get => _musicMute;
            set
            {
                if (!MusicMute.Equals(value))
                {
                    _musicMute = value;
                    _musicAudioSource.mute = _musicMute;
                }
            }
        }

        public bool SoundMute
        {
            get => _soundMute;
            set
            {
                if (!_soundMute.Equals(value)) _soundMute = value;
            }
        }

        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                if (!_musicVolume.Equals(value))
                {
                    _musicVolume = value;
                    _musicAudioSource.volume = _musicVolume;
                }
            }
        }

        public void OnSingletonInit()
        {
            _soundAudioSources = new Dictionary<string, AudioSource>();
            _musicAudioSource = gameObject.GetOrAddComponent<AudioSource>();
            _loadHelper = gameObject.GetOrAddComponent<LoadHelper>();
            MusicVolume = 0.5f;
            RegisterEvent(VoiceEvent.PlayMusic);
            RegisterEvent(VoiceEvent.MusicSwitch);
            RegisterEvent(VoiceEvent.PlaySound);
        }

        protected override void ProcessMsg(int key, QMsg msg)
        {
            switch (msg.EventID)
            {
                case (int) VoiceEvent.PlayMusic:
                    var tempMsg1 = msg as VoiceMsgQMsg;
                    PlayMusic(tempMsg1.Name);
                    break;
                case (int) VoiceEvent.MusicSwitch:
                    var tempMsg2 = msg as VoiceSwitchQMsg;
                    if (tempMsg2.IsOn) //想开启音乐
                        UnPauseMusic();
                    else
                        PauseMusic();
                    break;
                case (int) VoiceEvent.PlaySound:
                    var tempMsg3 = msg as VoiceMsgQMsg;
                    PlaySound(tempMsg3.Name);
                    break;
            }
        }

        public void PlayMusic(string clipName, bool isLoop = true)
        {
            AudioSourceOperation(_musicAudioSource, clipName, isLoop);
        }

        private void PauseMusic()
        {
            _musicAudioSource.Pause();
        }

        private void UnPauseMusic()
        {
            _musicAudioSource.UnPause();
        }

        public void PlaySound(string clipName, bool isLoop = false)
        {
            if (SoundMute) return;
            if (!_soundAudioSources.ContainsKey(clipName))
            {
                var tempAudioSource = gameObject.AddComponent<AudioSource>();
                AudioSourceOperation(tempAudioSource, clipName, isLoop);
                _soundAudioSources.Add(clipName, tempAudioSource);
            }
            else
            {
                _soundAudioSources[clipName].Play();
            }
        }

        private void AudioSourceOperation(AudioSource audioSource, string clipName, bool isLoop)
        {
            audioSource.clip = LoadAudioClip(clipName);
            audioSource.loop = isLoop;
            audioSource.Play();
        }

        private AudioClip LoadAudioClip(string clipName)
        {
            return _loadHelper.LoadThing<AudioClip>(clipName);
        }
    }
}