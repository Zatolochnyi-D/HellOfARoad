using HoaR.HealthSystem;
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
            
            Container.BindInstance(_enemySettings);
            Container.Bind<IHealthSettingsProvider>().To<EnemySettings>().FromResolve();

            Container.BindInterfacesAndSelfTo<Health>().AsSingle().NonLazy();
            Container.Bind<EnemyStateManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyAngeringHandler>().AsSingle().NonLazy();
            Container.Bind<Enemy>().AsSingle();
        }
    }
}