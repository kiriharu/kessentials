using KEssentialsKits.Cooldown;
using Kvsl.CooldownManager;
using Kvsl.Utils;
using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Cooldowns = KEssentialsKits.Cooldown.Cooldowns;

namespace KEssentialsKits
{
    public class KEssentialsKits : ModSystem
    {

        public static string KitsConfigName = "kits.json";
        public static string CooldownsConfigName = "kits_cooldowns.json";
        public static KitsConfig LoadedKitsConfig;
        public static ISaveableCooldownManager KitCooldownManagerInstance;
        
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
            
            // Kit Cooldown manager 
            KitCooldownManagerInstance = new KitDictCooldownManager(
                api.LoadOrCreateConf<Cooldowns>(CooldownsConfigName)
            );
            
            var kitsInstance = new Api.DefaultKits();
            
            if (LoadedKitsConfig.kits.Count == 0)
            {
                var defaultKit = kitsInstance.GetDefaultKit();
                LoadedKitsConfig.kits.Add(defaultKit);
                api.StoreModConfig(kitsInstance.GetLoadedKits(), KitsConfigName);
            }

            // Register commands
            api.RegisterCommand(new Commands.Kit(api, kitsInstance));
            api.RegisterCommand(new Commands.Kits(api, kitsInstance));
            
            // Register priveleges
            api.RegisterPrivilegeClass(typeof(Privilege));
            foreach (var kit in LoadedKitsConfig.kits)
            {
                api.Logger.Event($"Register {Privilege.kit}.{kit.name} kit-based permission");
                api
                    .Permissions
                    .RegisterPrivilege($"{Privilege.kit}.{kit.name}", $"Kit {kit.name}", true);
            }

            // Give kits on first join
            api.Event.PlayerCreate += kitsInstance.GiveFirstJoinKits;
            
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
            KitCooldownManagerInstance.Save(_coreServerApi);
            // Kits loading
            LoadedKitsConfig = _coreServerApi.LoadOrCreateConf<KitsConfig>(KitsConfigName);
        }
    }
}
