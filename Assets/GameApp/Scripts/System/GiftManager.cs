using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GiftType
{
 Null,
    Wand,//œ…≈Æ∞Ù
} 

public class GiftManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1))
        {
            print("f1 press");
        }

        if (Input.GetKeyUp(KeyCode.F2))
        {
            print("f2 press");
        }
    }
}
