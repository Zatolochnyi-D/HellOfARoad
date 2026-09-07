using HoaR.Turret.Shooting;
using UnityEngine;
using Zenject;

namespace HoaR.Turret
{
    public class TurretInstaller : MonoInstaller
    {
        [SerializeField] private TurretMoverSettings _turretMoverSettings;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPosition;
        [SerializeField] private TurretShooterSettings _turretShooterSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(transform);
            Container.BindInstance(_turretMoverSettings);
            Container.BindInstance<BulletSpawnPosition>(new(_bulletSpawnPosition));
            Container.BindInstance(_turretShooterSettings);

            Container.BindFactory<Transform, Bullet, BulletFactory>()
                     .FromPoolableMemoryPool(x => x.WithInitialSize(15)
                                                   .WithMaxSize(100)
                                                   .ExpandByOneAtATime()
                                                   .FromSubContainerResolve()
                                                   .ByNewContextPrefab(_bulletPrefab));

            Container.Bind<TurretShooter>().AsSingle().NonLazy();
            Container.Bind<TurretMover>().AsSingle().NonLazy();
        }
    }
}