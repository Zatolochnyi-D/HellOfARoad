using System;
using System.Threading;
using DenZ.DevelopmentTools.Utilities;
using HoaR.HealthSystem.DamageDealing;
using UnityEngine;
using Zenject;

namespace HoaR.Turret.Shooting
{
    public class Bullet : IPoolable<Transform, IMemoryPool>, IDisposable
    {
        private readonly BulletSettings _settings;
        private readonly TrailRenderer _trailRenderer;
        private readonly BulletPositionHandler _positionHandler;
        private readonly BulletVisibilityHandler _visibilityHandler;
        private readonly IDamageDealer _damageDealer;
        private readonly SignalBus _signalBus;

        private IMemoryPool _parentPool;
        private CancellationTokenSource _timeOutDespawnCancellation;

        public Bullet(BulletSettings settings,
                      TrailRenderer trailRenderer,
                      BulletPositionHandler positionHandler,
                      BulletVisibilityHandler visibilityHandler,
                      IDamageDealer damageDealer,
                      SignalBus signalBus)
        {
            _settings = settings;
            _trailRenderer = trailRenderer;
            _positionHandler = positionHandler;
            _visibilityHandler = visibilityHandler;
            _damageDealer = damageDealer;
            _signalBus = signalBus;

            _damageDealer.OnHitTarget += Despawn;
            _damageDealer.OnKill += FireKillSignal;
        }

        private void Despawn()
        {
            _parentPool.Despawn(this);
        }

        private void FireKillSignal()
        {
            _signalBus.TryFire<KillSignal>();
        }

        public void OnSpawned(Transform spawnPosition, IMemoryPool pool)
        {
            _parentPool = pool;
            _positionHandler.Spawn(spawnPosition);
            _visibilityHandler.Show();
            _trailRenderer.Clear();

            _positionHandler.StartFly();
            _damageDealer.Activate();

            _timeOutDespawnCancellation = new();
            _ = Timers.InvokeOnce(() => _parentPool.Despawn(this), _settings.TimeBeforeDespawn, _timeOutDespawnCancellation.Token);
        }

        public void OnDespawned()
        {
            _timeOutDespawnCancellation.Cancel();
            _timeOutDespawnCancellation = null;
            _positionHandler.StopFly();
            _visibilityHandler.Hide();
            _damageDealer.Deactivate();
        }

        public void Dispose()
        {
            _damageDealer.OnHitTarget += Despawn;
            _damageDealer.OnKill += FireKillSignal;
        }
    }
}