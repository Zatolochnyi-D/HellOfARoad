using HoaR.HealthSystem.DamageDealing;
using HoaR.HealthSystem.HealthComponent;
using UnityEngine;
using Zenject;

namespace HoaR.Car
{
    public class CarInstaller : MonoInstaller
    {
        [SerializeField] private CarSettings _carSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(transform);
            Container.BindInterfacesAndSelfTo<CarSettings>().FromInstance(_carSettings);
            Container.Bind<IDamageDealerSettings>().To<ScoopDamageDealerSettings>().AsSingle();
            Container.Bind<Animator>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<CarMover>().AsSingle();
            Container.BindInterfacesAndSelfTo<Health>().AsSingle();
            Container.Bind<CarHealthListener>().AsSingle().NonLazy();
            Container.Bind<CarAnimator>().AsSingle().NonLazy();

            Container.Bind<CarController>().AsSingle().NonLazy();
        }

        public class ScoopDamageDealerSettings : IDamageDealerSettings
        {
            public bool IsRelative => true;
            public int Damage => default;
            public float RelativeDamage => 1f;
            public LayerMask TriggerOn => LayerMask.NameToLayer("Enemy");
        }
    }
}