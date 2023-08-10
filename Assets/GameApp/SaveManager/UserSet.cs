using System;
using System.Collections.Generic;
using UniRx;


[Serializable]
public class UserSet
{

    public BoolReactiveProperty musicEnable = new BoolReactiveProperty(true);
    public BoolReactiveProperty soundEnable = new BoolReactiveProperty(true);

}