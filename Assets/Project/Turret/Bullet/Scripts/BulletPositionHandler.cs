using System.Threading;
using UnityEngine;

namespace HoaR.Turret.Shooting
{
    public class BulletPositionHandler
    {
        private readonly Transform _bulletTransform;
        private readonly BulletSettings _settings;

        private CancellationTokenSource _flyingCycleCancellation;

        public BulletPositionHandler(Transform bulletTransform, BulletSettings settings)
        {
            _bulletTransform = bulletTransform;
            _settings = settings;

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

        public void Spawn(Transform spawnPosition)
        {
            _bulletTransform.position = spawnPosition.position;
            _bulletTransform.forward = spawnPosition.forward;
        }

        public void StartFly()
        {
            _flyingCycleCancellation = new();
            FlyingCycle(_flyingCycleCancellation.Token);
        }

        public void StopFly()
        {
            _flyingCycleCancellation.Cancel();
            _flyingCycleCancellation = null;
        }
    }
}