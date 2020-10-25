using Vintagestory.API.Server;

namespace Kvsl.CooldownManager
{
    public interface ISaveableCooldownManager : ICooldownManager
    {
        void Save(ICoreServerAPI serverApi);
    }
}