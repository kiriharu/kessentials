using Kvsl.Utils;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace KEseentialsAutomessage
{
    public class KEssentialsAutomessage : ModSystem
    {
        public override bool ShouldLoad(EnumAppSide side)
        {
            return side == EnumAppSide.Server;
        }
        
        public override void StartServerSide(ICoreServerAPI api)
        {
            base.StartServerSide(api);
            api.RegisterKvslTimer(typeof(Task));
            api.Server.LogEvent($"Loaded KEssentials - AutoMessage");
        }
    }
}