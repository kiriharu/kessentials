namespace Kvsl.CooldownManager.Interfaces
{
    public interface IPlGettableCooldown
    {
        int GetCooldown(string playerUid);
    }
}