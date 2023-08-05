using System;
using UniRx;

[Serializable]
public class SaveMap
{
    public BoolReactiveProperty musicEnable = new BoolReactiveProperty(true);
    public BoolReactiveProperty soundEnable = new BoolReactiveProperty(true);
}