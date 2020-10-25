using Kvsl.Utils;
using Vintagestory.API.Server;

namespace KEssentialsKits.Cooldown
{
    public class CooldownSaveTimer : AbstractTimerEvent
    {

        private readonly ICoreServerAPI _coreServerApi;

        public CooldownSaveTimer(ICoreServerAPI api) : base(api)
        {
            _coreServerApi = api;
            Timer = 3600;
        }

        public override void Run()
        {
            KEssentialsKits.KitCooldownManagerInstance.SaveToConfig(_coreServerApi);
        }
    }
}