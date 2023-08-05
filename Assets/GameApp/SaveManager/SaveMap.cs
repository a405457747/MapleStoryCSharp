using System;
using System.Collections.Generic;
//using UniRx;

[Serializable]
public  class SaveMap
{
    public UserSet userSet = new UserSet();
   public  object k2 = new { name = "bill", age = 32 };
    public SaveMap()
    {
       
    }
}

[Serializable]
public class UserSet
{
    /*
    public ReactiveCollection<int> testRCList = new ReactiveCollection<int>() { 1, 3, 5, 7, 0 };
    public  ReactiveDictionary<string, int> _reactiveDictionary = new ReactiveDictionary<string, int>()
    {
        { "aaaa", 222 },
        { "bbbb", 333 }
    };

    public Dictionary<string, bool> testDic = new Dictionary<string, bool>()
    {
        { "a",true },
        { "b",false },
    };
    public string title = "UserSet";
    public BoolReactiveProperty musicEnable = new BoolReactiveProperty(true);
    public BoolReactiveProperty soundEnable = new BoolReactiveProperty(true);
    */
}