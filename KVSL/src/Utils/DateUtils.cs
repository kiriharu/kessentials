using System;

namespace Kvsl.Utils
{
    public static class DateUtils
    {
        
        public static int GetUnixTimestamp()
        {
            return (int) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
        
    }
}