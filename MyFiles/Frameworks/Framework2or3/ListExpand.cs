/*⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵
☠ ©2020 Chengdu Mighty Vertex Games. All rights reserved.                                                                        
⚓ Author: SkyAllen                                                                                                                  
⚓ Email: 894982165@qq.com                                                                                  
⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵*/

using System.Collections.Generic;

public static class ListExpand
{
    public static T GetRuleItem<T>(this List<T> list, ref int index)
    {
        return list[index++ % list.Count];
    }
}