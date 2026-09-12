using System;
using HoaR.HealthSystem.DamageDealing;
using HoaR.HealthSystem.HealthComponent;
using UnityEngine;

namespace HoaR.Enemies
{
    public class Enemy : IDisposable
    {
        private readonly GameObject _self;
        private readonly EnemyStateManager _stateManager;
        private readonly Health _health;
        private readonly IDamageDealer _damageDealer;

        public Enemy(GameObject self, Health health, IDamageDealer damageDealer, EnemyStateManager stateManager)
        {
            _self = self;
            _stateManager = stateManager;
            _health = health;
            _damageDealer = damageDealer;

            _health.OnHealthDepleted += Die;
            _damageDealer.OnHitTarget += Die;
        }

        private void Die()
        {
            _stateManager.SwitchState(EnemyState.Dead);
            UnityEngine.Object.Destroy(_self);
        }

        public void Dispose()
        {
            _health.OnHealthDepleted -= Die;
            _damageDealer.OnHitTarget -= Die;
        }
    }
}