using Vintagestory.API.Common;

namespace Kvsl.Utils
{
    public static class ItemStackExtensions
    {
        public static ItemStack GetItemStack(this IWorldAccessor worldAccessor,
            string itemType, string itemCode, int amount)
        {
            switch (itemType)
                {
                    case "block":
                        return new ItemStack(worldAccessor.GetBlock(new AssetLocation(itemCode)), amount);
                    case "item":
                        return new ItemStack(worldAccessor.GetItem(new AssetLocation(itemCode)), amount);
                    default:
                        // TODO: null or empty itemstack?
                        return new ItemStack();
                }
        }
        
        public static bool IsEmpty(this ItemStack itemStack)
        {
            return itemStack.Block == null & itemStack.Item == null;
        } 
    }
}