using System.Collections.Generic;

namespace Kvsl.CooldownManager
{
    public interface ICooldownManager
    {
        void SetCooldown(string playerUid, string key, int cooldown);
        int GetCooldown(string playerUid, string key);
        Dictionary<string, int> GetCooldowns(string playerUid);
        bool IsInCooldown(string playerUid, string key);
    }
}