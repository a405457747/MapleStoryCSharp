﻿using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PanelSet : Panel
{
    private Toggle _toggleMusic;
    private Toggle _toggleSound;
    private SaveMap _saveMap;

    public override void OnInit(PanelArgs arguments)
    {
        base.OnInit(arguments);

        _toggleMusic = transform.Find("ToggleMusic").GetComponent<Toggle>();
        _toggleSound = transform.Find("ToggleSound").GetComponent<Toggle>();

        _saveMap = SaveManager.Instance.SaveMap;

        _toggleMusic.onValueChanged.AddListener(isOn =>
        {
            _saveMap.musicEnable.Value = isOn;
        });
        
        _toggleSound.onValueChanged.AddListener(isOn =>
        {
            _saveMap.soundEnable.Value = isOn;
        });
        
        _toggleMusic.isOn =_saveMap.musicEnable.Value;
        _toggleSound.isOn = _saveMap.soundEnable.Value;
    }

    public override void OnOpen(PanelArgs arguments)
    {
        base.OnOpen(arguments);
    }
}