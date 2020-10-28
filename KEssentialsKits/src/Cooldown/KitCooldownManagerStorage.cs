using System.Collections.Generic;
using Kvsl.CooldownManager.Storage;
using Vintagestory.API.Server;

namespace KEssentialsKits.Cooldown
{
    public class KitCooldownManagerStorage : ListCooldownManagerStorage<Cooldowns>
    {
        public KitCooldownManagerStorage(ICoreServerAPI api, string configName) : base(api, configName)
        {
            
        }

        public override void Save()
        {
            ServerApi.Logger.Event("Starting to save kits cooldowns to file.");
            var cooldownsFile = new Cooldowns();
            foreach (var timerEntry in CooldownManager.Timers)
            {
                var playerCooldowns = new List<KitTimer>();
                foreach (var cooldownObj in timerEntry.Value)
                {
                    playerCooldowns.Add(new KitTimer(cooldownObj.Key, cooldownObj.EndTimeStamp));
                    
                }
                cooldownsFile.usageCooldowns.Add(new User(timerEntry.Key, playerCooldowns));
                // TODO: Maybe it's don't need
                ServerApi.StoreModConfig(cooldownsFile, ConfigName);
                ServerApi.Logger.Event("Kits cooldowns saved to file.");
            }  
        }

        public override void Load()
        {
            foreach (var cooldownsObjUsageCooldown in Storage.StorageClass.usageCooldowns)
            {
                CooldownManager.Timers.Add(cooldownsObjUsageCooldown.playerUID, 
                    new List<Kvsl.CooldownManager.Cooldowns>());
                foreach (var kitTimer in cooldownsObjUsageCooldown.timers)
                {
                    CooldownManager.Timers[cooldownsObjUsageCooldown.playerUID].Add(
                        new Kvsl.CooldownManager.Cooldowns(kitTimer.kit, kitTimer.endTimestamp)
                    );
                }
            }
        }
    }
}