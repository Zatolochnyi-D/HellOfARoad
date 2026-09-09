namespace HoaR.HealthSystem.DamageDealing
{
    public interface IDamageDealerSettings
    {
        public bool IsRelative { get; }
        public int Damage { get; }
        public float RelativeDamage { get; }
    }
}