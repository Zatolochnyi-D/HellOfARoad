using System;
using UnityEngine;

namespace HoaR.HealthSystem.HealthComponent
{
    public class Health : IDamageReceiver
    {
        public event Action OnHealthDepleted;
        public event Action<float> OnDamageReceived;

        private readonly IHealthSettingsProvider _settings;

        private int _currentHealth;

        public float NormalizedHealthPoints => (float)_currentHealth / _settings.MaxHealthPoints;

        public Health(IHealthSettingsProvider settings)
        {
            _settings = settings;

            _currentHealth = _settings.MaxHealthPoints;
        }

        public void ReceiveAbsoluteDamage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
                OnHealthDepleted?.Invoke();
            OnDamageReceived?.Invoke(NormalizedHealthPoints);
        }

        public void ReceiveRelativeDamage(float damage)
        {
            var actualDamage = Mathf.CeilToInt(_settings.MaxHealthPoints * damage);
            ReceiveAbsoluteDamage(actualDamage);
        }
    }
}