using Kvsl.Utils;
using Kvsl.CooldownManager.Interfaces;
using System.Collections.Generic;

namespace Kvsl.CooldownManager
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

    public class ListCooldownManager : 
        IPlKeyGettableCooldown, 
        IPlGettableCooldowns, 
        IPlKeySettableCoolodown
    {
        
        public Dictionary<string, List<Cooldowns>> Timers = new Dictionary<string, List<Cooldowns>>();
        
        public void SetCooldown(string playerUid, string key, int cooldown)
        {
            if (!Timers.ContainsKey(playerUid))
            {
                Timers[playerUid] = new List<Cooldowns>();   
            }
            Timers[playerUid].Add(new Cooldowns(key, cooldown + DateUtils.GetUnixTimestamp()));
        }

        public int GetCooldown(string playerUid, string key)
        {
            if (!Timers.ContainsKey(playerUid)) return 0;
            var keyCooldown = Timers[playerUid].Find(cooldown => cooldown.Key == key);
            if (keyCooldown == null) return 0;
            var difference = keyCooldown.EndTimeStamp - DateUtils.GetUnixTimestamp();
            if (difference > 0) return difference;
            Timers[playerUid].Remove(keyCooldown);
            return 0;
        }

        public Dictionary<string, int> GetCooldowns(string playerUid)
        {
            var dict = new Dictionary<string, int>();
            if (!Timers.ContainsKey(playerUid)) return dict;
            Timers[playerUid].ForEach(
                cooldown =>
                {
                    var itemCooldown = cooldown.EndTimeStamp - DateUtils.GetUnixTimestamp();
                    if (itemCooldown > 0)
                    {
                        dict.Add(cooldown.Key, itemCooldown);   
                    }
                });
            return dict;
        }
    }
}