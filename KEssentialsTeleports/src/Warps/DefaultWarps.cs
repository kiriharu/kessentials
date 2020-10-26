using System.Collections.Generic;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace KEssentialsTeleports.Warps
{
    public class DefaultWarps : IWarps
    {
        public Vec3d GetWarp(string warp)
        {
            throw new System.NotImplementedException();
        }

        public void SetWarp(string warp, Vec3d location)
        {
            throw new System.NotImplementedException();
        }

        public void SetWarp(string warp, IServerPlayer user)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveWarp(string warp)
        {
            throw new System.NotImplementedException();
        }

        public List<Warp> GetUserWarps(IServerPlayer user)
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetList()
        {
            throw new System.NotImplementedException();
        }
    }
}