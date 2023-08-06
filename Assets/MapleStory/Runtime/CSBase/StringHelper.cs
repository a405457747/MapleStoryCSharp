using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MapleStory
{
    public static class StringHelper
    {
        public static string ThousandsCoin(int v)
        {
            var str = v.ToString("N");
            var str1 = str.Split('.')[0];
            return str1;
        }

        public static string RemoveColons(string str)
        {
            if (str == "" || str.Length < 2) return str;

            var t = str.ToList();
            t.RemoveAt(t.Count - 1);
            t.RemoveAt(0);
            return new string(t.ToArray());
        }

        public static string DollarFormat(double num, bool isIgnorePoint = true)
        {
            var str = string.Format("{0:N}", num);
            if (isIgnorePoint)
                return str.Replace(".00", "");
            return str;
        }

        public static int GetPureNumber(string str)
        {
            var newStr = Regex.Replace(str, "[^0-9]*", "");
            if (newStr == "") return 0;
            return int.Parse(newStr);
        }

        public static string StrRepeat(string demo, int repeatCount)
        {
            StringBuilder sb = new StringBuilder();
            int step = 0;
            while (step < repeatCount)
            {
                sb.Append(demo);
                step += 1;
            }

            return sb.ToString();
        }

        public static string GetRatio(float val, string format = "f1")
        {
            var tempVal = val;
            tempVal *= 100;
            return tempVal.ToString(format) + "%";
        }

        /*
        public static string GetFormatVersion(int version, string fomat = ".")
        {
            var charList = version.ToString().ToList();
    
            while (charList.Count != 6) charList.Insert(0, '0');
    
            var list = new List<string>();
            for (var i = 0; i < charList.Count; i += 2)
            {
                var tempStr = charList[i] + charList[i + 1].ToString();
                var tempInt = int.Parse(tempStr);
                list.Add(tempInt.ToString());
            }
    
            return string.Join(fomat, list);
        }
        */

        public static string GetShortForNumber(long num, string digits = "f1")
        {
            var numBit = num.ToString().Length;

            long numFloat;
            checked
            {
                numFloat = num;
            }

            if (numBit < 4) return num.ToString();

            if (numBit < 7)
            {
                numFloat /= 1000L;
                return $"{numFloat.ToString(digits)}K";
            }

            if (numBit < 10)
            {
                numFloat /= 1000000L;
                return $"{numFloat.ToString(digits)}M";
            }

            if (numBit < 13)
            {
                numFloat /= 1000000000L;
                return $"{numFloat.ToString(digits)}B";
            }

            if (numBit < 16)
            {
                numFloat /= 1000000000000L;
                return $"{numFloat.ToString(digits)}T";
            }

            if (numBit < 19)
            {
                numFloat /= 1000000000000000L;
                return $"{numFloat.ToString(digits)}aa";
            }

            if (num < long.MaxValue)
            {
                numFloat /= 1000000000000000000L;
                return $"{numFloat.ToString(digits)}bb";
            }

            //cc uu
            return "Max";
        }
    }
}