using System;
using HoaR.HealthSystem.Damage;
using HoaR.HealthSystem.HealthComponent;
using UnityEngine;

namespace HoaR.Enemies
{
    public class EnemyHitHandler : IDisposable
    {
        private readonly Health _health;
        private readonly ParticleSystem _bloodParticles;

        public EnemyHitHandler(Health health, ParticleSystem bloodParticles)
        {
            _health = health;
            _bloodParticles = bloodParticles;

            _health.OnDamageReceived += HandleHit;
        }

        private void HandleHit(DamageReceivedInfo info)
        {
            _bloodParticles.transform.forward = info.HitForward;
            _bloodParticles.Play();
        }

        public void Dispose()
        {
            _health.OnDamageReceived -= HandleHit;
        }
    }
}