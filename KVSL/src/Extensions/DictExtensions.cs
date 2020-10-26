using System.Collections.Generic;

namespace Kvsl.Extensions
{
    public static class DictExtensions
    {
        private static void AddOrSet<TK, TV>(this Dictionary<TK, TV> dictionary, TK key, TV value) {
            if (dictionary.ContainsKey(key)) {
                dictionary[key] = value;
            } else {
                dictionary.Add(key, value);
            }
        }
    }
}