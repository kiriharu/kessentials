namespace Kvsl.CooldownManager.Interfaces
{
    public interface IPlKeySettableCoolodown
    {
        void SetCooldown(string playerUid, string key, int cooldown);
    }
}