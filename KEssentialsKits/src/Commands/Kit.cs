using KEssentialsKits.Api;
using Kvsl.Extensions;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace KEssentialsKits.Commands
{
    public class Kit : ServerChatCommand
    {
        private readonly ICoreServerAPI _api;
        private IKits _kits;

        public Kit(ICoreServerAPI api, IKits kitsInstance)
        {
            _api = api;
            _kits = kitsInstance;
            Command = "kit";
            Description = "Gives you a kit.";
            Syntax = "/kit <kitname>";
            RequiredPrivilege = Privilege.kit;
        }
        
        public override void CallHandler(IPlayer player, int groupId, CmdArgs args)
        {
            var serverPlayer = (IServerPlayer) player;
            if (args.Length == 0 || args.Length > 1)
            {
                serverPlayer.SendMsg(GetHelpMessage());
                return;
            }
            var kitName = args[0];
            _kits.GiveKit(serverPlayer, kitName);
        }
    }
}