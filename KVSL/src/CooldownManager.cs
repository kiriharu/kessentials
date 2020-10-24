using System;
using System.Collections.Generic;

namespace Kvsl
{
    public class CooldownManager
    {
        
        public Dictionary<string, Dictionary<string, int>> Timers;

        public CooldownManager()
        {
            Timers = new Dictionary<string, Dictionary<string, int>>();
        }

        public static int GetUnixTimestamp()
        {
            return (int) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
        
        public void SetCooldown(string playerUid, string key, int cooldown)
        {
            Timers[playerUid] = new Dictionary<string, int>
            {
                {key, cooldown + GetUnixTimestamp()}
            };
        }

        public int GetCooldown(string playerUid, string key)
        {
            if (!Timers.ContainsKey(playerUid)) return 0;
            if (!Timers[playerUid].ContainsKey(key)) return 0;
            var unixTimeToFinish = Timers[playerUid][key];
            var difference = unixTimeToFinish - GetUnixTimestamp();
            if (difference > 0) return difference;
            Timers[playerUid].Remove(key);
            return 0;
        }

        public bool IsInCooldown(string playerUid, string key)
        {
            return GetCooldown(playerUid, key) > 0;
        }
    }
}