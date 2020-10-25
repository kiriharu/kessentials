using System;
using System.Collections.Generic;
using KEssentialsKits.Cooldown;
using Kvsl.Utils;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace KEssentialsKits
{
    public class KEssentialsKits : ModSystem
    {

        public static string KitsConfigName = "kits.json";
        public static string CooldownsConfigName = "kits_cooldowns.json";
        public static KitsConfig LoadedKitsConfig;
        public static KitCooldownManager KitCooldownManagerInstance;
        
        // Used only in Dispose() ¯\_(ツ)_/¯. 
        private ICoreServerAPI _coreServerApi;

        public override bool ShouldLoad(EnumAppSide side)
        {
            return side == EnumAppSide.Server;
        }

        public override void StartServerSide(ICoreServerAPI api)
        {
            base.StartServerSide(api);
            _coreServerApi = api;

            // Kit Config register
            LoadedKitsConfig = api.LoadOrCreateConf<KitsConfig>(KitsConfigName);
            if (LoadedKitsConfig.kits.Count == 0)
            {
                api.Logger.Warning($"Oh, i don't see any kit in {KitsConfigName} file. Generating a new one...");
                var defaultKit = new Kit("start", new List<Item>(new[]
                {
                    new Item(ItemType.item, "game:gear-temporal", 1),
                    new Item(ItemType.item, "game:bread-rice", 4),
                    new Item(ItemType.item, "game:flint", 5),
                    new Item(ItemType.item, "game:stick", 4),
                    new Item(ItemType.block, "game:torch-up", 2)
                    
                }), 84600, true);
                LoadedKitsConfig.kits.Add(defaultKit);
                api.StoreModConfig(LoadedKitsConfig, KitsConfigName);
            }

            // Cooldown manager
            KitCooldownManagerInstance = new KitCooldownManager(
                api.LoadOrCreateConf<Cooldowns>(CooldownsConfigName)
            );

            // Register commands
            api.RegisterCommand(new Commands.Kit(api));
            api.RegisterCommand(new Commands.Kits(api));
            api.RegisterPrivilegeClass(typeof(Privilege));
            foreach (var kit in LoadedKitsConfig.kits)
            {
                api.Logger.Event($"Register {Privilege.kit}.{kit.name} kit-based permission");
                api
                    .Permissions
                    .RegisterPrivilege($"{Privilege.kit}.{kit.name}", $"Kit {kit.name}", true);
            }

            // Give kits on first join
            api.Event.PlayerCreate += Utils.GiveFirstJoinKits;
            
            // Save timer
            api.RegisterKvslTimer(typeof(CooldownSaveTimer));
        }
        
        /// <summary>
        /// Save cooldowns to file and update kits on reload and shutdown
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            // Save cooldowns
            KitCooldownManagerInstance.SaveToConfig(_coreServerApi);
            // Kits loading
            LoadedKitsConfig = _coreServerApi.LoadOrCreateConf<KitsConfig>(KitsConfigName);
        }
    }
}
