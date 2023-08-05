using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinStart : Controller
{
    public override void HandleEvent(object data)
    {
        var game = Game.I;
        game.SuPerSlotsAndCoreAndBrainsAndCoreGoodModel.SpinStart();
       
        game.levelModel.SetCurLvAndRatio();
    }
}