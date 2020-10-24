using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace KEssentialsKits
{
    public class Cooldowns
    {
        /*public List<User> usageCooldowns = new List<User>(new[]
        {
            new User("1234567891", new List<Timer>(new []
            {
             new Timer("start", 12345),    
            })),
        });*/
        public List<User> usageCooldowns;
    }

    public class User
    {
        public string playerUID;
        public List<KitTimer> timers;

        public User(string PlayerUID, List<KitTimer> Timers)
        {
            playerUID = PlayerUID;
            timers = Timers;
        }
    }

    public class KitTimer
    {
        public string kit;
        public int endTimestamp;
        
        public KitTimer(string Kit, int EndTimestamp)
        {
            kit = Kit;
            endTimestamp = EndTimestamp;
        }
    }
}