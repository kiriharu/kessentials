using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace KEssentialsTeleports.Warps
{
    public class WarpsConfig
    {
        
        public List<Warp> warps = new List<Warp>();
        
    }

    public class Warp
    {
        public string name;
        public string owner;
        public double x;
        public double y;
        public double z;
    }
}