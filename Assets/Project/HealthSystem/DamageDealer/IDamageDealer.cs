using System;

namespace HoaR.HealthSystem.DamageDealing
{
    public interface IDamageDealer
    {
        public event Action OnHitTarget;
        public event Action OnKill;
    }
}