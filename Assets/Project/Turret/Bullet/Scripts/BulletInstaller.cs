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

            Container.BindInterfacesAndSelfTo<Bullet>().AsSingle();
        }
    }
}