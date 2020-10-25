using System;
using System.Linq;
using System.Text;
using Kvsl.Utils;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace KEssentialsKits.Commands
{
    public class Kits : ServerChatCommand
    {

        private ICoreServerAPI _api;
        
        public Kits(ICoreServerAPI api)
        {
            _api = api;
            Command = "kits";
            Description = "List kits.";
            Syntax = "/kits";
            RequiredPrivilege = Privilege.kits;
        }
        
        public override void CallHandler(IPlayer player, int groupId, CmdArgs args)
        {
            var serverPlayer = (IServerPlayer) player;
            var accessedKits = Utils.GetAccessedKits(serverPlayer);
            var playerCooldowns = KEssentialsKits
                .KitCooldownManagerInstance
                .GetCooldowns(player.PlayerUID);
            var finalString = new StringBuilder();
            finalString.Append("You have access to this kits:");
            foreach (var playerCooldown in playerCooldowns)
            {
                if (accessedKits.Contains(playerCooldown.Key))
                {
                    finalString.Append($" {playerCooldown.Key}({playerCooldown.Value} sec) ");
                    accessedKits.Remove(playerCooldown.Key);
                }
            }
            accessedKits.ForEach(kit => finalString.Append($" {kit} "));
            serverPlayer.SendMsg(finalString.ToString());
        }
    }
}