using Zenject;

namespace HoaR.Turret
{
    public class TurretInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TurretShooter>().AsSingle().NonLazy();
        }
    }
}