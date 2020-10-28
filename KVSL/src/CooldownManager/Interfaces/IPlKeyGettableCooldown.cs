namespace Kvsl.CooldownManager.Interfaces
{
    public interface IPlKeyGettableCooldown
    {
        int GetCooldown(string playerUid, string key);
    }
}