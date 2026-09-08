using System;
using UnityEngine;
using Zenject;

namespace HoaR.Game.GoalChecking
{
    public class GoalChecker : ITickable
    {
        public event Action<float> OnDistanceChanged;

        private readonly Transform _originPosition;
        private readonly Transform _destinationPosition;
        private readonly Transform _trackedPosition;

        private readonly float _roadLength;

        public GoalChecker(OriginPosition originPosition, DestinationPosition destinationPosition, TrackedPosition trackedPosition)
        {
            _originPosition = originPosition.Value;
            _destinationPosition = destinationPosition.Value;
            _trackedPosition = trackedPosition.Value;

            _roadLength = Vector3.Distance(_originPosition.position, _destinationPosition.position);
        }

        public void Tick()
        {
            var distancePassed = Vector3.Distance(_originPosition.position, _trackedPosition.position);
            var normalizedDistancePassed = distancePassed / _roadLength;
            OnDistanceChanged?.Invoke(Mathf.Clamp01(normalizedDistancePassed));
        }
    }
}