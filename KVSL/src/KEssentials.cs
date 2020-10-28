using Kvsl.StorageOperator;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace Kvsl
{
    public class Kvsl : ModSystem
    {
        
        public override bool ShouldLoad(EnumAppSide side)
        {
            return side == EnumAppSide.Server;
        }
        
        public override void StartServerSide(ICoreServerAPI api)
        {
            base.StartServerSide(api);
        }
        
    }
}