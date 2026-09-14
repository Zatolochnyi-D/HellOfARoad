namespace HoaR.HealthSystem.Damage
{
    public interface IDamageReceiver
    {
        public bool ReceiveAbsoluteDamage(DamageInfo damageInfo);

        public bool ReceiveRelativeDamage(DamageInfo damageInfo);
    }
}