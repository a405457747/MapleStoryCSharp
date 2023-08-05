using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Device
{
    NULL,
    IOS_PAD,
    IOS_PHONE,
    ANDROID_PAD,
    ANDROID_PHONE
}

public class Differences
{
    public static bool Ediotr()
    {
        bool res = false;
#if UNITY_EDITOR
        res = true;
#endif
        return res;
    }

    public static bool Pad()
    {
        var device = GetDevice();
        return (device == Device.IOS_PAD) || (device == Device.ANDROID_PAD);
    }

    public static bool IOS()
    {
        var device = GetDevice();
        return (device == Device.IOS_PAD) || (device == Device.IOS_PHONE);
    }

    public static bool Android()
    {
        var device = GetDevice();
        return (device == Device.ANDROID_PAD) || (device == Device.ANDROID_PHONE);
    }

    public static Device GetDevice()
    {
        Device res = default;
#if UNITY_ANDROID
        float physicscreen = Mathf.Sqrt(Screen.width * Screen.width + Screen.height * Screen.height) / Screen.dpi;
        if (physicscreen >= 7f)
        {
            res = Device.ANDROID_PAD;
            Debug.Log(">=7f");
        }
        else
        {
            res = Device.ANDROID_PHONE;
        }
#elif UNITY_IPHONE
        string iP_genneration = UnityEngine.iOS.Device.generation.ToString();
        //The second Mothod: 
        //string iP_genneration=SystemInfo.deviceModel.ToString();
        if (iP_genneration.Substring(0, 3) == "iPa")
        {
           res = Device.IOS_PAD;
        }
        else
        {
           res = Device.IOS_PHONE;
        }
#endif
        return res;
    }
}