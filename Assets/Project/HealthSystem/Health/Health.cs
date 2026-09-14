using System;
using HoaR.HealthSystem.Damage;
using UnityEngine;

namespace HoaR.HealthSystem.HealthComponent
{
    public class Health : IDamageReceiver
    {
        public event Action OnHealthDepleted;
        public event Action<DamageReceivedInfo> OnDamageReceived;

        private readonly IHealthSettingsProvider _settings;

        private int _currentHealth;

        public float NormalizedHealthPoints => (float)_currentHealth / _settings.MaxHealthPoints;

        public Health(IHealthSettingsProvider settings)
        {
            _settings = settings;

            _currentHealth = _settings.MaxHealthPoints;
        }

        public bool ReceiveAbsoluteDamage(DamageInfo damageInfo)
        {
            var isKill = false;
            _currentHealth -= damageInfo.AbsoluteDamage;
            if (_currentHealth <= 0)
            {
                OnHealthDepleted?.Invoke();
                isKill = true;
            }
            OnDamageReceived?.Invoke(new() { NormalizedHealthLeft = NormalizedHealthPoints, HitForward = damageInfo.HitForward });
            return isKill;
        }

        public bool ReceiveRelativeDamage(DamageInfo damageInfo)
        {
            var actualDamage = Mathf.CeilToInt(_settings.MaxHealthPoints * damageInfo.RelativeDamage);
            damageInfo.AbsoluteDamage = actualDamage;
            return ReceiveAbsoluteDamage(damageInfo);
        }
    }
}