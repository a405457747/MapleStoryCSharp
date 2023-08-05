using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOver : Controller
{
    private TimeModel tModel;
    private SaveMap sMap;

    public override void HandleEvent(object data)
    {
        var game = Game.I;
        tModel = game._superTimeAndTimeModel;
        sMap = Game.I.saveMap;
        game.hallPanel.BuyButtonAction = () => { game.shopingPanel.Show(); };
        bool isShowLogin = false;
        int finalCoins = default;
        if (game._superTimeAndTimeModel.IsMeet(game._superTimeAndTimeModel.intervalLogin, Game.I.saveMap.LoginStamp, out int remaining))
        {
            Game.I.saveMap.LoginStamp = game._superTimeAndTimeModel.NowTimeStamp(); //更新时间戳 
            game.saveMap.LoginCount += 1;

            if (game._superTimeAndTimeModel.dayCoinDic.ContainsKey(game.saveMap.LoginCount))
            {
                finalCoins = game._superTimeAndTimeModel.GetFinalCoins;


                isShowLogin = true;
            }

            //这里已经保存了的哦
            game.saveSystem.Save();
        }

        game._LoginPanel.UpdateUIs();
        if (isShowLogin)
        {
            game._LoginPanel.Show();
            game._LoginPanel.DelayAddMoney(() =>
            {
                game.myUserAndYouPlayerSettingsAndHappyModel.AddCoins(finalCoins);
                Log.LogParas("加钱了对于登录奖励来说 加了:" + finalCoins);
            game.myUserAndYouPlayerSettingsAndHappyModel.CoinFly();
                game.saveSystem.Save();
            });
        }
        else
        {
            game._LoginPanel.HideBottomGameObject();
            
             game.myUserAndYouPlayerSettingsAndHappyModel.userModelPanelsOpen(); 
        }

        if (game.saveMap.LoginCount >= 30)
        {
            game.saveMap.LoginCount = 0;
            game.saveSystem.Save();
        }

        game.audioSystem.LOAD_OVER_Callback();
        // Offline();
        // game.hallPanel._slideShow.CtralAnimal();
        
        
        game._HolidayDayPanel.ShowInit();
    }
}