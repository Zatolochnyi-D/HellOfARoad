using System.Threading;
using HoaR.Turret.Shooting;
using DenZ.DevelopmentTools.Di;
using DenZ.DevelopmentTools.Utilities;
using UnityEngine;

namespace HoaR.Turret
{
    public class BulletSpawnPosition : TypeWrapper<Transform> { public BulletSpawnPosition(Transform value) : base(value) { } }

    public class TurretShooter
    {
        private readonly BulletFactory _bulletFactory;
        private readonly Transform _bulletSpawnPosition;

        private CancellationTokenSource _bulletSpawnCancellation;

        public TurretShooter(IPointerDownUpProvider pointerDownUpProvider, BulletFactory bulletFactory, BulletSpawnPosition bulletSpawnPosition)
        {
            _bulletFactory = bulletFactory;
            _bulletSpawnPosition = bulletSpawnPosition.Value;
            
            pointerDownUpProvider.OnDown += HandlePointerDown;
            pointerDownUpProvider.OnUp += HandlePointerUp;
        }

        private void HandlePointerDown()
        {
            _bulletSpawnCancellation = new();
            _ = Timers.InvokeIndefinitely(() => _bulletFactory.Create(_bulletSpawnPosition), 1f, _bulletSpawnCancellation.Token, true);
        }
        
        private void HandlePointerUp()
        {
            _bulletSpawnCancellation.Cancel();
        }
    }
}