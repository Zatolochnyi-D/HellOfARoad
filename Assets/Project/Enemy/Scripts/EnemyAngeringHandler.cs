using HoaR.Game.GameStateManagement;
using UnityEngine;
using Zenject;

namespace HoaR.Enemies
{
    public class EnemyAngeringHandler : ITickable
    {
        private readonly Transform _selfTransform;
        private readonly IEnemyTarget _target;
        private readonly EnemySettings _settings;
        private readonly EnemyStateManager _stateManager;
        private readonly IGameStateManager<GameState> _gameStateManager;

        public EnemyAngeringHandler(Transform selfTransform,
                                    IEnemyTarget target,
                                    EnemySettings settings,
                                    EnemyStateManager stateManager,
                                    IGameStateManager<GameState> gameStateManager)
        {
            _selfTransform = selfTransform;
            _target = target;
            _settings = settings;
            _stateManager = stateManager;
            _gameStateManager = gameStateManager;
        }

        public void Tick()
        {
            if (_gameStateManager.CurrentState == GameState.GameWon || _gameStateManager.CurrentState == GameState.GameLost)
                return;
            if (_stateManager.CurrentState == EnemyState.Dead)
                return;
            if (Vector3.Distance(_selfTransform.position, _target.Position) <= _settings.AngeringDistance)
                _stateManager.SwitchState(EnemyState.Angered);
        }
    }
}