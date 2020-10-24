using System.Collections.Generic;
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global

namespace KEssentialsKits
{
    
    public static class ItemType
    {
        public static string block = nameof(block);
        public static string item = nameof(item);
    }
    
    public class KitsConfig
    {

        public List<Kit> kits = new List<Kit>();
    }

    public class Kit
    {
        public string name;
        public List<Item> items;
        public int delay;
        public bool giveOnFirstJoin;

        public Kit(string Name, List<Item> Items, int Delay, bool GiveOnFirstJoin)
        {
            name = Name;
            items = Items;
            delay = Delay;
            giveOnFirstJoin = GiveOnFirstJoin;
        }
    }

    public class Item
    {
        /// <summary>
        /// You can use <see cref="ItemType"/> for types
        /// </summary>
        public string type;
        /// <summary>
        /// Item domain with item code, separated be : (like game:soil-medium-none)
        /// </summary>
        public string code;
        /// <summary>
        /// Amount of given items
        /// </summary>
        public int amount;

        public Item(string Type, string Code, int Amount)
        {
            type = Type;
            code = Code;
            amount = Amount;
        }
    }
}