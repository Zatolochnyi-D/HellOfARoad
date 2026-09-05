using DenZ.DevelopmentTools.Utilities;
using Zenject;

namespace HoaR.Game.GameStateManagement
{
    public class GameStateManager
    {
        private readonly SignalBus _signalBus;

        private GameState _gameState = GameState.PreGame;

        public GameStateManager(SignalBus signalBus)
        {
            _signalBus = signalBus;

            _ = Timers.InvokeOnce(() => ChangeState(GameState.Playing), 2f);
        }

        private void ChangeState(GameState newState)
        {
            _gameState = newState;
            _signalBus.Fire<GameStateChangeSignal>(new() { NewState = _gameState });
        }
    }
}