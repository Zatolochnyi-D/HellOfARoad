using Zenject;

namespace HoaR.Enemies
{
    public class EnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {   
            Container.Bind<Enemy>().AsSingle();
        }
    }
}