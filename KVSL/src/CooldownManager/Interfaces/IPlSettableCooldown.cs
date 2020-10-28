namespace Kvsl.CooldownManager.Interfaces
{
    public interface IPlSettableCooldown
    {
        void SetCooldown(string playerUid, int cooldown);
    }
}