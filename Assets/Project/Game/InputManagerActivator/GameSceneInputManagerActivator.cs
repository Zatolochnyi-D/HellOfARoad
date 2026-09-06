using HoaR.Game.GameStateManagement;
using HoaR.InputManagement;

namespace HoaR.Game.InputManagerActivation
{
    public class GameSceneInputManagerActivator
    {
        public GameSceneInputManagerActivator(IGameStateManager<GameState> stateManager, InputManager inputManager)
        {
            stateManager.OnStateChanged += state => { if (state == GameState.Playing) inputManager.Activate(); };
        }
    }
}