using System.Collections.Generic;
using Kvsl.Utils;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace KEseentialsAutomessage
{
    public class Task : AbstractTimerEvent
    {

        private List<string> _messages;
        private readonly string _prefix;
        private readonly int _minPlayersCountToPost;
        private readonly ICoreServerAPI _api;
        
        public Task(ICoreServerAPI api) : base(api)
        {
            _api = api;
            var conf = _api.LoadOrCreateConf<Config>("automessage.json");
            if(conf.Messages.Count == 0) _messages.Add("Configure your automessage.json!");
            else _messages = conf.Messages;
            _prefix = conf.Prefix;
            Timer = conf.Timer;
            _minPlayersCountToPost = conf.MinPlayersCountToPost;
        }

        public override void Run()
        {
            if (_minPlayersCountToPost < _api.World.AllOnlinePlayers.Length) return;
            var message = _messages.GetRandomItem();
            _api.BroadcastMessageToAllGroups($"{_prefix}{message}", EnumChatType.AllGroups);
        }
    }
}