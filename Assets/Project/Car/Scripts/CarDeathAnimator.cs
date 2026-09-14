using System;
using System.Collections.Generic;
using DenZ.DevelopmentTools.Extensions;
using HoaR.HealthSystem.HealthComponent;
using UnityEngine;

namespace HoaR.Car
{
    public class CarDeathAnimator : IDisposable
    {
        private readonly Health _health;
        private readonly IEnumerable<ParticleSystem> _explosionParticles;
        private readonly ObjectsToDisable _objectsToDisable;

        public CarDeathAnimator(Health health, IEnumerable<ParticleSystem> explosionParticles, ObjectsToDisable objectsToDisable)
        {
            _health = health;
            _explosionParticles = explosionParticles;
            _objectsToDisable = objectsToDisable;

            _health.OnHealthDepleted += HandleDeath;
        }
        
        private void HandleDeath()
        {
            _objectsToDisable.GameObjects.ForEach(x => x.SetActive(false));
            _explosionParticles.ForEach(x =>
            {
                x.transform.parent = null;
                x.Play();
            });
        }

        public void Dispose()
        {
            _health.OnHealthDepleted -= HandleDeath;
        }
    }
}
