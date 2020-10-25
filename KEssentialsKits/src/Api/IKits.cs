using System.Collections.Generic;
using Kvsl.CooldownManager;
using Vintagestory.API.Server;

namespace KEssentialsKits.Api
{
    public interface IKits
    {
        
        /// <returns>default kit</returns>
        Kit GetDefaultKit();
        
        /// <summary>
        /// Returns a cooldown manager
        /// </summary>
        /// <returns></returns>
        ICooldownManager GetCooldownManager();

        KitsConfig GetLoadedKits();
        
        /// <summary>
        /// Gives kit to player
        /// </summary>
        /// <param name="player">player instance</param>
        /// <param name="kitName">kit name</param>
        void GiveKit(IServerPlayer player, string kitName);
        
        /// <summary>
        /// Returns a string list of accessed kits
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        List<string> GetAccessedKits(IServerPlayer player);

        /// <summary>
        /// Gives player all kits, that have firstJoin: true param
        /// </summary>
        /// <param name="player"></param>
        void GiveFirstJoinKits(IServerPlayer player);

    }
}