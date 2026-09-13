using System;

namespace HoaR.Game.GameStateManagement
{
    public interface IGameStateManager<T> where T : Enum
    {
        public event Action<T> OnStateChanged;

        public T CurrentState { get; }

        public void ChangeState(T newState);
    }
}