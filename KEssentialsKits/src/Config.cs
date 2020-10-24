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
        public List<Kit> kits = new List<Kit>(new []
        {
            new Kit("start", new List<Item>(new []
            {
               new Item(ItemType.item, "game:gear-temporal", 1),
               new Item(ItemType.item,"game:vegetable-carrot", 10), 
               new Item(ItemType.item,"game:pickaxe-iron", 1),
               new Item(ItemType.block,"game:soil-medium-none", 20), 
            }), 30, true)
        });
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