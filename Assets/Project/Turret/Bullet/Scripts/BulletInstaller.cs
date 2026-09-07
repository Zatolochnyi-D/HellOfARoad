using UnityEngine;
using Zenject;

namespace HoaR.Turret.Shooting
{
    public class BulletInstaller : MonoInstaller
    {
        [SerializeField] private BulletSettings _settings;

        public override void InstallBindings()
        {
            Container.BindInstance(transform);
            Container.BindInstance(_settings);
            Container.Bind<TrailRenderer>().FromComponentOnRoot().AsSingle();
            Container.BindInstance(gameObject);

            Container.Bind<BulletPositionHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletVisibilityHandler>().AsSingle();

            Container.Bind<Bullet>().AsSingle();
        }
    }
}