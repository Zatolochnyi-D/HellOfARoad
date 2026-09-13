using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace HoaR.Enemies
{
    public class EnemyAnimator : ITickable
    {
        private static readonly int IDLE_RUN_PARAMETER_HASH = Animator.StringToHash("IdleRun");

        private readonly Animator _animator;
        private readonly CancellationToken _destroyCancellationToken;

        public EnemyAnimator(Animator animator, CancellationToken destroyCancellationToken)
        {
            _animator = animator;
            _destroyCancellationToken = destroyCancellationToken;
        }

        private async void SetIdleRunFloat(CancellationToken token)
        {
            while (!token.IsCancellationRequested && _animator.GetFloat(IDLE_RUN_PARAMETER_HASH) != 1f)
            {
                _animator.SetFloat(IDLE_RUN_PARAMETER_HASH, 1f, 0.1f, Time.deltaTime);
                await Awaitable.NextFrameAsync();
            }
        }
        
        public void StartRunning()
        {
            SetIdleRunFloat(_destroyCancellationToken);
        }

        public void Tick()
        {
            if (Keyboard.current.spaceKey.IsPressed())
            {
                _animator.Play("Attack", 1, 0f);
            }
        }
    }
}