using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class ListUtils
    {
        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static T GetRandomElement<T>(IList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
    }
}