using System;
using System.Collections.Generic;
using UniRx;

[Serializable]
public class UserSet
{
    public BoolReactiveProperty musicEnable = new BoolReactiveProperty(true);
    public BoolReactiveProperty soundEnable = new BoolReactiveProperty(true);

    public ReactiveDictionary<string, int> rDic = new ReactiveDictionary<string, int>(
    )
    {
        {"a",1},
        {"b",2}
    };
}