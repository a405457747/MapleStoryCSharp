using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class RandomHelper
{
    public static T RandomItem<T>(ICollection<T> ic)
    {
        return ic.ElementAt(Random.Range(0, ic.Count));
    }

    public static ICollection<T> CollectionsRandomItem<T>(ICollection<ICollection<T>> icc)
    {
        return icc.ElementAt(Random.Range(0, icc.Count));
    }

    public static List<T> GetTinyAfterShuffle<T>(List<T> pocker, int count)
    {
        var res = new List<T>();

        for (var i = 0; i < count; i++) res.Add(pocker[i]);

        return res;
    }

    public static void Shuffle<T>(List<T> poker)
    {
        for (var i = poker.Count - 1; i >= 0; i--)
        {
            var randomIndex = Random.Range(0, i + 1);
            var temp = poker[randomIndex];
            poker[randomIndex] = poker[i];
            poker[i] = temp;
        }
    }

    public static bool CanBirthPrecision1000(float birthProbability)
    {
        var random = Random.Range(1, 1001);
        var birthValue = (int) (birthProbability * 1000);
        return random <= birthValue;
    }

    public static float GetRandomPlusAndMinus(float val)
    {
        return Random.Range(-val, val);
    }

    public static List<int> GetWeightItemByNormalRange(List<int> normalRange)
    {
        var res = Enumerable.Repeat(0, normalRange.Count).ToList();
        for (var i = 0; i < normalRange.Count; i++)
        {
            var item = normalRange[i];
            var temp = item - res.Take(i).Sum();
            res[i] = temp;
        }

        return res;
    }

    public class Weight<T>
    {
        private readonly List<T> _items;
        private readonly Dictionary<int, int[]> _rangeDic;
        private readonly int _sum;
        private readonly List<int> _weights;

        public Weight(List<int> weights, List<T> items)
        {
            if (weights.Count != items.Count) throw new Exception("The count inequality.");

            _rangeDic = new Dictionary<int, int[]>();

            var first = 0;
            var second = 0;

            for (var i = 0; i < weights.Count; i++)
            {
                var arr = new int[2];

                first = second;
                second = weights[i] + first;

                arr[0] = first;
                arr[1] = second;

                _rangeDic[i] = arr;
            }

            _sum = _rangeDic[weights.Count - 1][1];

            _weights = weights;
            _items = items;
        }

        public string ShowPercentage()
        {
            var res = string.Empty;
            for (var i = 0; i < _rangeDic.Count; i++)
            {
                var kv = _rangeDic.ElementAt(i);
                var arr = kv.Value;
                var per = (float) (arr[1] - arr[0]) / _sum;
                res = $"{res}{_items[i]}probability:{StringHelper.GetRatio(per, "f5")}weight:{_weights[i]}\r\n";
            }

            return res;
        }

        public static int GetWeightByDoubleString(string floatString, int w = 1000000)
        {
            double f = default;
            try
            {
                f = double.Parse(floatString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return (int) (f * w);
        }


        public T GetRandomItem()
        {
            var temp = Random.Range(1, _sum + 1);
            return GetItemByNumber(temp);
        }

        public T GetItemByNumber(int number)
        {
            var key = GetAccordWithKey(number);
            return _items[key];
        }

        private int GetAccordWithKey(int randomNumber)
        {
            foreach (var kv in _rangeDic)
            {
                var arr = kv.Value;

                if (randomNumber > arr[0] && randomNumber <= arr[1]) return kv.Key;
            }

            return -1;
        }
    }
}