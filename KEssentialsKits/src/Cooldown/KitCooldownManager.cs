using System.Collections.Generic;
using Kvsl;
using Vintagestory.API.Server;

namespace KEssentialsKits.Cooldown
{
    public class KitCooldownManager : CooldownManager
    {

        public KitCooldownManager(Cooldowns cooldownsObj) : base()
        {
            foreach (var cooldownsObjUsageCooldown in cooldownsObj.usageCooldowns)
            {
                Timers.Add(cooldownsObjUsageCooldown.playerUID, new List<Kvsl.Cooldowns>());
                foreach (var kitTimer in cooldownsObjUsageCooldown.timers)
                {
                    Timers[cooldownsObjUsageCooldown.playerUID].Add(
                        new Kvsl.Cooldowns(kitTimer.kit, kitTimer.endTimestamp)
                    );
                }
            }
            
        }

        public void SaveToConfig(ICoreServerAPI serverApi)
        {
            serverApi.Logger.Event("Starting to save kits cooldowns to file.");
            var cooldownsFile = new Cooldowns();
            foreach (var timerEntry in Timers)
            {
                var playerCooldowns = new List<KitTimer>();
                foreach (var cooldownObj in timerEntry.Value)
                {
                    playerCooldowns.Add(new KitTimer(cooldownObj.Key, cooldownObj.EndTimeStamp));
                    
                }
                cooldownsFile.usageCooldowns.Add(new User(timerEntry.Key, playerCooldowns));
                serverApi.StoreModConfig(cooldownsFile, KEssentialsKits.CooldownsConfigName);
                serverApi.Logger.Event("Kits cooldowns saved to file.");
            }  
        }
    }
}