using System;
using HoaR.Game.GameStateManagement;

namespace HoaR.Enemies
{
    public class EnemyGameStateHandler : IDisposable
    {
        private readonly EnemyStateManager _enemyStateManager;
        private readonly IGameStateManager<GameState> _gameStateManager;

        public EnemyGameStateHandler(EnemyStateManager enemyStateManager, IGameStateManager<GameState> gameStateManager)
        {
            _enemyStateManager = enemyStateManager;
            _gameStateManager = gameStateManager;

            _gameStateManager.OnStateChanged += HandleGameStateChange;
        }

        private void HandleGameStateChange(GameState newState)
        {
            switch (newState)
            {
                case GameState.GameWon:
                case GameState.GameLost:
                    _enemyStateManager.SwitchState(EnemyState.Idle);
                    break;
            }
        }

        public void Dispose()
        {
            _gameStateManager.OnStateChanged -= HandleGameStateChange;
        }
    }
}