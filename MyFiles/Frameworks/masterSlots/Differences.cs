using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Devices
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
        if (GData.QuickPad)
        {
            return true;
        }
        
        var device = GetDevice();
        return (device == Devices.IOS_PAD) || (device == Devices.ANDROID_PAD);
    }

    public static bool IOS()
    {
        var device = GetDevice();
        return (device == Devices.IOS_PAD) || (device == Devices.IOS_PHONE);
    }

    public static bool Android()
    {
        var device = GetDevice();
        return (device == Devices.ANDROID_PAD) || (device == Devices.ANDROID_PHONE);
    }

    public static Devices GetDevice()
    {
        Devices res = default;
#if UNITY_ANDROID
        float physicscreen = Mathf.Sqrt(Screen.width * Screen.width + Screen.height * Screen.height) / Screen.dpi;
        if (physicscreen >= 7f)
        {
            res = Devices.ANDROID_PAD;
            Debug.Log(">=7f");
        }
        else
        {
            res = Devices.ANDROID_PHONE;
        }

#if UNITY_EDITOR
        res = Devices.ANDROID_PHONE;
#endif
        
#elif UNITY_IPHONE
        string iP_genneration = UnityEngine.iOS.Device.generation.ToString();
        //The second Mothod: 
        //string iP_genneration=SystemInfo.deviceModel.ToString();
        if (iP_genneration.Substring(0, 3) == "iPa")
        {
           res = Devices.IOS_PAD;
        }
        else
        {
           res = Devices.IOS_PHONE;
        }
#endif
        return res;
    }
}