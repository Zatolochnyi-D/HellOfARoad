using System;
using HoaR.LevelManagement;
using UnityEngine;
using Zenject;

namespace HoaR.GoalChecking
{
    public class GoalChecker : ITickable
    {
        public event Action<float> OnDistanceChanged;

        private readonly Transform _originPosition;
        private readonly Transform _destinationPosition;
        private readonly Transform _carTransform;

        private readonly float _roadLength;

        public GoalChecker(LevelOrigin originPosition, DestinationPosition destinationPosition, ICarTransformProvider carTransform)
        {
            _originPosition = originPosition.Value;
            _destinationPosition = destinationPosition.Value;
            _carTransform = carTransform.Value;

            _roadLength = Vector3.Distance(_originPosition.position, _destinationPosition.position);
        }

        public void Tick()
        {
            var distancePassed = Vector3.Distance(_originPosition.position, _carTransform.position);
            var normalizedDistancePassed = distancePassed / _roadLength;
            OnDistanceChanged?.Invoke(Mathf.Clamp01(normalizedDistancePassed));
        }
    }
}