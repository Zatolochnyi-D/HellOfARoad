using System;
using HoaR.Game.LevelManagement;
using HoaR.LevelManagement;
using UnityEngine;
using Zenject;

namespace HoaR.GoalChecking
{
    public class GoalChecker : ITickable
    {
        public event Action<float> OnDistanceChanged;

        private readonly Transform _originPosition;
        private readonly Transform _carTransform;
        private readonly LevelSettings _levelSettings;
        private readonly SignalBus _signalBus;

        private bool _destinationReached = false;

        public GoalChecker(LevelOrigin originPosition, ICarTransformProvider carTransform, LevelSettings levelSettings, SignalBus signalBus)
        {
            _originPosition = originPosition.Value;
            _carTransform = carTransform.Value;
            _levelSettings = levelSettings;
            _signalBus = signalBus;
        }

        public void Tick()
        {
            var distancePassed = Vector3.Distance(_originPosition.position, _carTransform.position);
            var normalizedDistancePassed = distancePassed / _levelSettings.LevelLength;
            OnDistanceChanged?.Invoke(Mathf.Clamp01(normalizedDistancePassed));
            if (normalizedDistancePassed >= 1f && !_destinationReached)
            {
                _signalBus.TryFire<PlayerReachedDestinationSignal>();
                _destinationReached = true;
            }
        }
    }
}