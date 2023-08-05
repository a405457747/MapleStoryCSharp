/*⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵
☠ ©2020 Chengdu Mighty Vertex Games. All rights reserved.                                                                        
⚓ Author: SkyAllen                                                                                                                  
⚓ Email: 894982165@qq.com                                                                                  
⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵*/

using System;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
internal class AutoRunActionAttribute : Attribute
{
    public string Description { get; }
}

/*
private void OtherTest()
{
    var asm = Assembly.GetExecutingAssembly();
    var types = asm.GetTypes();

    foreach (var t in types)
    {
        var _methods = t.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);

        foreach (var m in _methods)
        {
            var _attrs = m.GetCustomAttributes(typeof(AutoRunActionAttribute), false);
            if (_attrs.Length > 0)
            {
                m.Invoke(null, new object[] { });
                break;
            }
        }
    }
}
*/