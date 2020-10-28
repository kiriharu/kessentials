using System.Collections.Generic;
using Kvsl.CooldownManager.Interfaces;
using Kvsl.Extensions;

namespace Kvsl.CooldownManager
{
    public class DictCooldownManager : IPlSettableCooldown, IPlGettableCooldown
    {
        
        Dictionary<string, int> _сooldowns = new Dictionary<string, int>();
        
        
        public void SetCooldown(string playerUid, int cooldown)
        {
            _сooldowns.AddOrSet(playerUid, cooldown);
        }

        public int GetCooldown(string playerUid)
        {
            return !_сooldowns.ContainsKey(playerUid) ? 0 : _сooldowns[playerUid];
        }
    }
}