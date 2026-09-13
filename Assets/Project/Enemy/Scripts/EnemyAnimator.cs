using System.Threading;
using UnityEngine;

namespace HoaR.Enemies
{
    public class EnemyAnimator
    {
        private static readonly int IDLE_RUN_PARAMETER_HASH = Animator.StringToHash("IdleRun");

        private readonly Animator _animator;
        private readonly CancellationToken _destroyCancellationToken;

        public EnemyAnimator(Animator animator, CancellationToken destroyCancellationToken)
        {
            _animator = animator;
            _destroyCancellationToken = destroyCancellationToken;
        }

        private async void SetIdleRunFloat(bool forward, CancellationToken token)
        {
            var target = forward ? 1f : 0f;
            var step = forward ? 0.1f : -0.1f;
            while (!token.IsCancellationRequested && _animator.GetFloat(IDLE_RUN_PARAMETER_HASH) != target)
            {
                _animator.SetFloat(IDLE_RUN_PARAMETER_HASH, target, step, Time.deltaTime);
                await Awaitable.NextFrameAsync();
            }
        }

        public void StartRunning()
        {
            SetIdleRunFloat(true, _destroyCancellationToken);
        }
        
        public void StopRunning()
        {
            SetIdleRunFloat(false, _destroyCancellationToken);
        }
    }
}