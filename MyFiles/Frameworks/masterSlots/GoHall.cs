using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GoHall : Controller
{
    public override void HandleEvent(object data)
    {
        var game = Game.I;
        
        game.SuPerSlotsAndCoreAndBrainsAndCoreGoodModel.CurMachine.StopAllAnimal();
        
        game.hallPanel.CtrlSomeUI(true);
        game.playPanel.Hide();

        Sprite normalLvSprite = Factorys.GetAssetFactory().LoadSprite($"bj");
        game.hallPanel.ChangeBG(normalLvSprite);
        
      game.myUserAndYouPlayerSettingsAndHappyModel.  userModelPanelsOpen();
      
      game.audioSystem.PlayMusicAndSwitch("public_BGM");
      
      
      game._HolidayDayPanel.ShowInit();
    }


}