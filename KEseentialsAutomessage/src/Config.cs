using System.Collections.Generic;
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable InconsistentNaming
// ReSharper disable ConvertToConstant.Global

namespace KEseentialsAutomessage
{
    public class Config
    {
        public int timer = 30;
        public string prefix = "[Info]: ";
        public List<string> messages = new List<string>(new string[]
        // TODO: vtml builder?
        {
            "First message! Set messages in automessage.json!", 
            "Second message! Plugin repository <a href='http://github.com/kiriharu/kessentials'>is here</a>", 
            "Third message! I can use <font size='5' " +
            "color='green'" +
            " weight='bold'" + ">VTML</font>"
        });
        public int minPlayersCountToPost = 1;
    }
}