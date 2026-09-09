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

        public EnemyAngeringHandler(Transform selfTransform, IEnemyTarget target, EnemySettings settings, EnemyStateManager stateManager)
        {
            _selfTransform = selfTransform;
            _target = target;
            _settings = settings;
            _stateManager = stateManager;
        }

        public void Tick()
        {
            if (Vector3.Distance(_selfTransform.position, _target.Position) <= _settings.AngeringDistance)
                _stateManager.SwitchState(EnemyState.Angered);
        }
    }
}