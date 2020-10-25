using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace KEssentialsKits.Cooldown
{
    public class Cooldowns
    {
        public List<User> usageCooldowns = new List<User>();
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