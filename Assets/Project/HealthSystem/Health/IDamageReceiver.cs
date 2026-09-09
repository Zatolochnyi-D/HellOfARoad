namespace HoaR.HealthSystem.HealthComponent
{
    public interface IDamageReceiver
    {
        public void ReceiveAbsoluteDamage(int damage);

        public void ReceiveRelativeDamage(float damage);
    }
}