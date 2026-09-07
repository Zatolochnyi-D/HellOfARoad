using System.Threading;
using DenZ.DevelopmentTools.Utilities;
using UnityEngine;
using Zenject;

namespace HoaR.Turret.Shooting
{
    public class Bullet : IPoolable<Transform, IMemoryPool>, IInitializable
    {
        private readonly Transform _bulletTransform;
        private readonly BulletSettings _settings;
        private readonly TrailRenderer _trailRenderer;

        private IMemoryPool _parentPool;
        private CancellationTokenSource _flyingCycleCancellation;

        public Bullet(Transform bulletTransform, BulletSettings settings, TrailRenderer trailRenderer)
        {
            _bulletTransform = bulletTransform;
            _settings = settings;
            _trailRenderer = trailRenderer;

            _bulletTransform.parent = null;
        }

        private async void FlyingCycle(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                _bulletTransform.position += _settings.FlyingSpeed * Time.deltaTime * _bulletTransform.forward;
                await Awaitable.NextFrameAsync();
            }
        }

        public void OnSpawned(Transform spawnPosition, IMemoryPool pool)
        {
            _parentPool = pool;
            _bulletTransform.position = spawnPosition.position;
            _bulletTransform.forward = spawnPosition.forward;
            _bulletTransform.gameObject.SetActive(true);
            _trailRenderer.Clear();

            _flyingCycleCancellation = new();
            FlyingCycle(_flyingCycleCancellation.Token);

            _ = Timers.InvokeOnce(() => _parentPool.Despawn(this), _settings.TimeBeforeDespawn, _flyingCycleCancellation.Token);
        }

        public void OnDespawned()
        {
            _flyingCycleCancellation.Cancel();
            _flyingCycleCancellation = null;
            _bulletTransform.gameObject.SetActive(false);
        }

        public void Initialize()
        {
            _bulletTransform.gameObject.SetActive(false);
        }
    }
}