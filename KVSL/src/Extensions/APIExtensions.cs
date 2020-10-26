using System;
using System.Linq;
using System.Reflection;
using Kvsl.Utils;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace Kvsl.Extensions
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
        
        /// <summary>
        /// You can define class with public static string fields and use this method to register
        /// all strings to permission server list. All permissions will be registered without description.
        /// </summary>
        /// <param name="serverApi"></param>
        /// <param name="privilegeClass"></param>
        public static void RegisterPrivilegeClass(this ICoreServerAPI serverApi, Type privilegeClass)
        {
            var permissions = privilegeClass
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.FieldType == typeof(string))
                .Select(f => f.GetValue(null))
                .Where(f => f != null)
                .ToList();
            foreach (var permField in permissions)
            {
                serverApi.Logger.Event($"Register {permField} permission");
                serverApi.Permissions.RegisterPrivilege(permField.ToString(), "", true);
            }
        }
    }
}