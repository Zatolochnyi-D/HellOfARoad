using System.Threading;
using UnityEditor.UI;
using UnityEngine;

namespace HoaR.Enemies
{
    public class EnemyMover
    {
        private readonly Transform _selfTransform;
        private readonly Transform _targetTransform;
        private readonly EnemyStateManager _stateManager;
        private readonly EnemySettings _settings;

        public EnemyMover(Transform selfTransform, IAngerTrackable angerTrackable, EnemyStateManager stateManager, EnemySettings settings)
        {
            _selfTransform = selfTransform;
            _targetTransform = angerTrackable.Value;
            _stateManager = stateManager;
            _settings = settings;

            _stateManager.OnStateChanged += HandleStateChange;
        }

        private async void RunningLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var directionToTarget = (_targetTransform.position - _selfTransform.position).normalized;

                var rotateBy = 180f / _settings.TurnAroundTime * Mathf.Deg2Rad * Time.deltaTime;
                _selfTransform.forward = Vector3.RotateTowards(_selfTransform.forward, directionToTarget, rotateBy, 0f);
                _selfTransform.position += _settings.RunSpeed * Time.deltaTime * _selfTransform.forward;

                await Awaitable.NextFrameAsync();
            }
        }

        private void HandleStateChange(EnemyState newState)
        {
            if (newState == EnemyState.Angered)
            {
                RunningLoop(default);
            }
        }
    }
}