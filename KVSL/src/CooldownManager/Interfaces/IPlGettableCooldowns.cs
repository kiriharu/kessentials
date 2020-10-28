using System.Collections.Generic;

namespace Kvsl.CooldownManager.Interfaces
{
    public interface IPlGettableCooldowns
    {
        Dictionary<string, int> GetCooldowns(string playerUid);
    }
}