using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class MiniGame16Wheel : AwardWheel
{
    public override AwardWheelData GetTargetData()
    {
        return Datas[fixedIndex];
    }

    
    public override void Init(Transform rotateTrans, Action<AwardWheelData> func)
    {
        var betList = new List<int>
        {
           1,2,3,4,5,6
        };
        List<float> weights = new List<float>()
        {
          0.1f,0.1f,0.1f   ,0.1f,0.1f,0.1f,
        };

        Assert.IsTrue(weights.All(item => item < 1f));
        Assert.IsTrue(betList.Count == weights.Count);
        this.overCallback = func;
        _copies = betList.Count;

        _weight = new RandomHelper.Weight<int>(RandomHelper.GetWeightByFloat(weights),
            GetIndexResults());
        Datas = new Dictionary<int, AwardWheelData>();

        for (var i = 0; i < _copies; i++)
        {
            var data = new AwardWheelData();
            data.Bet = betList[i];
            data.Degree = GetDegree(i);
            data.idx = i;
            Datas.Add(i, data);
        }

        IsRun = false;
        rotateTransform = rotateTrans;
        curPos = 0;
        targetPos = 0;
        cost1 = 4;
        cost2 = 1;
        speed = GetSpeed();
        SetRotateZ(0);
    }
}