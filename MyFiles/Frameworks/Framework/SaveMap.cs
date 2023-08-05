using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public  class SaveMap
{
    public BoolReactiveProperty musicEnable = new BoolReactiveProperty(true);
    public BoolReactiveProperty soundEnable = new BoolReactiveProperty(true);
}