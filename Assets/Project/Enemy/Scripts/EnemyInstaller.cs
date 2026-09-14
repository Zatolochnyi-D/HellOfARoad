using HoaR.HealthSystem.DamageDealing;
using HoaR.HealthSystem.HealthComponent;
using HoaR.Utilities;
using UnityEngine;
using Zenject;

namespace HoaR.Enemies
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private EnemySettings _enemySettings;
        [SerializeField] private ObjectsToDisable _objectsToDisable;

        public override void InstallBindings()
        {
            Container.BindInstance(gameObject);
            Container.BindInstance(transform);
            Container.BindInstance(destroyCancellationToken);
            Container.BindInstance(_objectsToDisable);
            
            Container.BindInterfacesAndSelfTo<EnemySettings>().FromInstance(_enemySettings);
            Container.Bind<Animator>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<Health>().AsSingle().NonLazy();
            Container.Bind<IDamageDealer>().To<DamageDealerMb>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EnemyStateManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyAngeringHandler>().AsSingle().NonLazy();
            Container.Bind<EnemyAnimator>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyMover>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyGameStateHandler>().AsSingle().NonLazy();
            Container.Bind<Enemy>().AsSingle().NonLazy();
        }
    }
}