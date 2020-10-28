using System;
using System.Collections.Generic;

namespace Kvsl.Utils
{
    public static class TypeUtils
    {
        public static T CreateInstance<T>(Type clazz, params object[] paramArray)
        {
            return (T)Activator.CreateInstance(clazz, args:paramArray);
        }

        public static T GetRandomItem<T>(this List<T> list) 
        {
            var rnd = new Random();
            return list[rnd.Next(list.Count)];
        }
        
    }
}