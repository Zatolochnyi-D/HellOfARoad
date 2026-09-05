using HoaR.Game.GameStateManagement;
using UnityEngine;
using Zenject;

namespace HoaR.Car
{
    public class CarController
    {
        private readonly SignalBus _signalBus;
        private readonly CarMover _carMover;

        public CarController(SignalBus signalBus, CarMover carMover)
        {
            _signalBus = signalBus;
            _carMover = carMover;

            _signalBus.Subscribe<GameStateChangeSignal>(HandleStateChange);
        }

        private void HandleStateChange(GameStateChangeSignal args)
        {
            switch (args.NewState)
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