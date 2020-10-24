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
            LoadedKitsConfig = api.LoadOrCreateConf<KitsConfig>(KitsConfigName);
            KitCooldownManagerInstance = new KitCooldownManager(api.LoadOrCreateConf<Cooldowns>(CooldownsConfigName));
            api.RegisterCommand(new Commands.Kit(api));
            api.RegisterPrivilegeClass(typeof(Privilege));
            foreach (var kit in LoadedKitsConfig.kits)
            {
                api.Permissions.RegisterPrivilege($"{Privilege.kit}.{kit.name}", "Kit", true);
            }
        }
        
    }
        
}
