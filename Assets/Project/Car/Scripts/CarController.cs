using HoaR.Game.GameStateManagement;
using UnityEngine;

namespace HoaR.Car
{
    public class CarController
    {
        private readonly IGameStateManager<GameState> _stateManager;
        private readonly CarMover _carMover;

        public CarController(IGameStateManager<GameState> stateManager, CarMover carMover)
        {
            _stateManager = stateManager;
            _carMover = carMover;

            _stateManager.OnStateChanged += HandleStateChange;
        }

        private void HandleStateChange(GameState newState)
        {
            switch (newState)
            {
                case GameState.Playing:
                    _carMover.Enable();
                    break;
                default:
                    Debug.LogWarning("CarController got unhandled state");
                    break;
            }
        }
    }
}