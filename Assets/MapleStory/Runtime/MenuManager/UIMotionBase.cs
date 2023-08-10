using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MapleStory;
using System.Linq;
using static MapleStory.LogNote;


namespace MapleStory
{
    public abstract class UIMotionBase : MonoBehaviour
    {
        [SerializeField] protected float motionCost;
    }
}