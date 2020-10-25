﻿using System;
using System.Collections.Generic;

namespace Kvsl
{

    public class Cooldowns
    {
        public string Key;
        public int EndTimeStamp;
        
        public Cooldowns(string key, int endTimeStamp)
        {
            Key = key;
            EndTimeStamp = endTimeStamp;
        }
    }

    public class CooldownManager
    {
        
        public Dictionary<string, List<Cooldowns>> Timers;

        public CooldownManager()
        {
            Timers = new Dictionary<string, List<Cooldowns>>();
        }

        public static int GetUnixTimestamp()
        {
            return (int) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
        
        public void SetCooldown(string playerUid, string key, int cooldown)
        {
            if (!Timers.ContainsKey(playerUid))
            {
                Timers[playerUid] = new List<Cooldowns>();   
            }
            Timers[playerUid].Add(new Cooldowns(key, cooldown + GetUnixTimestamp()));
        }

        public int GetCooldown(string playerUid, string key)
        {
            if (!Timers.ContainsKey(playerUid)) return 0;
            var keyCooldown = Timers[playerUid].Find(cooldown => cooldown.Key == key);
            if (keyCooldown == null) return 0;
            var difference = keyCooldown.EndTimeStamp - GetUnixTimestamp();
            if (difference > 0) return difference;
            Timers[playerUid].Remove(keyCooldown);
            return 0;
        }

        public bool IsInCooldown(string playerUid, string key)
        {
            return GetCooldown(playerUid, key) > 0;
        }
    }
}