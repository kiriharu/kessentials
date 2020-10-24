using System.Collections.Generic;
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
        
        public override bool ShouldLoad(EnumAppSide side)
        {
            return side == EnumAppSide.Server;
        }
        
        public override void StartServerSide(ICoreServerAPI api)
        {
            base.StartServerSide(api);
            
            // Kit Config register
            LoadedKitsConfig = api.LoadOrCreateConf<KitsConfig>(KitsConfigName);
            if (LoadedKitsConfig.kits.Count == 0)
            {
                api.Logger.Warning($"Oh, i don't see any kit in {KitsConfigName} file. Generating a new one...");
                var defaultKit = new Kit("start", new List<Item>(new[]
                {
                    new Item(ItemType.item, "game:gear-temporal", 1),
                    new Item(ItemType.item, "game:vegetable-carrot", 10),
                    new Item(ItemType.item, "game:pickaxe-iron", 1),
                    new Item(ItemType.block, "game:soil-medium-none", 20),
                }), 30, true);
                LoadedKitsConfig.kits.Add(defaultKit);
                api.StoreModConfig(LoadedKitsConfig, KitsConfigName);
            }
            
            KitCooldownManagerInstance = new KitCooldownManager(
                api.LoadOrCreateConf<Cooldowns>(CooldownsConfigName)
            );
            
            // Register commands
            api.RegisterCommand(new Commands.Kit(api));
            api.RegisterPrivilegeClass(typeof(Privilege));
            foreach (var kit in LoadedKitsConfig.kits)
            {
                api.Logger.Event($"Register {Privilege.kit}.{kit.name} kit-based permission");
                api
                    .Permissions
                    .RegisterPrivilege($"{Privilege.kit}.{kit.name}", "Kit", true);
            }
            api.Event.PlayerCreate += Utils.GiveFirstJoinKits;
        }

    }
        
}
