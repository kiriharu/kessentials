using System.Collections.Generic;

namespace KEseentialsAutomessage
{
    public class Config
    {
        public int Timer = 30;
        public string Prefix = "[Info]";
        public List<string> Messages = new List<string>(new string[]
        {
            "first message!", 
            "second message!", 
            "third message!"
        });
        public int MinPlayersCountToPost = 1;
    }
}