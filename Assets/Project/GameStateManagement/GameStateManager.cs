using System;

namespace HoaR.Game.GameStateManagement
{
    public class GameStateManager : IGameStateManager<GameState>
    {
        public event Action<GameState> OnStateChanged;

        private GameState _gameState = GameState.PreGame;

        public void ChangeState(GameState newState)
        {
            if (_gameState != newState)
            {
                _gameState = newState;
                OnStateChanged?.Invoke(_gameState);
            }
        }
    }
}