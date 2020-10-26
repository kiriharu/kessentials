using Vintagestory.API.Common;

namespace KEssentialsTeleports.Warps.Commands
{
    public class Warps : ServerChatCommand
    {

        public Warps()
        {
            Command = "warps";
            Description = "Warps main command";
            Syntax = "/warps <operation> <arg>";
            RequiredPrivilege = Privilege.warps;
        }
        public override void CallHandler(IPlayer player, int groupId, CmdArgs args)
        {
            
            
            
        }
    }
}