using System.Linq;
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
                if (!player.HasPrivilege(Privilege.ignoreCooldowns))
                {
                    var cooldown = KEssentialsKits.KitCooldownManagerInstance.GetCooldown(player.PlayerUID, kitName);
                    if (cooldown > 0)
                    {
                        player.SendErr($"You must wait {cooldown} sec. to use this kit");
                        return;
                    }   
                }
                if (!player.HasPrivilege($"{Privilege.kit}.{kit.name}"))
                {
                    player.SendErr($"You don't have access to kit {kit.name}");
                    return;
                }
                player.SendOk($"Giving kit {kit.name}");
                kit.items.ForEach(
                    item => player.GiveItemStack(item.type, item.code, item.amount)
                );
                KEssentialsKits.KitCooldownManagerInstance.SetCooldown(player.PlayerUID, kitName, kit.delay);
            }
            else player.SendErr( $"{kitName} not found");
        }
        
        public static void GiveFirstJoinKits(IServerPlayer player)
        {
            
            var firstJoinKits = KEssentialsKits
                .LoadedKitsConfig
                .kits
                .Where(kit => kit.giveOnFirstJoin)
                .Select(kit => kit.name)
                .ToList();
            
            firstJoinKits.ForEach(kitName => GiveKit(player, kitName));
        }
    }
}