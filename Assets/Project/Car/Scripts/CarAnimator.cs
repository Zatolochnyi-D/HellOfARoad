using HoaR.Game.GameStateManagement;
using HoaR.HealthSystem.HealthComponent;
using UnityEngine;

namespace HoaR.Car
{
    public class CarAnimator
    {
        private static readonly int WHEEL_CLIP_HASH = Animator.StringToHash("WheelRotation");
        private static readonly int SHAKE_CLIP_HASH = Animator.StringToHash("HullShake");
        private static readonly int SHAKE_ANIMATION_DIRECTION_PARAMETER_HASH = Animator.StringToHash("ShakeDirection");
        private const int WHEEL_LAYER = 0;
        private const int SHAKE_LAYER = 1;

        private readonly Animator _animator;
        private readonly Health _health;

        private bool _isReverse = false;

        public CarAnimator(Animator animator, Health health, IGameStateManager<GameState> gameStateManager)
        {
            _animator = animator;
            _health = health;

            gameStateManager.OnStateChanged += HandleGameStateChange;
        }

        private void HandleGameStateChange(GameState newState)
        {
            switch (newState)
            {
                case GameState.Playing:
                    StartAnimation();
                    break;
            }
        }

        private void StartAnimation()
        {
            _animator.Play(WHEEL_CLIP_HASH, WHEEL_LAYER, 0f);

            _health.OnDamageReceived += _ =>
            {
                _animator.SetFloat(SHAKE_ANIMATION_DIRECTION_PARAMETER_HASH, _isReverse ? -1f : 1f);
                _animator.Play(SHAKE_CLIP_HASH, SHAKE_LAYER, _isReverse ? 1f : 0f);

                _isReverse = !_isReverse;
            };            
        }
    }
}