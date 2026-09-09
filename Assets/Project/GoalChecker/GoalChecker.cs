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

        public GoalChecker(LevelOrigin originPosition, ICarTransformProvider carTransform, LevelSettings levelSettings)
        {
            _originPosition = originPosition.Value;
            _carTransform = carTransform.Value;
            _levelSettings = levelSettings;
        }

        public void Tick()
        {
            var distancePassed = Vector3.Distance(_originPosition.position, _carTransform.position);
            var normalizedDistancePassed = distancePassed / _levelSettings.LevelLength;
            OnDistanceChanged?.Invoke(Mathf.Clamp01(normalizedDistancePassed));
        }
    }
}