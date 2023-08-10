

using UnityEngine;

namespace MapleStory
{
    public static class ImageHelper
    {
        public static Color GetColor(int r, int g, int b, int a = 255)
        {
            return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
        }

        public static Color GetColor(string c)
        {
            Color res = default;

            c = "#" + c;

            ColorUtility.TryParseHtmlString(c, out res);
            
            return res;
        }
    }
}