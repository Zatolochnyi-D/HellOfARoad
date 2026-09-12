using HoaR.HealthSystem.DamageDealing;
using HoaR.HealthSystem.HealthComponent;
using UnityEngine;
using Zenject;

namespace HoaR.Enemies
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private EnemySettings _enemySettings;

        public override void InstallBindings()
        {
            Container.BindInstance(gameObject);
            Container.BindInstance(transform);
            
            Container.BindInterfacesAndSelfTo<EnemySettings>().FromInstance(_enemySettings);

            Container.BindInterfacesAndSelfTo<Health>().AsSingle().NonLazy();
            Container.Bind<IDamageDealer>().To<DamageDealerMb>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EnemyStateManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyAngeringHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyMover>().AsSingle().NonLazy();
            Container.Bind<Enemy>().AsSingle().NonLazy();
        }
    }
}