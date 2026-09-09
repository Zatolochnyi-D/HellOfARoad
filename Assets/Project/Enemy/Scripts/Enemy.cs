using HoaR.HealthSystem.DamageDealing;
using HoaR.HealthSystem.HealthComponent;
using UnityEngine;

namespace HoaR.Enemies
{
    public class Enemy
    {
        private readonly GameObject _self;

        public Enemy(GameObject self, Health health, DamageDealerMb damageDealer)
        {
            _self = self;
            health.OnHealthDepleted += () => Object.Destroy(_self);
            damageDealer.OnHitTarget += () => Object.Destroy(_self);
        }
    }
}