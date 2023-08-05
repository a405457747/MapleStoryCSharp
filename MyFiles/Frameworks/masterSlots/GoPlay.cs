using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoPlay : Controller
{
    public override void HandleEvent(object data)
    {
        var game = Game.I;
        game.hallPanel.CtrlSomeUI(false);
        game.playPanel.Show();
        int lv = (int) data;
        Log.LogParas("fixed id is lv" + lv);
        

        MachineUI machineUI = game.playPanel.SetLevelImageShow(lv);
        game.SuPerSlotsAndCoreAndBrainsAndCoreGoodModel.InitCurMachine(lv, machineUI);
        game.hallPanel.ReFreshTotalWin();


        Sprite normalLvSprite = Factorys.GetAssetFactory().LoadSprite($"bj{lv}");
        game.hallPanel.ChangeBG(normalLvSprite);
        game.myUserAndYouPlayerSettingsAndHappyModel.guideOpen(2);
        
 
        
        //game.levelModel.ToMaxBetIndex();
    }
}