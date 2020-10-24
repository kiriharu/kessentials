using System;
using Kvsl.Utils;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace KEssentialsKits.Commands
{
    public class Kit : ServerChatCommand
    {
        private readonly ICoreServerAPI _api;

        public Kit(ICoreServerAPI api)
        {
            _api = api;
            Command = "kit";
            Description = "Gives you a kit.";
            Syntax = "/kit <kitname>";
            RequiredPrivilege = Privilege.kit;
        }
        
        public override void CallHandler(IPlayer player, int groupId, CmdArgs args)
        {
            // TODO: Mb create CallHandler with IServerPlayer?
            var serverPlayer = (IServerPlayer) player;
            if (args.Length == 0 || args.Length > 1)
            {
                serverPlayer.SendMsg(GetHelpMessage());
                return;
            }
            var kitName = args[0];
            serverPlayer.GiveKit(kitName);
        }
    }
}