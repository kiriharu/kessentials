using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace KEssentialsTeleports
{
    public class KEssentialsTeleports : ModSystem
    {
        
        public static string WarpsConfigName = "warps.json";

        public override bool ShouldLoad(EnumAppSide forSide)
        {
            return forSide == EnumAppSide.Server;
        }

        public override void StartServerSide(ICoreServerAPI api)
        {
            base.StartServerSide(api);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}