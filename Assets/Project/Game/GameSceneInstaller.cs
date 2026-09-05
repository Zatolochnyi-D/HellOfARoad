using HoaR.Game.GameStateManagement;
using Zenject;

namespace HoaR.Game
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameStateManager<GameState>>().To<GameStateManager>().AsSingle();
        }
    }
}