using System.Threading;
using DenZ.DevelopmentTools.Utilities;
using UnityEngine;
using Zenject;

namespace HoaR.Turret.Shooting
{
    public class Bullet : IPoolable<Transform, IMemoryPool>
    {
        private readonly BulletSettings _settings;
        private readonly TrailRenderer _trailRenderer;
        private readonly BulletPositionHandler _positionHandler;
        private readonly BulletVisibilityHandler _visibilityHandler;
        private readonly BulletDamageDealer _damageDealer;

        private IMemoryPool _parentPool;
        private CancellationTokenSource _timeOutDespawnCancellation;

        public Bullet(BulletSettings settings,
                      TrailRenderer trailRenderer,
                      BulletPositionHandler positionHandler,
                      BulletVisibilityHandler visibilityHandler,
                      BulletDamageDealer damageDealer)
        {
            _settings = settings;
            _trailRenderer = trailRenderer;
            _positionHandler = positionHandler;
            _visibilityHandler = visibilityHandler;
            _damageDealer = damageDealer;

            _damageDealer.OnBulletHitTarget += () => _parentPool.Despawn(this);
        }

        public void OnSpawned(Transform spawnPosition, IMemoryPool pool)
        {
            _parentPool = pool;
            _positionHandler.Spawn(spawnPosition);
            _visibilityHandler.Show();
            _trailRenderer.Clear();

            _positionHandler.StartFly();

            _timeOutDespawnCancellation = new();
            _ = Timers.InvokeOnce(() => _parentPool.Despawn(this), _settings.TimeBeforeDespawn, _timeOutDespawnCancellation.Token);
        }

        public void OnDespawned()
        {
            _timeOutDespawnCancellation.Cancel();
            _timeOutDespawnCancellation = null;
            _positionHandler.StopFly();
            _visibilityHandler.Hide();
        }
    }
}