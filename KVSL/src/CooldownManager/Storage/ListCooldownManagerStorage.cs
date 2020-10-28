using Kvsl.StorageOperator;
using Vintagestory.API.Server;

namespace Kvsl.CooldownManager.Storage
{
    public abstract class ListCooldownManagerStorage<T> where T: new()
    {
        
        public ListCooldownManager CooldownManager = new ListCooldownManager();
        public BaseStorage<T> Storage;
        protected readonly ICoreServerAPI ServerApi;
        protected readonly string ConfigName;

        public ListCooldownManagerStorage(ICoreServerAPI api, string configName)
        {
            ServerApi = api;
            ConfigName = configName;
            Storage = new BaseStorage<T>(api, configName);
        }

        public abstract void Save();
        public abstract void Load();

    }
}