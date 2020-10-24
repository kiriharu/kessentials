using Kvsl.Utils;
using Vintagestory.API.Server;

namespace KEssentialsKits
{
    public static class Utils
    {
        public static void GiveKit(this IServerPlayer player, string kitName)
        {
            var kit = KEssentialsKits.LoadedKitsConfig.kits.Find(
                x => x.name == kitName
            );
            if (kit != null)
            {
                var cooldown = KEssentialsKits.KitCooldownManagerInstance.GetCooldown(player.PlayerUID, kitName);
                if (cooldown > 0)
                {
                    player.SendErr($"You must wait {cooldown} sec. to use this kit");
                    return;
                }
                if (!player.HasPrivilege($"{Privilege.kit}.{kit.name}"))
                {
                    player.SendErr($"You don't have access to kit {kit.name}");
                    return;
                }
                player.SendOk($"Giving {kit.name}");
                kit.items.ForEach(
                    item => player.GiveItemStack(item.type, item.code, item.amount)
                );
                KEssentialsKits.KitCooldownManagerInstance.SetCooldown(player.PlayerUID, kitName, kit.delay);
            }
            else player.SendErr( $"{kitName} not found");
        }
        
    }
}