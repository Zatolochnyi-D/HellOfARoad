using Zenject;

namespace HoaR.Turret
{
    public class TurretInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInstance(transform);

            Container.Bind<TurretShooter>().AsSingle().NonLazy();
            Container.Bind<TurretMover>().AsSingle().NonLazy();
        }
    }
}