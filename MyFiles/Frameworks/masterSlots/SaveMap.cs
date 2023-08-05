using System;
using System.Collections.Generic;

[Serializable]
public class SaveMap
{
    //充值读取的配置表索引
    public int PurchasingExcelIndex = 0;
    //充值次数
    public int PurchasingCount = 0;
    
    //邮票ID的集合
    public int[] StampIDs = new int[96];
    //邮票领取过奖励的状态
    public bool[] haveAwardStamp = new bool[8];
    public bool haveTheBigAwardStamp = false;
    

    //Mission当前状态的进度值 
    public int MissionProcess = 0;
    //Mission当前的状态
    public int MissionLockState = 0;
    //Mission是否领取的状态
    public bool[] MissiionIsCollect = new bool[20];
    
    //这两个是为了一个S的配置而搞的
    public int S_Config_TotalWin = 0;
    public int S_Config_TotalLose = 1;
    public int S_Config_timeStamp = 0;
    
    public List<bool> pigIsPopa = new List<bool>()
    {
        false,
        false,
        false,
        false,
        false,
        false
    };
    
    public int IOS72TimeStamp = 0;
    
    public int maxBetForceMegaWinCount = 0;
    
    public int RookieForceCount = 1;
    
    public int FreeADCount = 0;
    public bool isRemoveAD = false;
    public bool isGuideRookie = true;
    public bool isFristBuy = true;
    public bool _musicEnable = true;
    public bool _soundEnable = true;
    public int coins = 0;
    public double LevelExp = 0;//399990000;
    public int LoginStamp = 0;
    public int LoginCount = 0;
    public int pigAddCoins = 0;
    public int vipCount = 0;
    public int offLineCount = 0; //转盘个数


    public int OffLineStamp = 0;


    public int Day50Counter = 0;
    public int Day50TimeStamp = 0;

    public int Day100Counter = 0;
    public int Day100TimeStamp = 0;

    public int christmasSliderVal0 = 0;
    public int christmasSliderVal1 = 0;
    public int christmasSliderVal2 = 0;
    public int christmasSliderVal3 = 0;
    public int christmasSliderVal4 = 0;
    public int christmasSliderVal5 = 0;
    public int christmasSliderVal6 = 0;
    public int christmasSliderVal7 = 0;
    public int christmasSliderVal8 = 0;
    public int christmasSliderVal9 = 0;
    public int christmasSliderVal10 = 0;
    public int christmasSliderVal11 = 0;
    public int christmasSliderVal12 = 0;
    public int christmasSliderVal13 = 0;
    public int christmasSliderVal14 = 0;
    public int christmasSliderVal15 = 0;
    public int christmasSliderVal16 = 0;
    public int christmasSliderVal17 = 0;
    
    
    public int kingKongSliderVal0 = 0;
    public int kingKongSliderVal1 = 0;
    public int kingKongSliderVal2 = 0;
    public int kingKongSliderVal3 = 0;
    public int kingKongSliderVal4 = 0;
    public int kingKongSliderVal5 = 0;
    public int kingKongSliderVal6 = 0;
    public int kingKongSliderVal7 = 0;
    public int kingKongSliderVal8 = 0;
    public int kingKongSliderVal9 = 0;
    public int kingKongSliderVal10 = 0;
    public int kingKongSliderVal11 = 0;
    public int kingKongSliderVal12 = 0;
    public int kingKongSliderVal13 = 0;
    public int kingKongSliderVal14 = 0;
    public int kingKongSliderVal15 = 0;
    public int kingKongSliderVal16 = 0;
    public int kingKongSliderVal17 = 0;
       
    public int goldSliderVal0 = 0;
    public int goldSliderVal1 = 0;
    public int goldSliderVal2 = 0;
    public int goldSliderVal3 = 0;
    public int goldSliderVal4 = 0;
    public int goldSliderVal5 = 0;
    public int goldSliderVal6 = 0;
    public int goldSliderVal7 = 0;
    public int goldSliderVal8 = 0;
    public int goldSliderVal9 = 0;
    public int goldSliderVal10 = 0;
    public int goldSliderVal11 = 0;
    public int goldSliderVal12 = 0;
    public int goldSliderVal13 = 0;
    public int goldSliderVal14 = 0;
    public int goldSliderVal15 = 0;
    public int goldSliderVal16 = 0;
    public int goldSliderVal17 = 0;
    
    
    public int aladdinSliderVal0 = 0;
    public int aladdinSliderVal1 = 0;
    public int aladdinSliderVal2 = 0;
    public int aladdinSliderVal3 = 0;
    public int aladdinSliderVal4 = 0;
    public int aladdinSliderVal5 = 0;
    public int aladdinSliderVal6 = 0;
    public int aladdinSliderVal7 = 0;
    public int aladdinSliderVal8 = 0;
    public int aladdinSliderVal9 = 0;
    public int aladdinSliderVal10 = 0;
    public int aladdinSliderVal11 = 0;
    public int aladdinSliderVal12 = 0;
    public int aladdinSliderVal13 = 0;
    public int aladdinSliderVal14 = 0;
    public int aladdinSliderVal15 = 0;
    public int aladdinSliderVal16 = 0;
    public int aladdinSliderVal17 = 0;
    
    
    
    public int firuitSliderVal0 = 0;
    public int firuitSliderVal1 = 0;
    public int firuitSliderVal2 = 0;
    public int firuitSliderVal3 = 0;
    public int firuitSliderVal4 = 0;
    public int firuitSliderVal5 = 0;
    public int firuitSliderVal6 = 0;
    public int firuitSliderVal7 = 0;
    public int firuitSliderVal8 = 0;
    public int firuitSliderVal9 = 0;
    public int firuitSliderVal10 = 0;
    public int firuitSliderVal11 = 0;
    public int firuitSliderVal12 = 0;
    public int firuitSliderVal13 = 0;
    public int firuitSliderVal14 = 0;
    public int firuitSliderVal15 = 0;
    public int firuitSliderVal16 = 0;
    public int firuitSliderVal17 = 0;
    
    
    public int royalSlider0 = 0;
    public int royalSlider1 = 0;
    public int royalSlider2 = 0;
    public int royalSlider3 = 0;
    public int royalSlider4 = 0;
    public int royalSlider5 = 0;
    public int royalSlider6 = 0;
    public int royalSlider7 = 0;
    public int royalSlider8 = 0;
    public int royalSlider9 = 0;
    public int royalSlider10 = 0;
    public int royalSlider11 = 0;
    public int royalSlider12 = 0;
    public int royalSlider13 = 0;
    public int royalSlider14 = 0;
    public int royalSlider15 = 0;
    public int royalSlider16 = 0;
    public int royalSlider17 = 0;
    
    
    public int beeSweetSlider0 = 0;
    public int beeSweetSlider1 = 0;
    public int beeSweetSlider2 = 0;
    public int beeSweetSlider3 = 0;
    public int beeSweetSlider4 = 0;
    public int beeSweetSlider5 = 0;
    public int beeSweetSlider6 = 0;
    public int beeSweetSlider7 = 0;
    public int beeSweetSlider8 = 0;
    public int beeSweetSlider9 = 0;
    public int beeSweetSlider10 = 0;
    public int beeSweetSlider11 = 0;
    public int beeSweetSlider12 = 0;
    public int beeSweetSlider13 = 0;
    public int beeSweetSlider14 = 0;
    public int beeSweetSlider15 = 0;
    public int beeSweetSlider16 = 0;
    public int beeSweetSlider17 = 0; 
    
}