using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PanelSet : Panel
{

    public override void OnInit(PanelArgs arguments)
    {
        base.OnInit(arguments);

        Toggle _toggleMusic = transform.Find("ToggleMusic").GetComponent<Toggle>();
        Toggle _toggleSound = transform.Find("ToggleSound").GetComponent<Toggle>();

        SaveMap _saveMap = SaveManager.Instance.SaveMap;

        _toggleMusic.onValueChanged.AddListener(isOn =>
        {
            _saveMap.musicEnable.Value = isOn;
        });

        _toggleSound.onValueChanged.AddListener(isOn =>
        {
            _saveMap.soundEnable.Value = isOn;
        });

        _toggleMusic.isOn = _saveMap.musicEnable.Value;
        _toggleSound.isOn = _saveMap.soundEnable.Value;
    }

}