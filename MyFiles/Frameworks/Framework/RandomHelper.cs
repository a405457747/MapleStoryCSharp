using System.Collections.Generic;
using UnityEngine;

public class RandomHelper 
{
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
        return UnityEngine.Random.Range(-val, val);
    }

    public class Weight<T>
    {
        private readonly List<T> _items;
        private readonly Dictionary<int, int[]> _rangeDic;
        private readonly int _sum;

        public Weight(List<int> weights, List<T> items)
        {
            if (weights.Count != items.Count) Log.LogError("The count inequality.");

            _rangeDic = new Dictionary<int, int[]>();
            _items = items;

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
        }

        public T GetRandomItem()
        {
            var temp = Random.Range(1, _sum + 1);

            var key = GetAccordWithKey(temp);

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