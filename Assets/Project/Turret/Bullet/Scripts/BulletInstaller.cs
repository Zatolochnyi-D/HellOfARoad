using Zenject;

namespace HoaR.Turret.Shooting
{
    public class BulletInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInstance(transform);

            Container.Bind<Bullet>().AsSingle();
        }
    }
}