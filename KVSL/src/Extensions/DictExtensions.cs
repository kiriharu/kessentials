using System.Collections.Generic;

namespace Kvsl.Extensions
{
    public static class DictExtensions
    {
        public static void AddOrSet<TK, TV>(this IDictionary<TK, TV> dictionary, TK key, TV value) {
            if (dictionary.ContainsKey(key)) {
                dictionary[key] = value;
            } else {
                dictionary.Add(key, value);
            }
        }
    }
}