using System;

namespace HoaR.Enemies
{
    public class EnemyStateManager
    {
        public event Action<EnemyState> OnStateChanged;

        private EnemyState _currentState = EnemyState.Idle;

        public void SwitchState(EnemyState newState)
        {
            if (newState != _currentState)
            {
                _currentState = newState;
                OnStateChanged?.Invoke(_currentState);
            }
        }
    }
}
