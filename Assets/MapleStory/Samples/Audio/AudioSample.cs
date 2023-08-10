using System;
using System.Collections;
using System.Collections.Generic;
using MapleStory;
using UnityEngine;


[RequireComponent(typeof(AudioManager))]
public class AudioSample : MonoBehaviour
{

    private AudioManager _audio;
    private void Start()
    {
        _audio = FindObjectOfType<AudioManager>();

        _audio.PlayMusic(AudioName.StartBackground);
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _audio.PlaySound(AudioName.Fire);
        }
    }
}
