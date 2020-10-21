using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace Kvsl.Commands
{
    public abstract class AbstractCommand
    {
        public string Command;
        public string Syntax;
        public string Description;
        public string RequiredPrivilege;

        public abstract void Handler(IServerPlayer player, int groupId, CmdArgs args);
    }
}