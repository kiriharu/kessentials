using KEssentialsKits.Cooldown;
using Kvsl.CooldownManager;
using Kvsl.CooldownManager.Storage;
using Kvsl.Extensions;
using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Cooldowns = KEssentialsKits.Cooldown.Cooldowns;

namespace KEssentialsKits
{
    public class KEssentialsKits : ModSystem
    {

        public static string KitsConfigName = "kits.json";
        public static string CooldownsConfigName = "kits_cooldowns.json";
        private KitCooldownManagerStorage KitsCooldownManagerStorage;
        public static ListCooldownManager KitsCooldownManager;
        public static KitsStorageClass LoadedKitsStorageClass;
        
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
            LoadedKitsStorageClass = api.LoadOrCreateConf<KitsStorageClass>(KitsConfigName);
            
            // Kit Cooldown manager 
            KitsCooldownManagerStorage = new KitCooldownManagerStorage(_coreServerApi, CooldownsConfigName);
            KitsCooldownManagerStorage.Load();
            KitsCooldownManager = KitsCooldownManagerStorage.CooldownManager;

            var kitsInstance = new Api.DefaultKits();
            
            if (LoadedKitsStorageClass.kits.Count == 0)
            {
                var defaultKit = kitsInstance.GetDefaultKit();
                LoadedKitsStorageClass.kits.Add(defaultKit);
                api.StoreModConfig(kitsInstance.GetLoadedKits(), KitsConfigName);
            }

            // Register commands
            api.RegisterCommand(new Commands.Kit(api, kitsInstance));
            api.RegisterCommand(new Commands.Kits(api, kitsInstance));
            
            // Register priveleges
            api.RegisterPrivilegeClass(typeof(Privilege));
            foreach (var kit in LoadedKitsStorageClass.kits)
            {
                api.Logger.Event($"Register {Privilege.kit}.{kit.name} kit-based permission");
                api
                    .Permissions
                    .RegisterPrivilege($"{Privilege.kit}.{kit.name}", $"Kit {kit.name}", true);
            }

            // Give kits on first join
            api.Event.PlayerCreate += kitsInstance.GiveFirstJoinKits;
            
            // Saving cooldowns to file
            api.Event.Timer(CooldownSaveTimer, 3600);
        }
        
        /// <summary>
        /// Save cooldowns to file and update kits on reload and shutdown
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            // Save cooldowns
            KitsCooldownManagerStorage.Save();
            // Kits loading
            LoadedKitsStorageClass = _coreServerApi.LoadOrCreateConf<KitsStorageClass>(KitsConfigName);
        }

        private void CooldownSaveTimer()
        {
            KitsCooldownManagerStorage.Save();
        }
    }
}
