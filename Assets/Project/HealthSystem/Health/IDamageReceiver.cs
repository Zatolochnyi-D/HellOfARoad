namespace HoaR.HealthSystem.HealthComponent
{
    public interface IDamageReceiver
    {
        public bool ReceiveAbsoluteDamage(int damage);

        public bool ReceiveRelativeDamage(float damage);
    }
}