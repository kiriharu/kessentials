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
        public List<string> messages = new List<string>();
        // TODO: vtml builder
        public int minPlayersCountToPost = 1;
    }
}