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
            Container.BindInstance(_carSettings);

            Container.BindInterfacesAndSelfTo<CarMover>().AsSingle();
            
            Container.Bind<CarController>().AsSingle().NonLazy();
        }
    }
}