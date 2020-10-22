using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;

namespace Kvsl.Utils
{
    public static class ServerPlayerExtensions
    {

        public static void SendErr(this IServerPlayer serverPlayer, string message, string data = null)
        {
            serverPlayer.SendMessage(GlobalConstants.GeneralChatGroup, message, EnumChatType.CommandError, data);
        }

        public static void SendOk(this IServerPlayer serverPlayer, string message, string data = null)
        {
            serverPlayer.SendMessage(GlobalConstants.GeneralChatGroup, message, EnumChatType.CommandSuccess, data);
        }

        public static void SendMsg(this IServerPlayer serverPlayer, string message, string data = null)
        {
            serverPlayer.SendMessage(GlobalConstants.GeneralChatGroup, message, EnumChatType.OwnMessage, data);
        }
        
    }
}