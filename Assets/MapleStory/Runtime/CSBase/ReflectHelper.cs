
using System;
using System.Linq;

namespace MapleStory
{
    public static class ReflectHelper
    {
        public static T GetField<T>(object ins, string name)
        {
            var temp = ins.GetType().GetField(name).GetValue(ins);

            return (T)temp;
        }
    }
}