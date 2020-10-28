using Kvsl.Extensions;
using Vintagestory.API.Server;

namespace Kvsl.StorageOperator
{
    public class BaseStorage<T> : IStorage where T: new()
    {
        private readonly ICoreServerAPI _serverApi;
        private readonly string _configStorage;
        public T StorageClass;

        public BaseStorage(ICoreServerAPI serverApi, string configName)
        {
            _configStorage = configName;
            _serverApi = serverApi;
            Load();
        }

        public void Load()
        {
            StorageClass = _serverApi.LoadOrCreateConf<T>(_configStorage);
        }

        public void Save()
        {
            _serverApi.StoreModConfig(StorageClass, _configStorage);
        }
    }
}