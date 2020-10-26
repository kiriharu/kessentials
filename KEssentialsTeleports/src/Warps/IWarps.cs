using System.Collections.Generic;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace KEssentialsTeleports.Warps
{
    public interface IWarps
    {
        
        // /warp <warp>
        Vec3d GetWarp(string warp);
        
        // /warps set <name>
        void SetWarp(string warp, Vec3d location);

        // /warps set <name>
        void SetWarp(string warp, IServerPlayer user);
        
        // /warps remove <name>
        void RemoveWarp(string warp);

        // /warps my
        // /warps user <nick>
        List<Warp> GetUserWarps(IServerPlayer user);
        
        // /warps or /warps list
        /// <summary>
        /// Warps list
        /// </summary>
        /// <returns>list of warps</returns>
        List<string> GetList();

    }
}