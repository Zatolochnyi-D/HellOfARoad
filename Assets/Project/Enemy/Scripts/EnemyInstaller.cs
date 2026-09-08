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
            Container.Bind<IHealthSettingsProvider>().FromInstance(_enemySettings);

            Container.Bind<Health>().AsSingle().NonLazy();
            Container.Bind<Enemy>().AsSingle();
        }
    }
}