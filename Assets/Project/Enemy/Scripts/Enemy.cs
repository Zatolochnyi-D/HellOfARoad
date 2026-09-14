using System;
using System.Threading;
using DenZ.DevelopmentTools.Utilities;
using HoaR.HealthSystem.DamageDealing;
using HoaR.HealthSystem.HealthComponent;
using HoaR.Utilities;
using UnityEngine;

namespace HoaR.Enemies
{
    public class Enemy : IDisposable
    {
        private readonly GameObject _self;
        private readonly EnemyStateManager _stateManager;
        private readonly Health _health;
        private readonly IDamageDealer _damageDealer;
        private readonly CancellationToken _destroyCancellationToken;
        private readonly ObjectsToDisable _objectsToDisable;
        private readonly EnemySettings _settings;

        public Enemy(GameObject self,
                     Health health,
                     IDamageDealer damageDealer,
                     EnemyStateManager stateManager,
                     CancellationToken destroyCancellationToken,
                     ObjectsToDisable objectsToDisable,
                     EnemySettings settings)
        {
            _self = self;
            _stateManager = stateManager;
            _health = health;
            _damageDealer = damageDealer;
            _destroyCancellationToken = destroyCancellationToken;
            _objectsToDisable = objectsToDisable;
            _settings = settings;

            _health.OnHealthDepleted += Die;
            _damageDealer.OnHitTarget += KillOnTouch;
        }

        private void KillOnTouch()
        {
            _health.ReceiveRelativeDamage(new() { RelativeDamage = 1f, HitForward = -_self.transform.forward });
        }

        private void Die()
        {
            _stateManager.SwitchState(EnemyState.Dead);
            _objectsToDisable.GameObjects.ForEach(x => x.SetActive(false));
            _ = Timers.InvokeOnce(() => UnityEngine.Object.Destroy(_self), _settings.TimeBeforeRemovingEnemyOnDeath, _destroyCancellationToken);
        }

        public void Dispose()
        {
            _health.OnHealthDepleted -= Die;
            _damageDealer.OnHitTarget -= KillOnTouch;
        }
    }
}