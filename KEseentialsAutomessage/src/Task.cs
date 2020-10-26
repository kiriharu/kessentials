using System.Collections.Generic;
using Kvsl.Extensions;
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
        private string _automessageFilename = "automessage.json";
        
        public Task(ICoreServerAPI api) : base(api)
        {
            _api = api;
            var conf = _api.LoadOrCreateConf<Config>(_automessageFilename);
            if (conf.messages.Count == 0)
            {
                api.Logger.Warning($"Oh, i don't see any messages in automessages file. Generating default...");
                var defaultMessages = new List<string>{
                    "First message! Set messages in automessage.json!", 
                    "Second message! Plugin repository <a href='http://github.com/kiriharu/kessentials'>is here</a>", 
                    "Third message! I can use <font size='5' " +
                    "color='green'" +
                    " weight='bold'" + ">VTML</font>"
                };
                conf.messages = defaultMessages;
                api.StoreModConfig(conf, _automessageFilename);
            }
            else _messages = conf.messages;
            _prefix = conf.prefix;
            Timer = conf.timer;
            _minPlayersCountToPost = conf.minPlayersCountToPost;
        }

        public override void Run()
        {
            // TODO: Modify messages by placeholders like {PlayersOnlineCount}
            if (_api.World.AllOnlinePlayers.Length < _minPlayersCountToPost) return;
            var message = _messages.GetRandomItem();
            _api.BroadcastMessageToAllGroups($"{_prefix}{message}", EnumChatType.AllGroups);
        }
    }
}