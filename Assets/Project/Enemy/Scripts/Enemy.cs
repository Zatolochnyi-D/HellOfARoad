using HoaR.HealthSystem.DamageDealing;
using HoaR.HealthSystem.HealthComponent;
using UnityEngine;

namespace HoaR.Enemies
{
    public class Enemy
    {
        private readonly GameObject _self;
        private readonly EnemyStateManager _stateManager;

        public Enemy(GameObject self, Health health, DamageDealerMb damageDealer, EnemyStateManager stateManager)
        {
            _self = self;
            _stateManager = stateManager;

            health.OnHealthDepleted += Die;
            damageDealer.OnHitTarget += Die;
        }

        private void Die()
        {
            _stateManager.SwitchState(EnemyState.Dead);
            Object.Destroy(_self);
        }
    }
}