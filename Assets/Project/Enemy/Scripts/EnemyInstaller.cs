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
            
            Container.Bind<IHealthSettingsProvider>().FromInstance(_enemySettings);

            Container.BindInterfacesAndSelfTo<Health>().AsSingle().NonLazy();
            Container.Bind<EnemyStateManager>().AsSingle();
            Container.Bind<Enemy>().AsSingle();
        }
    }
}