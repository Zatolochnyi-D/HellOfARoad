using System.Threading;
using HoaR.Turret.Shooting;
using DenZ.DevelopmentTools.Di;
using DenZ.DevelopmentTools.Utilities;
using UnityEngine;
using System;

namespace HoaR.Turret
{
    public class BulletSpawnPosition : TypeWrapper<Transform> { public BulletSpawnPosition(Transform value) : base(value) { } }

    public class TurretShooter : IDisposable
    {
        private readonly BulletFactory _bulletFactory;
        private readonly Transform _bulletSpawnPosition;
        private readonly TurretShooterSettings _settings;
        private readonly IPointerDownUpProvider _pointerDownUpProvider;

        private CancellationTokenSource _bulletSpawnCancellation;

        public TurretShooter(IPointerDownUpProvider pointerDownUpProvider,
                             BulletFactory bulletFactory,
                             BulletSpawnPosition bulletSpawnPosition,
                             TurretShooterSettings settings)
        {
            _bulletFactory = bulletFactory;
            _bulletSpawnPosition = bulletSpawnPosition.Value;
            _settings = settings;
            _pointerDownUpProvider = pointerDownUpProvider;
            
            _pointerDownUpProvider.OnDown += HandlePointerDown;
            _pointerDownUpProvider.OnUp += HandlePointerUp;
        }

        private void HandlePointerDown()
        {
            _bulletSpawnCancellation = new();
            _ = Timers.InvokeIndefinitely(() => _bulletFactory.Create(_bulletSpawnPosition), _settings.TimeToShoot, _bulletSpawnCancellation.Token, true);
        }
        
        private void HandlePointerUp()
        {
            _bulletSpawnCancellation.Cancel();
        }

        public void Dispose()
        {
            _pointerDownUpProvider.OnDown -= HandlePointerDown;
            _pointerDownUpProvider.OnUp -= HandlePointerUp;
        }
    }
}