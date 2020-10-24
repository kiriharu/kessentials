using System.Collections.Generic;
using Kvsl;
using Vintagestory.API.Server;

namespace KEssentialsKits
{
    public class KitCooldownManager : CooldownManager
    {

        public KitCooldownManager(Cooldowns cooldownsObj) : base()
        {
            // Load from config
            cooldownsObj.usageCooldowns?.ForEach(
                x => x.timers.ForEach(
                    y => Timers[x.playerUID].Add(y.kit, y.endTimestamp)
                )
            );
        }

        public void SaveToConfig(ICoreServerAPI serverApi)
        {
            var cooldownsFile = new Cooldowns();
            foreach (var entry in Timers)
            {
                var playerCooldowns = new List<KitTimer>();
                foreach (var playerCooldown in entry.Value)
                {
                    playerCooldowns.Add(new KitTimer(playerCooldown.Key, playerCooldown.Value));
                }
                cooldownsFile.usageCooldowns.Add(new User(entry.Key, playerCooldowns));
                serverApi.StoreModConfig(cooldownsFile, KEssentialsKits.CooldownsConfigName);
            }
        }
    }
}