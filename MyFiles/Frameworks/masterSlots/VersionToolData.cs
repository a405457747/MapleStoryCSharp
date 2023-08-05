/*⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵
☠ ©2020 Chengdu Mighty Vertex Games. All rights reserved.                                                                        
⚓ Author: SkyAllen                                                                                                                  
⚓ Email: 894982165@qq.com                                                                                  
⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵*/

using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
[CreateAssetMenu]
public class VersionToolData : ScriptableObject
{
    [FormerlySerializedAs("Channel")] public AndroidChannel androidChannel;
    [FormerlySerializedAs("Debug")] public bool OpenAPK;
    public bool OpenLog;
    public bool OpenTestAD;
}

[Serializable]
public enum AndroidChannel
{
    Null,
    Google,
    Amazon,
}

[Serializable]
public enum PackageType
{
    Null,
    aab,
    apk,
    ipa
}