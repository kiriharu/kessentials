using System.Collections.Generic;
using System.Linq;
using Kvsl.CooldownManager;
using Kvsl.Utils;
using Vintagestory.API.Server;

namespace KEssentialsKits.Api
{
    public class DefaultKits : IKits
    {
        public ICooldownManager GetCooldownManager()
        {
            return KEssentialsKits.KitCooldownManagerInstance;
        }

        public KitsConfig GetLoadedKits()
        {
            return KEssentialsKits.LoadedKitsConfig;
        }

        public Kit GetDefaultKit()
        {
            return new Kit("start", new List<Item>(new[]
            {
                new Item(ItemType.item, "game:gear-temporal", 1),
                new Item(ItemType.item, "game:bread-rice", 4),
                new Item(ItemType.item, "game:flint", 5),
                new Item(ItemType.item, "game:stick", 4),
                new Item(ItemType.block, "game:torch-up", 2)
                    
            }), 84600, true);
        }

        public void GiveKit(IServerPlayer player, string kitName)
        {
            var kit = GetLoadedKits().kits.Find(
                    x => x.name == kitName
                );
                if (kit != null)
                {
                    if (!player.HasPrivilege(Privilege.ignoreCooldowns))
                    {
                        var cooldown =
                            GetCooldownManager().GetCooldown(player.PlayerUID, kitName);
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
                    GetCooldownManager().SetCooldown(player.PlayerUID, kitName, kit.delay);
                }
                else player.SendErr($"{kitName} not found");
            }

        public List<string> GetAccessedKits(IServerPlayer player)
        {
            return GetLoadedKits()
                .kits
                .Where(kit => player.HasPrivilege($"{Privilege.kit}.{kit.name}"))
                .Select(kit => kit.name)
                .ToList();
        }

        public void GiveFirstJoinKits(IServerPlayer player)
        {
            var firstJoinKits = GetLoadedKits()
                .kits
                .Where(kit => kit.giveOnFirstJoin)
                .Select(kit => kit.name)
                .ToList();
            
            firstJoinKits.ForEach(kitName => GiveKit(player, kitName));
        }
        
    }
}