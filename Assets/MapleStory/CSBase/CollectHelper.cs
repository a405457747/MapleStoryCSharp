
using System.Collections.Generic;

namespace MapleStory
{
    public static class CollectHelper
    {
        public static T GetRuleItem<T>(List<T> list, ref int index)
        {
            return list[index++ % list.Count];
        }

        public static T ListRemoveHead<T>(List<T> lis)
        {
            int headIndex = 0;
            T res = lis[headIndex];
            lis.RemoveAt(headIndex);
            return res;
        }
    }
}