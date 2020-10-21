using System;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace Kvsl.Utils
{
    public static class APIExtensions
    {
        public static T LoadOrCreateConf<T>(this ICoreAPI serverApi, string filename) where T: new()
        {
            try
            {
                var loadedConf = serverApi.LoadModConfig<T>(filename);
                if (loadedConf != null) return loadedConf;
            }
            catch (Exception e)
            {
                serverApi.Logger.Error($"Failed to loading config {filename} with error {e}. Initialize new...");
            }
            var newConf = new T();
            serverApi.StoreModConfig(newConf, filename);
            serverApi.Logger.Warning($"Created new {filename} config.");
            return newConf;
        }

        public static void RegisterKvslTimer(this ICoreServerAPI serverApi, Type timerEvent)
        {
            AbstractTimerEvent instance = TypeUtils.CreateInstance<AbstractTimerEvent>(timerEvent, serverApi);
            serverApi.Event.Timer(instance.Run, instance.Timer);
            serverApi.Server.LogEvent($"Loaded {instance.GetType()} time task with time {instance.Timer}");
        }
    }
}