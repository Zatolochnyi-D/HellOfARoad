using System.Threading;
using UnityEngine;

namespace HoaR.Enemies
{
    public class EnemyMover
    {
        private readonly Transform _selfTransform;
        private readonly IEnemyTarget _target;
        private readonly EnemyStateManager _stateManager;
        private readonly EnemySettings _settings;

        private CancellationTokenSource _runningLoopCancellation;

        public EnemyMover(Transform selfTransform, IEnemyTarget target, EnemyStateManager stateManager, EnemySettings settings)
        {
            _selfTransform = selfTransform;
            _target = target;
            _stateManager = stateManager;
            _settings = settings;

            _stateManager.OnStateChanged += HandleStateChange;
        }

        private async void RunningLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var directionToTarget = (_target.Position - _selfTransform.position).normalized;

                var rotateBy = 180f / _settings.TurnAroundTime * Mathf.Deg2Rad * Time.deltaTime;
                _selfTransform.forward = Vector3.RotateTowards(_selfTransform.forward, directionToTarget, rotateBy, 0f);
                _selfTransform.position += _settings.RunSpeed * Time.deltaTime * _selfTransform.forward;

                await Awaitable.NextFrameAsync();
            }
        }

        private void HandleStateChange(EnemyState newState)
        {
            switch (newState)
            {
                case EnemyState.Angered:
                    _runningLoopCancellation = new();
                    RunningLoop(_runningLoopCancellation.Token);
                    break;
                case EnemyState.Dead:
                    _runningLoopCancellation.Cancel();
                    break;
                default:
                    break;
            }
        }
    }
}