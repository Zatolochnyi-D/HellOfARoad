using Zenject;

namespace HoaR.Car
{
    public class CarInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInstance(transform);

            Container.BindInterfacesAndSelfTo<CarMover>().AsSingle();
            
            Container.Bind<CarController>().AsSingle().NonLazy();
        }
    }
}