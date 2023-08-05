using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FontSize : MonoBehaviour
{
    public enum Sizes
    {
        S,
        M,
        L,
    }

    [FormerlySerializedAs("Size")] public Sizes size;

    private void Awake()
    {
        var txt = GetComponent<Text>();

        switch (size)
        {
            case Sizes.S:
                txt.fontSize = 23;
                break;
            case Sizes.M:
                txt.fontSize = 25;
                break;
            case Sizes.L:
                txt.fontSize = 27;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}