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

            Container.BindInterfacesAndSelfTo<CarMover>().AsSingle();
            Container.BindInterfacesAndSelfTo<Health>().AsSingle();
            
            Container.Bind<CarController>().AsSingle().NonLazy();
        }
    }
}