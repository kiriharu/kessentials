using KEseentialsAutomessage;
using Kvsl.Extensions;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace KEssentials
{
    public class KEssentials : ModSystem
    {
        public override bool ShouldLoad(EnumAppSide side)
        {
            return side == EnumAppSide.Server;
        }
        
        public override void StartServerSide(ICoreServerAPI api)
        {
            base.StartServerSide(api);
            
            // Load main config
            var conf = api.LoadOrCreateConf<Config>("kessentials.json");
            
            // TODO: Сначала смотреть, есть ли вообще либа. Если её нет - просто писать варнинг, что её нет
            // KEssentials Automessage
            if (conf.EnableAutomessage)
            {
                api.ModLoader.GetModSystem<KEssentialsAutomessage>();
            }
        }
    }
}
