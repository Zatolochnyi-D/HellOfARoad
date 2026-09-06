using HoaR.InputManagement;
using Zenject;

namespace HoaR
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.Bind<InputManager>().AsSingle();
        }
    }
}